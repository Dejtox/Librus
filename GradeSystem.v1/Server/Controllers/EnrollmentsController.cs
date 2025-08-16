using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradeSystem.v1.Server.Data;
using System.Transactions;
using Microsoft.AspNetCore.SignalR;



namespace GradeSystem.v1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public EnrollmentsController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollment()
        {
            return await _context.Enrollment.Include(c => c.Class).Include(s => s.Subject).Include(t => t.Subject.Teacher).ToListAsync();
        }
        [HttpGet("without_duplicates")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentWithoutDuplicates()
        {
            var duplicates = await _context.Enrollment.Where(e => e.SubEnrollmentID != null).Select(e => e.SubEnrollmentID).ToListAsync();
            return await _context.Enrollment.Include(c => c.Class).Include(s => s.Subject).Include(t => t.Subject.Teacher).Include(e => e.SubEnrollment).Where(e => !duplicates.Contains(e.EnrollmentID)).ToListAsync();
        }
        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollment.Include(c => c.Class).Include(s => s.Subject).Include(t => t.Subject.Teacher).FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }
        //[HttpGet("get_enrollments_by_teacherid")]
        //public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentByTeacherID([FromQuery]int id, [FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
        //{

        //}

        // PUT: api/Enrollments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return BadRequest();
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
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

        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Zmianson
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            enrollment.Class = null;
            enrollment.Subject = null;
            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEnrollment", new { id = enrollment.EnrollmentID }, enrollment);
        }
        [HttpPost("create_many")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> PostManyEnrollments(IEnumerable<Enrollment> enrollments)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var enrollment in enrollments)
                    {
                        enrollment.Class = null;
                        enrollment.Subject = null;
                        _context.Enrollment.Add(enrollment);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(ex.Message);
                }
            }
            return Ok(enrollments);
            //_context.Enrollment.AddRange(enrollments);
            //await _context.SaveChangesAsync();
            //return Ok();
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentID == id);
        }

        [HttpGet("get_enrollments_by_classid")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentsByClassID([FromQuery]int classID)
        {
            return await _context.Enrollment.Include(s=>s.Subject).Where(cc=>cc.ClassID == classID).ToListAsync();
        }
    }
}
