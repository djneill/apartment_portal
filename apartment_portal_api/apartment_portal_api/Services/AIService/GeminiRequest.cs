using System.Text.Json.Serialization;

namespace apartment_portal_api.Services.AIService;

public class GeminiRequest
{
    [JsonPropertyName("contents")] public List<Content> Contents { get; set; }

    public GeminiRequest(string prompt, string issues)
    {
        Contents =
        [
            new Content
            {
                Parts =
                [
                    new Parts($"{prompt}\n\nIssues:\n{issues}") // Merge prompt and issues
                ]
            }
        ];
    }
}