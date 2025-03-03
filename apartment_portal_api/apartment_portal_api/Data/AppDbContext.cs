using apartment_portal_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace apartment_portal_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Users> Users { get; set; }
    public DbSet<Issues> Issues { get; set; }
}
