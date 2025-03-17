using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Models.Packages;

public class PackageGetByIdResponse{
    public int Id { get; set; }
    public int LockerNumber { get; set; }
    public int Code { get; set; }
    public Models.Statuses.Status Status { get; set; }
    public List<UserResponse> Tenants { get; set; } = new();
};