using apartment_portal_api.Models.Guests;

namespace apartment_portal_api.Models.ParkingPermits;

public partial class ParkingPermit
{
    public int Id { get; set; }

    public int GuestId { get; set; }

    public string VehicleMake { get; set; } = null!;

    public string VehicleModel { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string LicensePlateState { get; set; } = null!;

    // Linking properties for EF Core
    public virtual Guest Guest { get; set; } = null!;
}
