using GradeSystem.v1.Client.Pages;
using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GradeSystem.v1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolTripController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;

        public SchoolTripController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolTrip>>> GetSchoolTrip()
        {
            return await _context.SchoolTrip.Include(c=>c.Classes).ThenInclude(cc=>cc.Class).Include(s=>s.Students).Include(t=>t.TripLeader).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip
                    .Include(st => st.Classes)
                    .Include(st => st.Students)
                    .Include(st => st.Guardians)
                    .FirstOrDefaultAsync(st => st.SchoolTripID == id);
            if (schoolTrip != null)
            {
                _context.SchoolTripClasses.RemoveRange(schoolTrip.Classes);
                _context.SchoolTripStudents.RemoveRange(schoolTrip.Students);
                _context.Guardians.RemoveRange(schoolTrip.Guardians);
                _context.SchoolTrip.Remove(schoolTrip);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolTrip>> GetSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip.Include(g => g.Guardians).Include(c => c.Classes).ThenInclude(cc=>cc.Class).Include(s => s.Students).ThenInclude(ss=>ss.Student).Include(tl=>tl.TripLeader).FirstOrDefaultAsync(st => st.SchoolTripID == id);

            if (schoolTrip == null)
            {
                return NotFound();
            }

            return schoolTrip;
        }


        [HttpPost]
        public async Task<ActionResult<SchoolTrip>> PostSchoolTrip(SchoolTrip schoolTrip)
        {
            _context.SchoolTrip.Add(schoolTrip);
            await _context.SaveChangesAsync();
            await AddSubstitution(schoolTrip.SchoolTripID);
            return CreatedAtAction("GetSchoolTrip", new { id = schoolTrip.SchoolTripID }, schoolTrip);
        }
        private async Task AddSubstitution(int id)
        {
            var schoolTrip=await _context.SchoolTrip.Include(tl=>tl.TripLeader).Include(g=>g.Guardians).ThenInclude(t=>t.Teacher).FirstOrDefaultAsync(st=>st.SchoolTripID==id);
            var teachers=schoolTrip.Guardians.Select(t=>t.Teacher).ToList();
            teachers.Add(schoolTrip.TripLeader);
            var startDate = schoolTrip.StartDate;
            var endDate = schoolTrip.EndDate;
            foreach (var teacher in teachers) 
            {
                teacher.StartDate=startDate;
                teacher.EndDate=endDate;
                _context.Entry(teacher).State=EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
        private async Task DeleteSubsitution(int id)
        {
            var schoolTrip = await _context.SchoolTrip.Include(tl => tl.TripLeader).Include(g => g.Guardians).ThenInclude(t => t.Teacher).FirstOrDefaultAsync(st => st.SchoolTripID == id);
            var teachers = schoolTrip.Guardians.Select(t => t.Teacher).ToList();
            teachers.Add(schoolTrip.TripLeader);
            foreach (var teacher in teachers)
            {
                teacher.StartDate = null;
                teacher.EndDate = null;
                _context.Entry(teacher).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            foreach (var teacher in teachers)
            {
                _context.Entry(teacher).State = EntityState.Detached;
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> PutSchoolTrip(int id, SchoolTrip schoolTrip)
        {
            await DeleteSubsitution(id);
            var oldSchoolTrip=await _context.SchoolTrip.Include(c=>c.Classes).Include(s=>s.Students).Include(g=>g.Guardians).FirstOrDefaultAsync(t => t.SchoolTripID == id);
            _context.SchoolTripClasses.RemoveRange(oldSchoolTrip.Classes);
            _context.SchoolTripStudents.RemoveRange(oldSchoolTrip.Students);
            _context.Guardians.RemoveRange(oldSchoolTrip.Guardians);
            _context.Entry(oldSchoolTrip).State = EntityState.Detached;
            _context.SchoolTripClasses.AddRange(schoolTrip.Classes);
            _context.SchoolTripStudents.AddRange(schoolTrip.Students);
            _context.Guardians.AddRange(schoolTrip.Guardians);
            _context.Entry(schoolTrip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await AddSubstitution(id);
            return NoContent();
        }
    }
}
