using apartment_portal_api.DTOs;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.Users;

namespace apartment_portal_api.Models.Issues;

public class IssueResponse
{
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public IssueTypeResponse IssueType { get; set; }
    public StatusResponse Status { get; set; }
    public UserResponse User { get; set; }
}