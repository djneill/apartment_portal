using apartment_portal_api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class UserRepository : Repository<ApplicationUser>
{
    private readonly DbSet<ApplicationUser> _dbSet;

    public UserRepository(PostgresContext context) : base(context)
    {
        _dbSet = context.Set<ApplicationUser>();
    }

    public async Task<ICollection<ApplicationUser>> GetUsers()
    {
        IQueryable<ApplicationUser> query = _dbSet
            .Include(user => user.Status)
            .Include(user => user.UnitUserUsers)
            .ThenInclude(uu => uu.Unit);

        ICollection<ApplicationUser> users = (await query
            .ToListAsync());

        return users;
    }
}