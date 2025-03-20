namespace apartment_portal_api.Models.ParkingPermits;

public class ParkingPermitPatchDTO
{
    public int Id { get; set; }
    public string? VehicleMake { get; set; }
    public string? VehicleModel { get; set; }
    public string? LicensePlate { get; set; }
    public string? LicensePlateState { get; set; }
}