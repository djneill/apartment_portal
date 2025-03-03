using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<Issue> Issue { get; set; }
}
