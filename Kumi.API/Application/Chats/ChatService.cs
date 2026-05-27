using Kumi.API.Application.Chats.Dtos;
using Kumi.Core.Chats;

namespace Kumi.API.Application.Chats
{
    public class ChatService(Chat chat)
    {
        public async Task<Result<List<ChatMessageDto>>> Chat(ChatMessageDto prompt)
        {
            List<ChatMessageDto> messages = new();
            messages.Add(prompt);
            try
            {                
                messages.Add(new ChatMessageDto
                {
                    Type = "RESPONSE",
                    Content = await chat.PromptAgent(prompt.Content)
                });

                return Result<List<ChatMessageDto>>.Success(messages);
            } catch(Exception ex)
            {
                if (ex is LanguageModelNotConfiguredException || ex is LanguageModelConfigurationErrorException)
                { 
                    return Result<List<ChatMessageDto>>.Failure(404);
                }
                return Result<List<ChatMessageDto>>.Failure(500);
            } 

        }
    }
}
