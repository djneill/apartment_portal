using apartment_portal_api.Models.InsightStatuses;

namespace apartment_portal_api.Models.Insights;

public partial class Insight
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Suggestion { get; set; } = default!;
    public string? ActionTaken { get; set; }
    public DateTime CreatedOn { get; set; }
    public int InsightStatusId { get; set; }

    public virtual InsightStatus InsightStatus { get; set; } = null!;
}