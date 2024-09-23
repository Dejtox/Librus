using GradeSystem.v1.Client.Pages;
using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GradeSystem.v1.Server.Controllers
{
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

        //usuniecie wycieczki oraz nieaktwynych zastepstw
        //Do obgadania tak czy siak dodany jest schooltripID jeszcze bedzie trzeba sprawdzic currrent date etc
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip.FindAsync(id);
            if (schoolTrip == null)
                return NotFound();
            _context.Remove(schoolTrip);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolTrip>> GetSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip.Include(g => g.Guardians).Include(c => c.Classes).Include(s => s.Students).FirstOrDefaultAsync(st => st.SchoolTripID == id);

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
            //await _context.SaveChangesAsync();
            //var (startDate, endDate) = (schoolTrip.StartDate, schoolTrip.EndDate);
            //var teacherIds = schoolTripCombined.Teachers.Select(t => t.TeacherID).ToList();
            //var classIds = schoolTripCombined.Classes.Select(c => c.ClassID).ToList();
            //var inactiveSubstitutes = await _context.Enrollment
            //    .Include(e => e.Subject)
            //    .Include(e => e.Class)
            //    .Where(e => (classIds.Contains(e.ClassID) || (classIds.Contains(e.ClassID) && teacherIds.Contains(e.Subject.TeacherID))) &&
            //                (((e.Date >= startDate && e.Date <= endDate) || (e.EndDate >= startDate && e.EndDate <= endDate))))
            //    .ToListAsync();
            //var needActionSubstitutes = await _context.Enrollment
            //    .Include(e => e.Subject)
            //    .Include(e => e.Class)
            //    .Where(e => (teacherIds.Contains(e.Subject.TeacherID) && !classIds.Contains(e.ClassID)) &&
            //                (((e.Date >= startDate && e.Date <= endDate) || (e.EndDate >= startDate && e.EndDate <= endDate))))
            //    .ToListAsync();

            //foreach (var substitute in inactiveSubstitutes)
            //{
            //    substitute.Status = "inactive";
            //    _context.Entry(substitute).State = EntityState.Modified;
            //}
            //foreach (var substitute in needActionSubstitutes)
            //{
            //    substitute.Status = "need action";
            //    _context.Entry(substitute).State = EntityState.Modified;
            //}
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSchoolTrip", new { id = schoolTrip.SchoolTripID }, schoolTrip);
        }
        //    [HttpPost("{id}")]
        //    public async Task<IActionResult> PutSchoolTrip(int id, SchoolTripCombined schoolTripCombined)
        //    {
        //        var oldSchoolTrip = await _context.SchoolTrip.Include(c => c.SchoolTripClasses).Include(t => t.SchoolTripTeachers).FirstOrDefaultAsync(s => s.SchoolTripID == id);
        //        if (oldSchoolTrip == null) { return NotFound(); }
        //        var oldClassIds = oldSchoolTrip.SchoolTripClasses.Select(s => s.ClassID).ToList();
        //        _context.SchoolTripTeachers.RemoveRange(oldSchoolTrip.SchoolTripTeachers);
        //        _context.SchoolTripClasses.RemoveRange(oldSchoolTrip.SchoolTripClasses);
        //        await _context.SaveChangesAsync();
        //        var (oldStartDate, oldEndDate) = (oldSchoolTrip.StartDate, oldSchoolTrip.EndDate);

        //        var oldInactiveSubstitutes = await _context.Enrollment
        //            .Include(e => e.Subject)
        //            .Include(e => e.Class)
        //            .Where(s => (oldClassIds.Contains(s.ClassID)) && (s.Status != "active") &&
        //                        ((s.Date >= oldStartDate && s.Date <= oldEndDate) || (s.EndDate >= oldStartDate && s.EndDate <= oldEndDate)))
        //            .ToListAsync();
        //        foreach (var s in oldInactiveSubstitutes)
        //        {
        //            s.Status = "active";
        //            _context.Entry(s).State = EntityState.Modified;
        //        }
        //        await _context.SaveChangesAsync();
        //        SchoolTrip schoolTrip = schoolTripCombined.SchoolTrip;
        //        var schoolTripTeachers = schoolTripCombined.Teachers.Select(t => new SchoolTripTeachers { SchoolTrip = schoolTrip, TeacherID = t.TeacherID }).ToList();
        //        var schoolTripClasses = schoolTripCombined.Classes.Select(c => new SchoolTripClasses { SchoolTrip = schoolTrip, ClassID = c.ClassID }).ToList();
        //        oldSchoolTrip.Name = schoolTrip.Name;
        //        oldSchoolTrip.StartDate = schoolTrip.StartDate;
        //        oldSchoolTrip.EndDate = schoolTrip.EndDate;
        //        oldSchoolTrip.Description = schoolTrip.Description;
        //        oldSchoolTrip.SchoolTripTeachers = schoolTripTeachers;
        //        oldSchoolTrip.SchoolTripClasses = schoolTripClasses;
        //        await _context.SaveChangesAsync();
        //        //post
        //        var (startDate, endDate) = (schoolTrip.StartDate, schoolTrip.EndDate);
        //        var teacherIds = schoolTripCombined.Teachers.Select(t => t.TeacherID).ToList();
        //        var classIds = schoolTripCombined.Classes.Select(c => c.ClassID).ToList();
        //        var inactiveSubstitutes = await _context.Enrollment
        //            .Include(e => e.Subject)
        //            .Include(e => e.Class)
        //            .Where(e => (classIds.Contains(e.ClassID) || (classIds.Contains(e.ClassID) && teacherIds.Contains(e.Subject.TeacherID))) &&
        //                        (((e.Date >= startDate && e.Date <= endDate) || (e.EndDate >= startDate && e.EndDate <= endDate))))
        //            .ToListAsync();
        //        var needActionSubstitutes = await _context.Enrollment
        //            .Include(e => e.Subject)
        //            .Include(e => e.Class)
        //            .Where(e => (teacherIds.Contains(e.Subject.TeacherID) && !classIds.Contains(e.ClassID)) &&
        //                        (((e.Date >= startDate && e.Date <= endDate) || (e.EndDate >= startDate && e.EndDate <= endDate))))
        //            .ToListAsync();

        //        foreach (var substitute in inactiveSubstitutes)
        //        {
        //            substitute.Status = "inactive";
        //            _context.Entry(substitute).State = EntityState.Modified;
        //        }
        //        foreach (var substitute in needActionSubstitutes)
        //        {
        //            substitute.Status = "need action";
        //            _context.Entry(substitute).State = EntityState.Modified;
        //        }
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
    }
}
