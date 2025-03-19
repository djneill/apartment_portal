namespace apartment_portal_api.Models.ParkingPermits;

public class ParkingPermitDTO
{
    public int Id { get; set; }

    public required int GuestId { get; set; }

    public required string VehicleMake { get; set; }

    public required string VehicleModel { get; set; }

    public required string LicensePlate { get; set; }

    public required string LicensePlateState { get; set; }

}