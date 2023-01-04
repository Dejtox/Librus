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
    public class EventsController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public EventsController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEvent>>> GetEvents()
        {
            return await _context.CalendarEvent.Include(c => c.Class).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarEvent>> GetEvent(int id)
        {
            var Event = await _context.CalendarEvent.Include(c => c.Class).FirstOrDefaultAsync(e => e.CalendarEventID == id);

            if (Event == null)
            {
                return NotFound();
            }

            return Event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, CalendarEvent Event)
        {
            if (id != Event.CalendarEventID)
            {
                return BadRequest();
            }

            _context.Entry(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalendarEvent>> PostEvent(CalendarEvent Event)
        {
            Event.Class = null;
            _context.CalendarEvent.Add(Event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = Event.CalendarEventID }, Event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var Event = await _context.CalendarEvent.FindAsync(id);
            if (Event == null)
            {
                return NotFound();
            }

            _context.CalendarEvent.Remove(Event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.CalendarEvent.Any(e => e.CalendarEventID == id);
        }
    }
}
