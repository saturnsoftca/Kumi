using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Kumi.Core.Messages;
using Kumi.Domain.Messages;
using Kumi.LLM.Interfaces;
using Kumi.Domain.Tools;
using Kumi.Core.Tools.Interfaces;
using Kumi.LLM.Integrations.Ollama;

namespace Kumi.Core.Agents;

public class Agent
{
    private MessageHistory _messageHistory;
    private ILanguageModel _llm;
    private IToolQueryActions _toolQueryActions;

    public Agent(IToolQueryActions toolQueryActions, ILanguageModel languageModel, MessageHistory messageHistory) 
    {
        this._messageHistory = messageHistory;
        this._toolQueryActions = toolQueryActions;
        this._llm = languageModel;
    }

    public async Task<Message> Prompt(string message)
    {
       this._messageHistory.AppendUserMessage(message);
       Message response = await _llm.Chat(this._messageHistory.History);
       this._messageHistory.Append(response);
       return await ParseMessage(response.Content);
    }

    public async Task<Message> ParseMessage(string llmResponse)
    {
        this._messageHistory.PrintHistory();
        var wrapped = $"<root>{llmResponse}</root>";
        XElement element = XElement.Parse(wrapped);

        var pause = element.Element("pause");
        var response = element.Element("response")?.Value;

        if (pause != null)
        {
            string? toolResponse = await MaybeCallTool(element);
            this._messageHistory.AppendUserMessage(toolResponse); 
            Message newChatResponse = await _llm.Chat(this._messageHistory.History);
            this._messageHistory.Append(newChatResponse);
            return await ParseMessage(newChatResponse.Content); 
        }
        if (response != null) 
        {
            return new Message
            {
                Role = "assistant",
                Content = element.Element("response").Value
            };
        }

        return null;
    }

    public async Task<string?> MaybeCallTool(XElement element)
    {
        string rawToolCall = element.Element("call_tool").Value;
        if (rawToolCall != null) 
        {
            return await CallTool(rawToolCall);
        }
        return null;
    }

    public async Task<string> CallTool(string rawToolCall)
    {
        CallTool callTool = JsonSerializer.Deserialize<CallTool>(
            rawToolCall,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
        string parameters = JsonSerializer.Serialize(callTool.Parameters); 
        string? response = await ToolInvoker.Invoke(await _toolQueryActions.FindToolByName(callTool.Name), new StringContent(parameters, Encoding.UTF8, "application/json"));
        return response;
    }
}
