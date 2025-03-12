using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data.Repositories;

public class IssueRepository: Repository<Issue>
{
    private readonly PostgresContext _context;
    private readonly DbSet<Issue> _dbSet;

    public IssueRepository(PostgresContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Issue>();
    }

    public async Task<ICollection<Issue>> GetIssues(int userId, int recordRetrievalCount, int statusId, bool orderByDesc)
    {
        IQueryable<Issue> query = _dbSet
            .Include(issue => issue.IssueType)
            .Include(issue => issue.Status)
            .Include(issue => issue.ApplicationUser)
            .ThenInclude(user => user.Status);

        if (userId != 0)
        {
            query = query.Where(issue => issue.UserId == userId);
        }

        if (statusId  != 0)
        {
            query = query.Where(issue => issue.StatusId == statusId);
        }

        if (orderByDesc)
        {
            query = query.OrderByDescending(issue => issue.CreatedOn);
        }
        else
        {
            query = query.OrderBy(issue => issue.CreatedOn);
        }

        ICollection<Issue> issues = await query
            .Take(recordRetrievalCount).ToListAsync();

        return issues;
    }
}
