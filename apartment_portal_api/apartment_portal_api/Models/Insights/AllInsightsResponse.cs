namespace apartment_portal_api.Models.Insights;

public class AllInsightsResponse(ICollection<InsightResponse> currentInsights, ICollection<InsightResponse> pastInsights)
{
    public ICollection<InsightResponse> CurrentInsights { get; set; } = currentInsights;
    public ICollection<InsightResponse> PastInsights { get; set; } = pastInsights;
}