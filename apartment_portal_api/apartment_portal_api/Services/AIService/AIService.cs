using apartment_portal_api.Models.Insights;
using System.Text.Json;

namespace apartment_portal_api.Services.AIService;

public class AIService
{
    private readonly string? _apiKey;

    private readonly HttpClient _httpClient = new();

    private readonly string _url =
        "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-lite:generateContent?key=";

    private const string Prompt = """""
                                  You are an assistant to an apartment manager. Your job is to analyze issues
                                  you are given. You will create a title and summary of the issues, grouping them by the issueTypeId,
                                  then make a suggestion on the steps the apartment manager could take to resolve the issues.
                                  The title will be a short relevant summary of the issues. The summary will be a generalization of all
                                  the descriptions that have the same issueTypeId, being no more than 100 words. The suggestion will be
                                  the steps the apartment manager could take to resolve the issues of that type, being no more than 200 words.
                                  The response should be formatted into json and have the properties title, summary, and suggestion.
                                  Don't use "```json", just send me as plain text, not marked down.

                                  The issues will be formatted like this:
                                  public class IssueAIPostRequest
                                  {
                                      public int IssueTypeId { get; set; }
                                      public string Description { get; set; } = default!;
                                  }

                                  For each issue, generate an insight with a title, summary, and suggestion.
                                  Return an array of insights in valid JSON format.

                                  Issues Example:
                                  [
                                      { "IssueTypeId": 1, "Description": "Leaking faucet in apartment 3B" },
                                      { "IssueTypeId": 2, "Description": "Heating system not working in unit 5A" }
                                  ]

                                  Expected Response:
                                  [
                                      { "title": "Leaking Faucet", "summary": "Water leakage detected...", "suggestion": "Call maintenance." },
                                      { "title": "Heating Issue", "summary": "Heating is broken...", "suggestion": "Check the thermostat." }
                                  ]
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


        string issueJson = JsonSerializer.Serialize(issues);
        var reqBody = new GeminiRequest(Prompt, issueJson);

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, reqBody);

        var jsonResponse = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();

        string rawJsonString = JsonSerializer.Deserialize<GeminiApiResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "{}";

        var insights = JsonSerializer.Deserialize<List<InsightPostRequest>>(rawJsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        insights ??= [];

        return insights;
    }
}