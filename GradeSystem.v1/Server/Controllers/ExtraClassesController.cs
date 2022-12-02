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
    public class ExtraClassesController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ExtraClassesController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/ExtraClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraClasses>>> GetExtraClasses()
        {
            return await _context.ExtraClasses.Include(t=>t.Teacher).ToListAsync();
        }

        // GET: api/ExtraClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraClasses>> GetExtraClasses(int id)
        {
            var extraClasses = await _context.ExtraClasses.Include(t => t.Teacher).FirstOrDefaultAsync(e=>e.ExtraClassesID==id);

            if (extraClasses == null)
            {
                return NotFound();
            }

            return extraClasses;
        }

        // PUT: api/ExtraClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExtraClasses(int id, ExtraClasses extraClasses)
        {
            if (id != extraClasses.ExtraClassesID)
            {
                return BadRequest();
            }

            _context.Entry(extraClasses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtraClassesExists(id))
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

        // POST: api/ExtraClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExtraClasses>> PostExtraClasses(ExtraClasses extraClasses)
        {
            extraClasses.Teacher = null;
            _context.ExtraClasses.Add(extraClasses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExtraClasses", new { id = extraClasses.ExtraClassesID }, extraClasses);
        }

        // DELETE: api/ExtraClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtraClasses(int id)
        {
            var extraClasses = await _context.ExtraClasses.FindAsync(id);
            if (extraClasses == null)
            {
                return NotFound();
            }

            _context.ExtraClasses.Remove(extraClasses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExtraClassesExists(int id)
        {
            return _context.ExtraClasses.Any(e => e.ExtraClassesID == id);
        }
    }
}
