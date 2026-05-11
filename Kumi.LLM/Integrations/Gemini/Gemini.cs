using Kumi.Domain.Messages;
using Kumi.LLM.Integrations.Gemini.Models;
using Kumi.LLM.Integrations.Gemini.Mappings;
using Kumi.LLM.Interfaces;
using System.Text.Json;
using System.Text;

namespace Kumi.LLM.Integrations.Gemini;

public class Gemini : ILanguageModel
{

    private string _geminiUri;
    private string _geminiApiToken;

    public Gemini(string model, string token)
    {
        this._geminiUri = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent";
        this._geminiApiToken = token;
    }

    public async Task<Message> Chat(List<Message> messages)
    {
        GeminiMessageMapper geminiMessageMapper = new();
        GeminiMessage[] geminiMessages = messages
            .Select(message => geminiMessageMapper.ToGeminiMessage(message))
            .ToArray();

        HttpClient httpClient = new()
        {
        };
        httpClient.DefaultRequestHeaders.Add("x-goog-api-key", _geminiApiToken);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var payload = JsonSerializer.Serialize(new GeminiRequest
        {
            Contents = geminiMessages
        }, options);

        StringContent jsonContent = new(payload, Encoding.UTF8, "application/json");
      
        HttpResponseMessage response = await httpClient.PostAsync(_geminiUri, jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(jsonResponse);

        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(jsonResponse, options);

        return geminiMessageMapper.FromResponseToMessage(geminiResponse);
    }
}
