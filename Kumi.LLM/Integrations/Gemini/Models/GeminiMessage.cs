namespace Kumi.LLM.Integrations.Gemini.Models;

public class GeminiMessage
{
    public required string Role { get; set; }
    public required GeminiParts[] Parts { get; set; }
}
