using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ExamController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            return await _context.Exam.Include(e => e.Subject).Include(e => e.Class).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.Exam.Include(e => e.Subject).Include(e => e.Class).FirstOrDefaultAsync(e => e.ExamID == id);
            if (exam == null)
            {
                return NotFound();
            }
            return exam;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (id != exam.ExamID)
            {
                return BadRequest();
            }
            _context.Entry(exam).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exam.Add(exam);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetExam", new { id = exam.ExamID }, exam);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exam.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            _context.Exam.Remove(exam);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ExamExists(int id)
        {
            return _context.Exam.Any(e => e.ExamID == id);
        }
    }
}
