namespace apartment_portal_api.Models.Packages;

public record PackagePutRequest(int Id, int UnitId, int LockerNumber, int StatusId);