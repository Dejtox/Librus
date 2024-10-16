﻿using Microsoft.AspNetCore.Mvc;
using GradeSystem.v1.Server.Data;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public ParentsController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Parents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> GetParent()
        {
            return await _context.Parent.Include(u=>u.User).ToListAsync();
        }

        // GET: api/Parents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parent>> GetParent(int id)
        {
            var parent = await _context.Parent.Include(u => u.User).FirstOrDefaultAsync(p=>p.ParentID==id);

            if (parent == null)
            {
                return NotFound();
            }

            return parent;
        }

        // PUT: api/Parents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParent( Parent parent, int id)
        {

            if (id != parent.ParentID)
            {
                return BadRequest();
            }

            _context.Entry(parent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentExists(id))
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

        // POST: api/Parents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parent>> PostParent(Parent parent)
        {
            parent.User = null;
            _context.Parent.Add(parent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParent", new { id = parent.ParentID }, parent);
        }

        // DELETE: api/Parents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var dbParent = await _context.Parent.FirstOrDefaultAsync(p => p.ParentID == id);
            if (dbParent == null)
            {
                return NotFound("Sorry no Parent...");
            }
            else
            {

                _context.Parent.Remove(dbParent);
                await _context.SaveChangesAsync();
            }
               

            return Ok();
        }

        private bool ParentExists(int id)
        {
            return _context.Parent.Any(e => e.ParentID == id);
        }
    }
}
