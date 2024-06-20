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
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetSubstitute()
        {
            return await _context.Enrollment.Include(c => c.Class).Include(s => s.Subject).ThenInclude(ss => ss.Teacher).Where(e => e.Status != "active").ToListAsync();
        }

        [HttpPut("leave_add/{id}")]
        public async Task<IActionResult> PostLeave(int id, Teacher teacher)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var (startDate, endDate) = (teacher.StartDate, teacher.EndDate);
                    var oldTeacher = await _context.Teacher.FindAsync(id);
                    var (oldStartDate, oldEndDate) = (oldTeacher.StartDate, oldTeacher.EndDate);
                    oldTeacher.StartDate = teacher.StartDate;
                    oldTeacher.EndDate = teacher.EndDate;
                    oldTeacher.Status = "unavailable";
                    await _context.SaveChangesAsync();

                    var oldEnrollments = await _context.Enrollment.Include(s => s.Subject).ThenInclude(t => t.Teacher).
                        Where(e => ((e.Date >= oldStartDate && e.Date <= oldEndDate) || (e.EndDate >= oldStartDate && e.EndDate <= oldEndDate)) && e.Subject.TeacherID == id && e.Status != "active").ToListAsync();

                    foreach (var enrollment in oldEnrollments)
                    {
                        enrollment.Status = "active";
                        _context.Entry(enrollment).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();

                    var enrollments = await _context.Enrollment.Include(s => s.Subject).ThenInclude(t => t.Teacher).
                        Where(e => ((e.Date >= startDate && e.Date <= endDate) || (e.EndDate >= startDate && e.EndDate <= endDate)) && e.Subject.TeacherID == id && e.Status == "active").ToListAsync();

                    foreach (var enrollment in enrollments)
                    {
                        enrollment.Status = "need action";
                        _context.Entry(enrollment).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();

                    //var schooltrips=await _context.SchoolTrip.Include(stt=>stt.SchoolTripTeachers)
                    //    .Where(st=>st.SchoolTripTeachers.Any(t=>t.TeacherID==teacher.TeacherID) && (st.StartDate>=startDate && st.EndDate<=endDate)).ToListAsync();

                    var schooltrips = await _context.SchoolTripTeachers.Include(st => st.SchoolTrip).
                        Where(stt => ((stt.SchoolTrip.StartDate >= startDate && stt.SchoolTrip.StartDate <= endDate) || (stt.SchoolTrip.EndDate >= startDate && stt.SchoolTrip.EndDate <= endDate)) && stt.TeacherID == id).ToListAsync();

                    _context.SchoolTripTeachers.RemoveRange(schooltrips);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest($"Error:{ex.Message}");
                }
            }
        }

        //Pobiera dostępnych nauczycieli, lekkie spaghetti wyszło, ale cóż. Sprawdza czy w danym odstepie czasowym nauczyciel nie ma lekcji,
        //zastepstwa i nie bierze udzialu w wycieczce. Jesli nie bierze to mozna go przypisac do zastepstwa.
        [HttpGet("available_teachers/{startDate}/{endDate}")]
        public async Task<ActionResult<List<Subject>>> GetAvailableTeachers(DateTime startDate, DateTime endDate)
        {
            return await _context.Subject.Include(t => t.Teacher)
                .Where(s => !_context.Enrollment
                .Any(e => (s.Teacher.Status == "available") && (e.Subject.Teacher.TeacherID == s.TeacherID) &&
                (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate))).
                Where(s => !_context.SchoolTripTeachers
                .Any(st => (s.Teacher.Status == "available") && (st.TeacherID == s.TeacherID) &&
                (st.SchoolTrip.StartDate >= startDate && st.SchoolTrip.EndDate <= endDate || st.SchoolTrip.EndDate >= startDate && st.SchoolTrip.StartDate <= endDate))).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubstitute(int id)
        {
            var substitute = await _context.Enrollment.FindAsync(id);
            if (substitute == null)
                return NotFound();

            _context.Enrollment.Remove(substitute);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //do poprawy
        [HttpPut("leave/{id}")]
        public async Task<IActionResult> DeleteLeave(int id, Teacher teacher)
        {

            if (teacher.TeacherID != id) return BadRequest();
            teacher.StartDate = null;
            teacher.EndDate = null;
            teacher.Status = "available";
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetSubstituteByID(int id)
        {
            var substitute = await _context.Enrollment.Include(s => s.Subject).Include(c => c.Class).FirstOrDefaultAsync(s => s.EnrollmentID == id);
            if (substitute == null)
                return NotFound();
            return substitute;
        }

        //Kradzione od was to nie opisuje
        [HttpPut("{id}")]
        public async Task<IActionResult> PostSubstitute(int id, Enrollment enrollment)
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
                if (!SubstituteExists(id))
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

        private bool SubstituteExists(int id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentID == id);
        }
    }
}
