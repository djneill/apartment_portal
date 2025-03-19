using apartment_portal_api.Models.Insights;

namespace apartment_portal_api.Models.InsightStatuses;

public partial class InsightStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Insight> Insights { get; set; } = [];
}