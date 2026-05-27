using System.Text;
using System.Text.Json;
using Kumi.Core.Tools.Interfaces;
using Kumi.Core.Messages;
using Kumi.Core.Agents;
using Kumi.Domain.Messages;
using Kumi.LLM.Interfaces;
using Kumi.LLM.Integrations.Ollama;
using Kumi.Core.Configs.Interfaces;
using Kumi.Domain.Configs;
using System.Diagnostics;
using Kumi.LLM.Integrations.Gemini;

namespace Kumi.Core.Chats;

public class Chat(IToolQueryActions toolQueryActions,
                  IConfigQueryActions configQueryActions)
{
    private MessageHistory messageHistory;

    public async Task<string> PromptAgent(string message)
    {
        try
        {
            string tools = JsonSerializer.Serialize(await toolQueryActions.ListAllTools());
            this.messageHistory = new MessageHistory(tools);

            ILanguageModel languageModel = await InitializeLanguageModel(); 
            Agent agent = new Agent(toolQueryActions, languageModel, messageHistory);

            Message response = await agent.Prompt(message);

            return response.Content;
        }
        catch (Exception)
        {
            throw new ChatResponseException();
        }

    }

    private async Task<ILanguageModel> InitializeLanguageModel()
    {
        Config? config = await configQueryActions.FindConfigFor("SYSTEM");

        if (config is null)
        {
            throw new LanguageModelNotConfiguredException();
        }

        switch(config!.Type)
        {
            case LanguageModelProviderType.Ollama:
                return new Ollama(config.Model);
            case LanguageModelProviderType.Gemini:
                return new Gemini(config.Model, config.ApiKey!);
            default:
                break;
        }

        throw new Exception();
    } 

}
