using apartment_portal_api.Data.Repositories;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Guest> GuestRepository { get; }
    public IRepository<ParkingPermit> ParkingPermitRepository { get; }
    public IssueRepository IssueRepository { get; }
    public PackageRepository PackageRepository { get; }
    public IRepository<Status> StatusRepository { get; }
    public IRepository<Unit> UnitRepository { get; }
    public IRepository<ApplicationUser> UserRepository { get; }
    public IRepository<UnitUser> UnitUserRepository { get; }
    public IRepository<Insight> InsightRepository { get; }
    public Task SaveAsync();
}