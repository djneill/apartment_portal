using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.LeaseAgreements;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class LeaseAgreementRepository : Repository<Issue>
{
    private readonly PostgresContext _context;
    private readonly DbSet<LeaseAgreement> _dbSet;

    public LeaseAgreementRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<LeaseAgreement>();
    }
}