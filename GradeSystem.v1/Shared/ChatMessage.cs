public class ChatMessage
{
    public int ID { get; set; }
    public int SenderID { get; set; }
    public User? Sender { get; set; }
    public int ReceiverID { get; set; } 
    public User? Receiver { get; set; }
    public string Content { get; set; }       
    public DateTime SentAt { get; set; }     
}

