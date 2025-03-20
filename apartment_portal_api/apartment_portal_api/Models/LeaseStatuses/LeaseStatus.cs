using apartment_portal_api.Models.LeaseAgreements;

namespace apartment_portal_api.Models.LeaseStatuses;

public class LeaseStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public virtual ICollection<LeaseAgreement> LeaseAgreements { get; set; } = [];
}