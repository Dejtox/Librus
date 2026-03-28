using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtracurricularActivityController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public ExtracurricularActivityController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtracurricularActivity>>> GetAllExtracurricularActivities()
        {
            return await _context.ExtracurricularActivity.Include(p => p.Participants).Include(t=>t.Teacher).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtracurricularActivity>> GetExtracurricularActivity(int id)
        {
            var activity = await _context.ExtracurricularActivity.Include(p => p.Participants).ThenInclude(ps => ps.Student).FirstOrDefaultAsync(e => e.ExtracurricularActivityID == id);
            if (activity == null)
            {
                return NotFound();
            }
            return activity;
        }
        [HttpPost]
        public async Task<ActionResult<ExtracurricularActivity>> CreateExtracurricularActivity(ExtracurricularActivity activity)
        {
            _context.ExtracurricularActivity.Add(activity);
            await _context.SaveChangesAsync();
            List<ExtraActivity> extraActivities = new List<ExtraActivity>();
            DateTime firstDate=FirstOnOrAfter(activity.StartDate, activity.DayOfWeek);
            for (DateTime d=firstDate; d < activity.EndDate; d=d.AddDays(7))
            {
                extraActivities.Add(new ExtraActivity
                {
                    ExtracurricularActivityID = activity.ExtracurricularActivityID,
                    StartDate = d.Date+activity.StartDate.TimeOfDay,
                    EndDate = d.Date+activity.EndDate.TimeOfDay
                });
            }
            _context.ExtraActivity.AddRange(extraActivities);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetExtracurricularActivity", new { id = activity.ExtracurricularActivityID }, activity);
        }
        private static DateTime FirstOnOrAfter(DateTime givenStart, DayOfWeek targetDay)
        {
            int delta = ((int)targetDay - (int)givenStart.DayOfWeek + 7) % 7;
            return givenStart.Date.AddDays(delta);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExtracurricularActivity(int id)
        {
            var activity = await _context.ExtracurricularActivity.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            _context.ExtracurricularActivity.Remove(activity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("student/{id}")]
        public async Task<ActionResult<ExtracurricularActivityStudents>> GetExtracurricularActivityStudent(int id)
        {
            var activityStudent = await _context.ExtracurricularActivityStudents.Include(s => s.Student).FirstOrDefaultAsync(e => e.ExtracurricularActivityStudentsID == id);
            if (activityStudent == null)
            {
                return NotFound();
            }
            return activityStudent;
        }
        [HttpDelete("student/{id}/{activityid}")]
        public async Task<IActionResult> DeleteExtracurricularActivityStudent(int id,int activityid)
        {
            var activityStudent = await _context.ExtracurricularActivityStudents.FirstOrDefaultAsync(s => s.ExtracurricularActivityID == activityid&& s.StudentID == id);
            if (activityStudent == null)
                return NotFound();
            bool wasMainList = !activityStudent.IsReserve;
            _context.ExtracurricularActivityStudents.Remove(activityStudent);
            
            if (wasMainList)
            {
                var reserveStudent = await _context.ExtracurricularActivityStudents.Where(s => s.ExtracurricularActivityID == activityid && s.IsReserve).OrderBy(s => s.JoinedAt).FirstOrDefaultAsync();
                if (reserveStudent != null)
                {
                    reserveStudent.IsReserve = false;
                    _context.Entry(reserveStudent).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateExtracurricularActivityStudent(int id, ExtracurricularActivityStudents activityStudent)
        {
            activityStudent.IsReserve=!activityStudent.IsReserve;
            if (id != activityStudent.ExtracurricularActivityStudentsID)
            {
                return BadRequest();
            }
            _context.Entry(activityStudent).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtracurricularActivityStudentExists(id))
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

        private bool ExtracurricularActivityStudentExists(int id)
        {
            return _context.ExtracurricularActivityStudents.Any(e => e.ExtracurricularActivityStudentsID == id);
        }

        [HttpGet("students/{id}")]
        public async Task<ActionResult<IEnumerable<ExtracurricularActivityStudents>>> GetAllExtracurricularActivityStudents(int id)
        {
            var activityStudents=await _context.ExtracurricularActivityStudents.Include(s => s.Student).Where(s => s.ExtracurricularActivityID == id).ToListAsync();
            if (activityStudents == null)
            {
                return NotFound();
            }
            return activityStudents;
        }
        [HttpPost("student")]
        public async Task<ActionResult<ExtracurricularActivityStudents>> CreateExtracurricularActivityStudent(ExtracurricularActivityStudents activityStudent)
        {
            _context.ExtracurricularActivityStudents.Add(activityStudent);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetExtracurricularActivityStudent", new { id = activityStudent.ExtracurricularActivityStudentsID }, activityStudent);
        }
        
    }
}
