using System.Text.Json.Serialization;
using apartment_portal_api.Models.Insights;

namespace apartment_portal_api.Services;

public class AIService
{
    private string? _apiKey;

    private HttpClient _httpClient = new();

    private readonly string _url =
        "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-lite:generateContent?key=";

    private readonly string prompt = """""
                                     You are an assistant to an apartment manager. Your job is to analyze issues
                                     you are given. You will create a title and summary of the issues, grouping them by the issueTypeId,
                                     then make a suggestion on the steps the apartment manager could take to resolve the issues.
                                     The title will be a short relevant summary of the issues. The summary will be a generalization of all
                                     the descriptions that have the same issueTypeId, being no more than 100 words. The suggestion will be
                                     the steps the apartment manager could take to resolve the issues of that type, being no more than 200 words.
                                     The response should be formatted into json and have the properties title, summary, and suggestion.
                                     
                                     The issues will be formatted like this:
                                     public class IssueAIPostRequest
                                     {
                                         public int IssueTypeId { get; set; }
                                         public string Description { get; set; } = default!;
                                     }
                                     """"";

    public AIService(IConfiguration iConfiguration)
    {
        _apiKey = iConfiguration["AIKey"];

        _url += _apiKey;
    }

    public async Task<ICollection<InsightPostRequest>> GenerateInsights(ICollection<IssueAIPostRequest> issues)
    {
        if (_apiKey is null)
        {
            throw new NullReferenceException("Missing API Key");
        }

        GeminiRequest reqBody = new(prompt, issues.ToString()); // serialize issues to json

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, reqBody);

        response.EnsureSuccessStatusCode();

        var insights = await response.Content.ReadFromJsonAsync<ICollection<InsightPostRequest>>();

        return insights ?? [];
    }

    
    public class GeminiRequest
    {
        public GeminiRequest(string prompt, string issues)
        {
            SystemInstruction = new SystemInstruction(prompt, issues);
        }

        [JsonPropertyName("system_instruction")]
        public SystemInstruction SystemInstruction { get; set; }
    }

    public class SystemInstruction
    {
        public SystemInstruction(string prompt, string issues)
        {
            Parts = new Parts(prompt);
            Contents = new Contents(issues);
        }

        [JsonPropertyName("parts")]
        public Parts Parts { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents {get; set;}
    }

    public class Contents
    {
        public Contents(string info)
        {
            Parts = new Parts(info);
        }

        [JsonPropertyName("parts")]
        public Parts Parts { get; set; }
    }

    public class Parts
    {
        public Parts(string info)
        {
            Text = info;
        }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

}