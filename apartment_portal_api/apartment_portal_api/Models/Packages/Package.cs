using apartment_portal_api.Models.Statuses;

namespace apartment_portal_api.Models.Packages;

public partial class Package
{
    public int Id { get; set; }

    public int LockerNumber { get; set; }

    public int Code { get; set; }

    public int UnitId { get; set; }

    public int StatusId { get; set; }

    // Linking properties for EF Core
    public virtual Status Status { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
