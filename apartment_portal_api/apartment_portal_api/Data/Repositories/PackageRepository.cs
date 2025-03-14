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
            .FromSql($"SELECT p.\"id\", p.\"lockerNumber\", p.\"code\", p.\"unitId\", p.\"statusId\" FROM \"packages\" AS p LEFT JOIN \"statuses\" AS S ON p.\"statusId\" = s.\"id\" LEFT JOIN \"units\" AS u ON p.\"unitId\" = u.\"id\" LEFT JOIN \"unitUsers\" AS uu ON u.\"id\" = uu.\"unitId\" WHERE uu.\"userId\" = {userId}")
            .Include(p => p.Status);

        if (statusId != 0)
        {
            query = query.Include(p => p.Status).Where(p => p.StatusId == statusId);
        }

        return await query.ToListAsync();
    }
}