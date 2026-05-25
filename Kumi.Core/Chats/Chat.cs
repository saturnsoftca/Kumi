using System.Text;
using System.Text.Json;
using Kumi.Core.Tools.Interfaces;
using Kumi.Core.Messages;
using Kumi.Core.Agents;
using Kumi.Domain.Messages;
using Kumi.LLM.Interfaces;
using Kumi.LLM.Integrations.Ollama;

namespace Kumi.Core.Chats;

public class Chat(IToolQueryActions toolQueryActions)
{
    private MessageHistory messageHistory;

    public async Task<string> PromptAgent(string message)
    {
        string tools = JsonSerializer.Serialize(await toolQueryActions.ListAllTools());
        this.messageHistory = new MessageHistory(tools);
        Agent agent = new Agent(toolQueryActions, new Ollama("gemma4:26b"), messageHistory);
        Message response = await agent.Prompt(message);
        return response.Content;
    }
}
