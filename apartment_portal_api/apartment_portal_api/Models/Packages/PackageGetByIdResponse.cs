using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Models.Packages;

public record PackageGetByIdResponse(int Id, int LockerNumber, int Code, Models.Statuses.Status Status, List<UserResponse> Tenants);