using apartment_portal_api.Data.Repositories;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.InsightStatuses;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.LeaseStatuses;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Abstractions;

public interface IUnitOfWork
{
    public IRepository<Guest> GuestRepository { get; }
    public IRepository<ParkingPermit> ParkingPermitRepository { get; }
    public IssueRepository IssueRepository { get; }
    public IRepository<IssueType> IssueTypeRepository { get; }
    public PackageRepository PackageRepository { get; }
    public IRepository<Status> StatusRepository { get; }
    public IRepository<Unit> UnitRepository { get; }
    public UserRepository UserRepository { get; }
    public IRepository<UnitUser> UnitUserRepository { get; }
    public InsightRepository InsightRepository { get; }
    public IRepository<InsightStatus> InsightStatusRepository { get; }
    public LeaseAgreementRepository LeaseAgreementRepository { get; }
    public IRepository<LeaseStatus> LeaseStatusRepository { get; }
    public Task SaveAsync();
}