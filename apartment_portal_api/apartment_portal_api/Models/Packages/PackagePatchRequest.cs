namespace apartment_portal_api.Models.Packages;

public record PackagePatchRequest(int Id, int? UnitUsersId, int? LockerNumber, int? Code, int? StatusId);