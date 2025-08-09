namespace GradeSystem.v1.Client.Services.ChatMessageService
{
    public interface IChatMessageService
    {
        Task CreateChatMessage(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetChatMessages(int senderID, int receiverID);
    }
}
