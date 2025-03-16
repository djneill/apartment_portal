namespace apartment_portal_api.Models.Insights;

public class IssueAIPostRequest
{
    public int IssueTypeId { get; set; }
    public string Description { get; set; } = default!;
}