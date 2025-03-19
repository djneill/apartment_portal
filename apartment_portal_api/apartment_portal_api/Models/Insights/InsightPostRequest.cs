namespace apartment_portal_api.Models.Insights;

public class InsightPostRequest
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Suggestion { get; set; } = default!;
    public int InsightStatusId = 1;
}