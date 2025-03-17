using System.Text.Json.Serialization;

namespace apartment_portal_api.Services.AIService;

public class GeminiApiResponse
{
    [JsonPropertyName("candidates")] public List<Candidate> Candidates { get; set; } = [];
}