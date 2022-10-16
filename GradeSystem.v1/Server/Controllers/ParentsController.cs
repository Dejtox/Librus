using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradeSystem.v1.Server.Data;
using System.Runtime.CompilerServices;

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
        public async Task<ActionResult<List<Parent>>> GetParent()
        {
            return await _context.Parent.ToListAsync();
        }

        // GET: api/Parents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parent>> GetParent(int id)
        {
            var parent = await _context.Parent.FindAsync(id);

            if (parent == null)
            {
                return NotFound();
            }

            return parent;
        }

        // PUT: api/Parents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Parent>>> PutParent( Parent parent, int id)
        {
            var dbParent = await _context.Parent.FirstOrDefaultAsync(p => p.ParentID == id);
            if (dbParent == null)
            {
                return NotFound("Sory no Parent...");
            }
            dbParent.FirstName=parent.FirstName;
            dbParent.LastName=parent.LastName;
            dbParent.Email=parent.Email;
            dbParent.PhoneNumber=parent.PhoneNumber;
            dbParent.Login = parent.Login;
            dbParent.Password = parent.Password;
            await _context.SaveChangesAsync();
            return Ok(await GetAllHeroes());
        }

        // POST: api/Parents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Parent>>> PostParent(Parent parent)
        {
 
            _context.Parent.Add(parent);
            await _context.SaveChangesAsync();

            return Ok(await GetAllHeroes());
        }

        // DELETE: api/Parents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var dbParent = await _context.Parent.FirstOrDefaultAsync(p => p.ParentID == id);
            if (dbParent == null)
            {
                return NotFound("Sory no Parent...");
            }

            _context.Parent.Remove(dbParent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParentExists(int id)
        {
            return _context.Parent.Any(e => e.ParentID == id);
        }
        private async Task<List<Parent>> GetAllHeroes()
        {
            return await _context.Parent.ToListAsync();
        }
    }
}
