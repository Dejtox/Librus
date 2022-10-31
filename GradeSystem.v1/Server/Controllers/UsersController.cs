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
    public class UsersController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public UsersController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.User.FirstOrDefaultAsync(u=>u.UserID==id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.UserID)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.UserID }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.User.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }
    }
}
