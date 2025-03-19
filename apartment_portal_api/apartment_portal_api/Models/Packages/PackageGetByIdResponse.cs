using apartment_portal_api.DTOs;
using apartment_portal_api.Models.Statuses;

namespace apartment_portal_api.Models.Packages;

public class PackageGetByIdResponse
{
    public int Id { get; set; }
    public int LockerNumber { get; set; }
    public int Code { get; set; }
    public StatusResponse Status { get; set; } = default!;
    public UnitResponseDTO Unit { get; set; } = default!;
};