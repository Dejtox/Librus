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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SyllabusController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public SyllabusController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Syllabus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Syllabus>>> GetSyllabus()
        {
            return await _context.Syllabus.ToListAsync();
        }

        // GET: api/Syllabus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Syllabus>> GetSyllabus(int id)
        {
            var @sylab = await _context.Syllabus
                .FirstOrDefaultAsync(c=>c.SyllabusID == id);

            if (@sylab == null)
            {
                return NotFound();
            }

            return @sylab;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Syllabus @sylab)
        {
            if (id != @sylab.SyllabusID)
            {
                return BadRequest();
            }

            _context.Entry(@sylab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SyllabusExists(id))
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

        // POST: api/Syllabus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Syllabus>> PostSyllabus(Syllabus @sylab)
        {
            _context.Syllabus.Add(@sylab);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @sylab.SyllabusID }, @sylab);
        }

        // DELETE: api/Syllabus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSyllabus(int id)
        {
            var @sylab = await _context.Syllabus.FindAsync(id);
            if (@sylab == null)
            {
                return NotFound();
            }

            _context.Syllabus.Remove(@sylab);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SyllabusExists(int id)
        {
            return _context.Syllabus.Any(e => e.SyllabusID == id);
        }


    }
}
