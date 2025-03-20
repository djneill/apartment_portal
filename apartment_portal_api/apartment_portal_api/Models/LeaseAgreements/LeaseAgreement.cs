using apartment_portal_api.Models.LeaseStatuses;
using apartment_portal_api.Models.UnitUsers;

namespace apartment_portal_api.Models.LeaseAgreements;

public class LeaseAgreement
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly? SignedOn { get; set; }
    public string Link { get; set; } = default!;
    public int UnitUsersId { get; set; }
    public int LeaseStatusId { get; set; }

    public virtual LeaseStatus Status { get; set; } = default!;
    public virtual UnitUser UnitUser { get; set; } = default!;
}