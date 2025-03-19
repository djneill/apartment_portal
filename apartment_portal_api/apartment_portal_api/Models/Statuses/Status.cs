using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.Users;


namespace apartment_portal_api.Models.Statuses;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    // Linking properties for EF Core
    public virtual ICollection<Package> Packages { get; set; } = [];
    public virtual ICollection<Issue> Issues { get; set; } = [];

    public virtual ICollection<Unit> Units { get; set; } = [];

    public virtual ICollection<ApplicationUser> Users { get; set; } = [];

}
