using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;
using Microsoft.AspNetCore.Identity;

namespace apartment_portal_api.Models.Users;

public partial class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public int StatusId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int ModifiedBy { get; set; }



    // Linking properties for EF Core
    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ApplicationUser ModifiedByNavigation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Guest> Guests { get; set; } = [];

    public virtual ICollection<Issue> Issues { get; set; } = [];



    // List of all UnitUsers that the current ApplicationUser is a part of. (Tenants can have multiple units)
    public virtual ICollection<UnitUser> UnitUserUsers { get; set; } = [];


    // List of all Users created by the current user
    public virtual ICollection<ApplicationUser> InverseCreatedByNavigation { get; set; } = [];


    // List of all Users modified by the current user
    public virtual ICollection<ApplicationUser> InverseModifiedByNavigation { get; set; } = [];


    // List of all UnitUsers created by the current user 
    public virtual ICollection<UnitUser> UnitUserCreatedByNavigations { get; set; } = [];


    // List of all UnitUsers created by the current user
    public virtual ICollection<UnitUser> UnitUserModifiedByNavigations { get; set; } = [];


}
