using Kumi.API.Application.Chats;
using Kumi.API.Application.Chats.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kumi.API.Apis
{
    public class ChatController(ChatService chatService) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<List<ChatMessageDto>>> Chat(ChatMessageDto chatMessage)
        {
            return HandleResult(await chatService.Chat(chatMessage));
        }
    }
}
