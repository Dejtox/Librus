using Microsoft.AspNetCore.SignalR;
using GradeSystem.v1.Client.Services.ChatMessageService;
namespace GradeSystem.v1.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            //message.SenderID = int.Parse(Context.UserIdentifier);
            //await Clients.All.SendAsync("ReceiveMessage", message); 
            //await _chatMessageService.CreateChatMessage(message);
            await Clients.User(message.ReceiverID.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
