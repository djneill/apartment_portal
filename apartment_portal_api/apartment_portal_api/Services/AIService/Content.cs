using System.Text.Json.Serialization;

namespace apartment_portal_api.Services.AIService;

public class Content
{
    [JsonPropertyName("parts")] public List<Parts> Parts { get; set; } = new();
}