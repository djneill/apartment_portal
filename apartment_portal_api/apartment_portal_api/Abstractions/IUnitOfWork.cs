using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Guest> GuestRepository { get; }
    public IRepository<Issue> IssueRepository { get; }
    public IRepository<Package> PackageRepository { get; }
    public IRepository<Status> StatusRepository { get; }
    public IRepository<Unit> UnitRepository { get; }
    public IRepository<ApplicationUser> UserRepository { get; }
    public Task SaveAsync();
}