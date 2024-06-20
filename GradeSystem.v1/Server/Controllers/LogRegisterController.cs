using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogRegisterController : ControllerBase
    {
        private readonly GradeSystemv1LogRegisterContext _context;
        public LogRegisterController(GradeSystemv1LogRegisterContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLog(LogRegister logRegister)
        {
            if (logRegister == null) { return BadRequest(); }
            _context.LogRegister.Add(logRegister);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
