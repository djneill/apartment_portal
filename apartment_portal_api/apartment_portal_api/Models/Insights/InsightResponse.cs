using apartment_portal_api.Models.InsightStatuses;

namespace apartment_portal_api.Models.Insights;

public class InsightResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Suggestion { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public InsightStatusResponse Status { get; set; } = default!;
}