
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.ChatMessageService
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly HttpClient _http;
        public ChatMessageService(HttpClient http)
        {
            _http = http;
        }
        public async Task CreateChatMessage(ChatMessage chatMessage)
        {
            await _http.PostAsJsonAsync("api/ChatMessage", chatMessage);
        }

        public async Task<List<ChatMessage>> GetChatMessages(int senderID, int receiverID)
        {
            var result= await _http.GetFromJsonAsync<List<ChatMessage>>($"api/ChatMessage/{senderID}/{receiverID}");
            if (result==null)
            {
                throw new Exception("No messages");
            }
            return result;
        }
    }
}
