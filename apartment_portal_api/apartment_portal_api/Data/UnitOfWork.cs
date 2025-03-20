using apartment_portal_api.Abstractions;
using apartment_portal_api.Data.Repositories;
using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.InsightStatuses;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.LeaseStatuses;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;
using apartment_portal_api.Services.AIService;

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
    private InsightRepository? _insightRepository;
    private IRepository<InsightStatus>? _insightStatusRepository;
    private LeaseAgreementRepository? _leaseAgreementRepository;
    private IRepository<LeaseStatus>? _leaseStatusRepository;

    private readonly AIService _aiService;

    public UnitOfWork(PostgresContext context, AIService aiService)
    {
        _context = context;
        _aiService = aiService;
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

    public InsightRepository InsightRepository
    {
        get
        {
            _insightRepository ??= new InsightRepository(_context);
            return _insightRepository;
        }
    }

    public IRepository<InsightStatus> InsightStatusRepository
    {
        get
        {
            _insightStatusRepository ??= new Repository<InsightStatus>(_context); 
            return _insightStatusRepository;
        }
    }

    public LeaseAgreementRepository LeaseAgreementRepository
    {
        get
        {
            _leaseAgreementRepository ??= new LeaseAgreementRepository(_context);
            return _leaseAgreementRepository;
        }
    }

    public IRepository<LeaseStatus> LeaseStatusRepository
    {
        get
        {
            _leaseStatusRepository ??= new Repository<LeaseStatus>(_context); 
            return _leaseStatusRepository;
        }
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}