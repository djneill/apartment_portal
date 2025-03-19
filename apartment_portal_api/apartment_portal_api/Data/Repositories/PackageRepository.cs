using apartment_portal_api.Models.Packages;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class PackageRepository : Repository<Package>
{
    private readonly PostgresContext _context;
    private readonly DbSet<Package> _dbSet;
    public PackageRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Package>();
    }

    public async Task<ICollection<Package>> GetByUserId(int userId, int statusId)
    {
        IQueryable<Package> query = _dbSet
            .Include(p => p.Status);

        if (userId != 0)
        {
            query = query
                .Include(p => p.Unit)
                .ThenInclude(u => u.UnitUsers)
                .Where(p => p.Unit.UnitUsers.Any(uu => uu.UserId == userId));
        }

        if (statusId != 0)
        {
            query = query.Where(p => p.StatusId == statusId);
        }

        return await query.ToListAsync();
    }
}