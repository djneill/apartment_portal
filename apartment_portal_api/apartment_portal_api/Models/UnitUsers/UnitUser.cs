using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Models.UnitUsers;

public partial class UnitUser
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int UnitId { get; set; }

    public bool IsPrimary { get; set; }

    public string LeaseAgreement { get; set; } = null!;

    public DateTime LeaseExpiration { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int ModifiedBy { get; set; }


    // Linking properties for EF Core
    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ApplicationUser ModifiedByNavigation { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}
