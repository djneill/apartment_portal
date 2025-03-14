using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apartment_portal_api.DTOs;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Packages;
using System.Security.Claims;
using AutoMapper;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("api/notifications")]
public class Notifications : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public Notifications(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet("latest")]
    public async Task<ActionResult> GetLatestNotifications([FromQuery] int? userId, [FromQuery] int? limit)
    {
        var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(loggedInUserId == null)
        {
            return Unauthorized();
        }

        int.TryParse(loggedInUserId, out int loggedInUserIdInt);
        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userId.HasValue && userId != loggedInUserIdInt)
        {
            return Forbid();
        }


        var userUnits = await _unitOfWork.UnitUserRepository.GetAsync(uu => 
        (isAdmin || uu.UserId == loggedInUserIdInt) &&
        (!userId.HasValue || uu.UserId == userId)
        );
        var userUnitIds = userUnits.Select(uu => uu.UnitId).ToList();
        if (!userUnitIds.Any()) return NotFound("User is not associated with any units.");

        // int userUnitId = userUnit.First().UnitId;

        var packageLists = await _unitOfWork.PackageRepository.GetAsync(p =>
        (isAdmin || userUnitIds.Contains(p.UnitId)) &&
        (!userId.HasValue || (userUnitIds.Contains(p.UnitId) && p.Status.Name == "Arrived")),
            includeProperties: nameof(Package.Status)
        );
        var packageList = packageLists.ToList();

        var packages = packageList.Select(p => new NotificationDTO
            {
                Type = "Package",
                Message = $"You have a new package at the front desk. Status: {(p.Status != null ? p.Status.Name : "Unknown")}",
                Date = DateTime.UtcNow
            })
            .ToList();


        var issueLists = await _unitOfWork.IssueRepository.GetAsync(i =>
        (isAdmin || i.UserId == loggedInUserIdInt) &&
        (!userId.HasValue || (i.UserId == userId && i.Status.Name != "Inactive")),
        includeProperties: nameof(Issue.Status)
        );

        var issueList = issueLists.ToList();
        var issues = issueList
            .OrderByDescending(i => i.CreatedOn)
            .Select(i => new NotificationDTO
            {
                Type = "Issue",
                Message = $"Issue: {i.Description}. Status: {(i.Status != null ? i.Status.Name : "Unknown")}",
                Date = i.CreatedOn
            })
            .ToList();

        var users = await _unitOfWork.UserRepository.GetAsync(u =>
        (isAdmin || u.Id == loggedInUserIdInt) && 
        (!userId.HasValue || u.Id == userId)
        );
        var user = users.FirstOrDefault();

        var leaseNotification = new List<NotificationDTO>();
        if (user != null)
        {
            DateTime leaseExpiration = user.CreatedOn.AddYears(1);
            var daysUntilExpiration = (leaseExpiration - DateTime.UtcNow).TotalDays;
            if (daysUntilExpiration <= 30)
            {
                leaseNotification.Add(new NotificationDTO
                {
                    Type = "Lease",
                    Message = $"Reminder: Your lease expires on {leaseExpiration:MMMM dd, yyyy}.",
                    Date = leaseExpiration
                });
            }
        }
        
        var notifications = packages
            .Concat(issues)
            .Concat(leaseNotification)
            .OrderByDescending(n => n.Date)
            .Take(limit.GetValueOrDefault(10)) 
            .ToList();

        return Ok(notifications);
    }
}