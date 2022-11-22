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
    public class ExtraClassesListsController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ExtraClassesListsController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/ExtraClassesLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraClassesList>>> GetExtraClassesList()
        {
            return await _context.ExtraClassesList.Include(s=>s.Student).Include(e=>e.ExtraClasses).ToListAsync();
        }

        // GET: api/ExtraClassesLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraClassesList>> GetExtraClassesList(int id)
        {
            var extraClassesList = await _context.ExtraClassesList.Include(s => s.Student).Include(e => e.ExtraClasses).FirstOrDefaultAsync(ecl => ecl.ExtraClassesListID == id);

            if (extraClassesList == null)
            {
                return NotFound();
            }

            return extraClassesList;
        }

        // PUT: api/ExtraClassesLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExtraClassesList(int id, ExtraClassesList extraClassesList)
        {
            if (id != extraClassesList.ExtraClassesListID)
            {
                return BadRequest();
            }

            _context.Entry(extraClassesList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtraClassesListExists(id))
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

        // POST: api/ExtraClassesLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExtraClassesList>> PostExtraClassesList(ExtraClassesList extraClassesList)
        {
            extraClassesList.Student = null;
            extraClassesList.ExtraClasses = null;

            _context.ExtraClassesList.Add(extraClassesList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExtraClassesList", new { id = extraClassesList.ExtraClassesListID }, extraClassesList);
        }

        // DELETE: api/ExtraClassesLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtraClassesList(int id)
        {
            var extraClassesList = await _context.ExtraClassesList.FindAsync(id);
            if (extraClassesList == null)
            {
                return NotFound();
            }

            _context.ExtraClassesList.Remove(extraClassesList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExtraClassesListExists(int id)
        {
            return _context.ExtraClassesList.Any(e => e.ExtraClassesListID == id);
        }
    }
}
