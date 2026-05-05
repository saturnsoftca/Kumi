using Kumi.Domain.Messages;
using Kumi.LLM.Integrations.Gemini.Models;
using Kumi.LLM.Integrations.Gemini.Mappings;
using Kumi.LLM.Interfaces;
using System.Text.Json;
using System.Text;

namespace Kumi.LLM.Integrations.Gemini;

public class Gemini : ILanguageModel
{

    private string _geminiUri = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";
    private string _geminiApiToken = ;

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

        var optiobns = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var payload = JsonSerializer.Serialize(new GeminiRequest
        {
            Contents = geminiMessages
        }, optiobns);

        StringContent jsonContent = new(payload, Encoding.UTF8, "application/json");
      
        HttpResponseMessage response = await httpClient.PostAsync(_geminiUri, jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        Console.WriteLine(jsonResponse);


        throw new NotImplementedException();
    }
}
