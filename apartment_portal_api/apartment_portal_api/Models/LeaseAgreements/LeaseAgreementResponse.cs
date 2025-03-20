using apartment_portal_api.Models.LeaseStatuses;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Models.LeaseAgreements;

public class LeaseAgreementResponse
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? SignedOn { get; set; }
    public string Link { get; set; } = default!;
    public UnitUserResponse UnitUser { get; set; } = default!;
    public LeaseStatusResponse Status { get; set; } = default!;
}