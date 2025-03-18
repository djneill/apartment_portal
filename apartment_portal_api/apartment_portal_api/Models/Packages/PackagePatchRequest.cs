namespace apartment_portal_api.Models.Packages;

public record PackagePatchRequest(int Id, int? UnitId, int? LockerNumber, int? StatusId);