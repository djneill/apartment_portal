namespace apartment_portal_api.Models.Insights;

public record InsightPatchRequest(int Id, string ActionTaken, bool IsComplete);