namespace apartment_portal_api.Entities;

public class ParkingPermit
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public string VehicleMake { get; set; }
    public string VehicleModel { get; set; }
    public string LicensePlate { get; set; }
    public string LicensePlateState { get; set; }
}