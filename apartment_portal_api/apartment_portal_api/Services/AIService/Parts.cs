using System.Text.Json.Serialization;

namespace apartment_portal_api.Services.AIService;

public class Parts
{
    [JsonPropertyName("text")] public string Text { get; set; }

    public Parts(string text)
    {
        Text = text;
    }
}