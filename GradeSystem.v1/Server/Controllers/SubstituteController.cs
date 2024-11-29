using GradeSystem.v1.Client.Pages;
using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubstituteController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public SubstituteController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDTO>>> GetAbsentTeachers()
        {
            var teachers = await _context.Teacher.Where(t=>t.StartDate!=null).ToListAsync();
            var teachersDTO = new List<TeacherDTO>();
            foreach (var teacher in teachers)
            {
                var teacherDTO = new TeacherDTO
                {
                    ID = teacher.TeacherID,
                    Name = teacher.Name,
                    Duration = teacher.StartDate.HasValue && teacher.EndDate.HasValue ? $"{teacher.StartDate.Value:dd.MM.yyyy}-{teacher.EndDate.Value:dd.MM.yyyy}" : "Brak",
                    StartDate=teacher.StartDate,
                    EndDate=teacher.EndDate,
                    AbsentHours = await GetAbsentHours(teacher.TeacherID, teacher.StartDate, teacher.EndDate),
                    AssignHours = await GetAssignHours(teacher.TeacherID, teacher.StartDate, teacher.EndDate),
                };
                teachersDTO.Add(teacherDTO);
            }
            return Ok(teachersDTO);
        }

        private async Task<int> GetAbsentHours(int id,DateTime? startDate,DateTime? endDate)
        {
            return await _context.Enrollment.Include(s => s.Subject).CountAsync(e => ((e.Subject.TeacherID == id) && !(e.EndDate <= startDate || e.Date >= endDate)));
        }
        private async Task<int> GetAssignHours(int id, DateTime? startDate, DateTime? endDate)
        {
            return await _context.Enrollment.Include(s => s.Subject).CountAsync(e => ((e.Subject.TeacherID == id) && (e.SubEnrollmentID==null) && !(e.EndDate<=startDate || e.Date>=endDate) ));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteTeacherSubstitute(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            teacher.StartDate = null;
            teacher.EndDate = null;
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("add/{id}")]
        public async Task<IActionResult> CreateTeacherSubstitute(int id, Teacher teacher)
        {
            if (id != teacher.TeacherID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teacher.Any(e => e.TeacherID == id))
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

        [HttpGet("enrollments")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments([FromQuery] int id,[FromQuery] DateTime? startDate,[FromQuery] DateTime? endDate)
        {
            var enrollments=await _context.Enrollment.Include(se => se.SubEnrollment).ThenInclude(see => see.Subject).ThenInclude(seee => seee.Teacher).Include(c=>c.Class).Include(s=>s.Subject).ThenInclude(t=>t.Teacher).Where(e=>e.Subject.TeacherID==id && !(e.EndDate <= startDate || e.Date >= endDate)).ToListAsync();
            return enrollments;
        }

        [HttpPost("add_substitute/{enrollmentID}")]
        public async Task<IActionResult> CreateSubstitute(int enrollmentID, Enrollment substitute)
        {
            _context.Enrollment.Add(substitute);
            await _context.SaveChangesAsync();
            var enrollment=await _context.Enrollment.FindAsync(enrollmentID);
            enrollment.SubEnrollmentID=substitute.EnrollmentID;
            _context.Entry(enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
