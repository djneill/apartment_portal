using apartment_portal_api.Models.LeaseAgreements;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class LeaseAgreementRepository : Repository<LeaseAgreement>
{
    private readonly PostgresContext _context;
    private readonly DbSet<LeaseAgreement> _dbSet;

    public LeaseAgreementRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<LeaseAgreement>();
    }

    public async Task<ICollection<LeaseAgreement>> GetLeaseAgreementsAsync(int userId, int statusId)
    {
        IQueryable<LeaseAgreement> query = _dbSet
            .Include(lease => lease.UnitUser)
            .Include(lease => lease.Status);

        if (userId > 0)
        {
            query = query.Where(lease => lease.UnitUser.UserId == userId);
        }

        if (statusId > 0)
        {
            query = query.Where(lease => lease.LeaseStatusId == statusId);
        }

        var response = await query.ToListAsync();

        return response;
    }
}