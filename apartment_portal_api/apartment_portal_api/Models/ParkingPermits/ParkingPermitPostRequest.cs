namespace apartment_portal_api.Models.ParkingPermits;

public record ParkingPermitPostRequest(string VehicleMake, string VehicleModel, string LicensePlate, string LicensePlateState);