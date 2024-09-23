using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTypeController : Controller
    {
        private readonly GradeSystemv1ServerContext _context;
        public GradeTypeController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeType>>> GetGradeTypes()
        {
            return await _context.GradeType.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeType>> GetGradeTypeById(int id)
        {
            var GradeType = await _context.GradeType.FirstOrDefaultAsync(b => b.GradeTypeId == id);
            if (GradeType == null)
            {
                return NotFound();
            }

            return GradeType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradeType(int id, GradeType gradetype)
        {
            if (id != gradetype.GradeTypeId)
            {
                return BadRequest();
            }

            _context.Entry(gradetype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeTypeExists(id))
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
        public async Task<ActionResult<Class>> PostGradeType(GradeType gradetype)
        {
            _context.GradeType.Add(gradetype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradeNumber", new { id = gradetype.GradeTypeId }, gradetype);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGradeType(int id)
        {
            var gradetype = await _context.GradeType.FindAsync(id);
            if (gradetype == null)
            {
                return NotFound();
            }

            _context.GradeType.Remove(gradetype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeTypeExists(int id)
        {
            return _context.GradeType.Any(e => e.GradeTypeId == id);
        }
    }
}
