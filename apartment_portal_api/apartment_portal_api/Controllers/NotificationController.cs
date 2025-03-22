using System.Security.Claims;
using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Notifications;
using apartment_portal_api.Models.Packages;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("latest")]
    public async Task<ActionResult<ICollection<NotificationDTO>>> GetLatestNotifications([FromQuery] int? userId, [FromQuery] int? limit)
    {
        // Retrieve the current user's ID from claims.
        var loggedInUserIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(loggedInUserIdStr))
        {
            return Unauthorized();
        }
        int loggedInUserId = int.Parse(loggedInUserIdStr);
        bool isAdmin = User.IsInRole("Admin");

        // If a userId is provided and the caller is not an admin,
        // ensure that the requested userId matches the logged-in user.
        if (userId.HasValue && !isAdmin && userId.Value != loggedInUserId)
        {
            return Forbid();
        }

        // For non-admins, if userId is not specified, default to the logged-in user.
        int effectiveUserId = isAdmin ? (userId ?? 0) : loggedInUserId;

        // Retrieve unit associations for the effective user.
        var userUnits = await _unitOfWork.UnitUserRepository.GetAsync(uu =>
            (!userId.HasValue || uu.UserId == effectiveUserId)
        );
        var userUnitIds = userUnits.Select(uu => uu.UnitId).ToList();
        if (!userUnitIds.Any())
        {
            return NotFound("User is not associated with any units.");
        }

        var packageLists = await _unitOfWork.PackageRepository.GetAsync(p =>
            userUnitIds.Contains(p.UnitId) &&
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
            (!userId.HasValue || (i.UserId == effectiveUserId && i.Status.Name != "Inactive")),
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
            (!userId.HasValue || u.Id == effectiveUserId)
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