namespace apartment_portal_api.Models.Insights;

public partial class Insight
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Suggestion { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
}