using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradeSystem.v1.Server.Data;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ClassesController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClass()
        {
            return await _context.Class.Include(c => c.Teacher).ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @classe = await _context.Class
                .Include(c=>c.Teacher)
                .FirstOrDefaultAsync(c=>c.ClassID==id);

            if (@classe == null)
            {
                return NotFound();
            }

            return @classe;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @classe)
        {
            if (id != @classe.ClassID)
            {
                return BadRequest();
            }

            _context.Entry(@classe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @classe)
        {
            classe.Teacher = null;
            _context.Class.Add(@classe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @classe.ClassID }, @classe);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @classe = await _context.Class.FindAsync(id);
            if (@classe == null)
            {
                return NotFound();
            }

            _context.Class.Remove(@classe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.ClassID == id);
        }
    }
}
