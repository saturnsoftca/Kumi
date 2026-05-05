using Kumi.LLM.Integrations.Gemini.Models;
using Kumi.Domain.Messages;

namespace Kumi.LLM.Integrations.Gemini.Mappings;

public class GeminiMessageMapper
{
    public GeminiMessage ToGeminiMessage(Message message)
    {
        String role = (message.Role == "system" || message.Role == "assistant") ? "MODEL" : message.Role;
        return new GeminiMessage
        {
           Role = role, 
           Parts = [
              new GeminiParts
              {
                  Text = message.Content
              }
           ]
        };
    }
}
