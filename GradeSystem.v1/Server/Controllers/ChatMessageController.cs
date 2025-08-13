using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ChatMessageController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet("{senderID}/{receiverID}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages(int senderID,int receiverID)
        {
            return await _context.ChatMessage.Include(c => c.Sender).Include(r => r.Receiver).Where(m=>(m.SenderID==senderID&&m.ReceiverID==receiverID)||(m.SenderID==receiverID && m.ReceiverID==senderID)).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<ChatMessage>> PostChatMessage(ChatMessage chatMessage)
        {
            if (chatMessage == null)
            {
                return BadRequest("Chat message cannot be null.");
            }
            _context.ChatMessage.Add(chatMessage);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetChatMessage", new { id = chatMessage.ID }, chatMessage);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(int id)
        {
            return await _context.ChatMessage.Include(c => c.Sender).Include(r => r.Receiver).FirstOrDefaultAsync(m => m.ID == id);
        }
    }
}
