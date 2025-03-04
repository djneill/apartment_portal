namespace apartment_portal_api.Models.Packages;

public record PackageGetResponse(int Id, int LockerNumber, int Code, Models.Statuses.Status Status);