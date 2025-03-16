using apartment_portal_api.Abstractions;
using apartment_portal_api.Data.Repositories;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgresContext _context;
    private IRepository<Guest>? _guestRepository;
    private IRepository<ParkingPermit>? _parkingPermitRepository;
    private IssueRepository? _issueRepository;
    private IRepository<IssueType>? _issueTypeRepository;
    private PackageRepository? _packageRepository;
    private IRepository<Status>? _statusRepository;
    private IRepository<Unit>? _unitRepository;
    private UserRepository? _userRepository;
    private IRepository<UnitUser>? _unitUserRepository;
    private IRepository<Insight>? _insightRepository;

    public UnitOfWork(PostgresContext context)
    {
        _context = context;
    }

    public IRepository<Guest> GuestRepository
    {
        get
        {
            _guestRepository ??= new Repository<Guest>(_context);
            return _guestRepository;
        }
    }    
    public IRepository<ParkingPermit> ParkingPermitRepository
    {
        get
        {
            _parkingPermitRepository ??= new Repository<ParkingPermit>(_context);
            return _parkingPermitRepository;
        }
    }

    public IssueRepository IssueRepository
    {
        get
        {
            _issueRepository ??= new IssueRepository(_context);
            return _issueRepository;
        }
    }
    
    public IRepository<IssueType> IssueTypeRepository
    {
        get
        {
            _issueTypeRepository ??= new Repository<IssueType>(_context);
            return _issueTypeRepository;
        }
    }

    public PackageRepository PackageRepository
    {
        get
        {
            _packageRepository ??= new PackageRepository(_context);
            return _packageRepository;
        }
    }

    public IRepository<Status> StatusRepository
    {
        get
        {
            _statusRepository ??= new Repository<Status>(_context);
            return _statusRepository;
        }
    }

    public IRepository<Unit> UnitRepository
    {
        get
        {
            _unitRepository ??= new Repository<Unit>(_context);
            return _unitRepository;
        }
    }

    public UserRepository UserRepository
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }
    public IRepository<UnitUser> UnitUserRepository
    {
        get
        {
            _unitUserRepository ??= new Repository<UnitUser>(_context); 
            return _unitUserRepository;
        }
    }

    public IRepository<Insight> InsightRepository
    {
        get
        {
            _insightRepository ??= new Repository<Insight>(_context);
            return _insightRepository;
        }
    }    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}