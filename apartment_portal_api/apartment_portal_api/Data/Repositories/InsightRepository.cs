using apartment_portal_api.Models.Insights;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class InsightRepository : Repository<Insight>
{
    private readonly DbSet<Insight> _dbSet;
    private readonly PostgresContext _context;

    public InsightRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Insight>();
    }

    public async Task<ICollection<Insight>> GetPastInsights()
    {
        IQueryable<Insight> query = _dbSet.OrderByDescending(insight => insight.CreatedOn).Take(5);

        return await query.ToListAsync();
    }
}