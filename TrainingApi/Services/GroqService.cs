using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TrainingApi.Services;

public class GroqService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public GroqService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["Groq:ApiKey"] ?? throw new Exception("Groq API key not set. Did you run dotnet user-secrets set?");
    }

    public async Task<string> GetFeedbackAsync(int score, int totalQuestions, List<string> choiceQualities)
    {
        var qualitiesSummary = string.Join(", ", choiceQualities);

        var prompt = $"""
            You are a cybersecurity training coach. A trainee just completed a security incident-response
            simulation, scoring {score}/{totalQuestions}. Their choice qualities in order were: {qualitiesSummary}.
            Write a short (3-4 sentences), encouraging but honest feedback paragraph in English, highlighting what
            they did well and one concrete thing to improve. Do not use markdown formatting.
            """;

        var requestBody = new
        {
            model = "openai/gpt-oss-20b",
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            temperature = 0.7
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _http.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            return $"(AI feedback unavailable: {response.StatusCode})";
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseBody);
        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return content ?? "(No feedback generated)";
    }
}