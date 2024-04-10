using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradenumberController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public GradenumberController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeNumber>>> GetGradeNumbers()
        {
            return await _context.GradeNumber.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeNumber>> GetGradeNumberById(int id)
        {
            var GradeNumber = await _context.GradeNumber.FirstOrDefaultAsync(b => b.GradeNumberID == id);
            if (GradeNumber == null)
            {
                return NotFound();
            }

            return GradeNumber;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradeNumber(int id, GradeNumber gradenumber)
        {
            if (id != gradenumber.GradeNumberID)
            {
                return BadRequest();
            }

            _context.Entry(gradenumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeNumberExists(id))
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
        public async Task<ActionResult<Class>> PostGradeNumber(GradeNumber gradenumber)
        {
            _context.GradeNumber.Add(gradenumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradeNumber", new { id = gradenumber.GradeNumberID }, gradenumber);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGradeNumber(int id)
        {
            var gradenumber = await _context.GradeNumber.FindAsync(id);
            if (gradenumber == null)
            {
                return NotFound();
            }

            _context.GradeNumber.Remove(gradenumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeNumberExists(int id)
        {
            return _context.GradeNumber.Any(e => e.GradeNumberID == id);
        }
    }
}
