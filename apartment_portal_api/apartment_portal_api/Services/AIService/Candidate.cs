using System.Text.Json.Serialization;

namespace apartment_portal_api.Services.AIService;

public class Candidate
{
    [JsonPropertyName("content")] public Content Content { get; set; } = new();
}