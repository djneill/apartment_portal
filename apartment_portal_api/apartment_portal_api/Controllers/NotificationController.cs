using apartment_portal_api.Abstractions;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.LeaseAgreements;
using apartment_portal_api.Models.Notifications;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.UnitUsers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace apartment_portal_api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("latest")]
    public async Task<ActionResult<ICollection<NotificationDTO>>> GetLatestNotifications([FromQuery] int userId = 0)
    {
        var isUserOrAdmin = IsUserOrAdmin(userId);
        if (isUserOrAdmin is not null) return isUserOrAdmin;


        UnitUser[] unitUsers = await GetUnitUsers(userId);
        if (unitUsers.Length == 0) return BadRequest("User is not associated with any units.");

        var userUnitIds = unitUsers.Select(uu => uu.UnitId);

        List<NotificationDTO> packages = await GetPackageNotifications(userUnitIds, userId);

        List<NotificationDTO> issues = await GetIssueNotifications(userId);

        List<NotificationDTO> leaseNotifications = await GetLeaseAgreementNotifications(userId);

        var notifications = leaseNotifications
            .Concat(packages)
            .Concat(issues)
            .OrderBy(n => n.Date)
            .ToList();

        return Ok(notifications);
    }


    private async Task<List<NotificationDTO>> GetLeaseAgreementNotifications(int userId)
    {
        List<NotificationDTO> leaseNotifications = [];
        IEnumerable<LeaseAgreement> leasesResponse;

        if (userId > 0)
        {
            leasesResponse = await unitOfWork.LeaseAgreementRepository.GetAsync(lease =>
                    lease.LeaseStatusId == 3 &&
                    lease.UnitUser.UserId == userId ||
                    lease.LeaseStatusId == 1 &&
                    lease.UnitUser.UserId == userId,
                nameof(LeaseAgreement.UnitUser));
        }
        else
        {
            leasesResponse = await unitOfWork.LeaseAgreementRepository.GetAsync(lease =>
                    lease.LeaseStatusId == 3 ||
                    lease.LeaseStatusId == 1,
                $"{nameof(LeaseAgreement.UnitUser)}");
        }

        foreach (var lease in leasesResponse)
        {
            if (lease.LeaseStatusId == 3)
            {
                DateTime startDateTime = lease.StartDate.ToDateTime(new TimeOnly());

                NotificationDTO notification = new()
                {
                    Type = "Lease",
                    Date = startDateTime,
                    Message = $"Reminder: Your upcoming lease needs to be signed before {startDateTime:MMMM dd, yyyy}."
                };

                if (userId == 0)
                    notification.Message =
                        $"Unit {lease.UnitUser.Unit.Number}: Lease signature is due by {startDateTime:MMMM dd, yyyy}.";

                leaseNotifications.Add(notification);

                continue;
            }



            DateTime expiration = lease.EndDate.ToDateTime(new TimeOnly());
            int daysUntilExpiration = (int)(expiration - DateTime.UtcNow).TotalDays;
            if (lease.LeaseStatusId == 1 && daysUntilExpiration is <= 30 and >= 0)
            {
                var notification = new NotificationDTO
                {
                    Type = "Lease",
                    Message = $"Reminder: Your lease expires on {expiration:MMMM dd, yyyy}.",
                    Date = expiration
                };

                if (userId == 0) notification.Message =
                    $"Unit {lease.UnitUser.Unit.Number}: Lease expires on {expiration:MMMM dd, yyyy}.";

                leaseNotifications.Add(notification);
            }
        }

        return leaseNotifications;
    }

    private async Task<List<NotificationDTO>> GetIssueNotifications(int userId)
    {
        IEnumerable<Issue> issueList;
        if (userId > 0)
        {
            issueList = await unitOfWork.IssueRepository.GetAsync(i =>
                    i.UserId == userId && i.Status.Name != "Inactive",
                includeProperties: nameof(Issue.Status)
            );
        }
        else
        {
            issueList = await unitOfWork.IssueRepository.GetAsync(i => i.Status.Name != "Inactive", nameof(Issue.Status));
        }

        var issues = issueList
            .OrderByDescending(i => i.CreatedOn)
            .Select(i => new NotificationDTO
            {
                Type = "Issue",
                Message = $"Issue: {i.Description}. Status: {i.Status.Name}",
                Date = i.CreatedOn
            })
            .ToList();

        return issues;
    }

    private async Task<List<NotificationDTO>> GetPackageNotifications(IEnumerable<int> userUnitIds, int userId)
    {
        IEnumerable<Package> packageLists = [];
        if (userId > 0)
        {
            packageLists = await unitOfWork.PackageRepository.GetAsync(p =>
                    userUnitIds.Contains(p.UnitId) && p.Status.Name == "Arrived",
                nameof(Package.Status));
        }

        var packages = packageLists.Select(p => new NotificationDTO
        {
            Type = "Package",
            Message = $"You have a new package at the front desk. Status: {p.Status.Name}",
            Date = DateTime.UtcNow
        })
            .ToList();

        return packages;
    }

    private async Task<UnitUser[]> GetUnitUsers(int userId)
    {
        IEnumerable<UnitUser> unitUsersRes;

        if (userId > 0)
        {
            unitUsersRes = await unitOfWork.UnitUserRepository.GetAsync(uu => uu.UserId == userId, nameof(UnitUser.Unit));
        }
        else
        {
            unitUsersRes = await unitOfWork.UnitUserRepository.GetAsync(includeProperties: nameof(UnitUser.Unit));
        }

        var unitUsers = unitUsersRes.ToArray();

        return unitUsers;
    }
    private ActionResult? IsUserOrAdmin(int requestUserId)
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userClaim is null)
        {
            return Unauthorized();
        }

        bool isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userClaim.Value != requestUserId.ToString())
        {
            return Forbid();
        }

        return null;
    }
}