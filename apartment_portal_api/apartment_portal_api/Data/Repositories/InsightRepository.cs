using apartment_portal_api.Models.Insights;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Services;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class InsightRepository: Repository<Insight>
{
    private readonly DbSet<Insight> _dbSet;
    private readonly PostgresContext _context;

    public InsightRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Insight>();
    }

    public async Task<ICollection<Issue>> GenerateInsights()
    {
        var query = _context.Issues
            .FromSql($"SELECT * FROM \"issues\" WHERE \"issueTypeId\" IN (SELECT \"issueTypeId\" FROM \"issues\" WHERE age(\"createdOn\") < make_interval(days => 1) GROUP BY \"issueTypeId\" ORDER BY COUNT(\"issueTypeId\") DESC LIMIT 3) AND age(\"createdOn\") < make_interval(days => 1) AND \"statusId\" = 1;");

        var res = await query.ToListAsync();

        return res;
    }
}