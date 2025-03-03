using apartment_portal_api.Abstractions;

namespace apartment_portal_api.Models.Issues;

public class Issue : Entity
{
    public Issue(
        int id,
        int unitUsersId,
        int issueTypeId)
    : base(id)
    {
        UnitUsersId = unitUsersId;
        IssueTypeId = issueTypeId;
    }

    public int UnitUsersId { get; set; }
    public int IssueTypeId { get; set; }
}