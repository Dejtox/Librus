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

        // Transakcja dodania wycieczki
        [HttpPost]
        public async Task<ActionResult<SchoolTrip>> PostSchoolTrip(SchoolTripCombined schoolTripCombined)
        {
            //start transakcji
            using(var transaction=_context.Database.BeginTransaction())
            {
                try
                {
                    DateTime startDate = schoolTripCombined.SchoolTrip.StartDate;
                    DateTime endDate = schoolTripCombined.SchoolTrip.EndDate;

                    //przypisanie nowej wycieczki
                    var newSchoolTrip = new SchoolTrip
                    {
                        Name = schoolTripCombined.SchoolTrip.Name,
                        Description = schoolTripCombined.SchoolTrip.Description,
                        StartDate = schoolTripCombined.SchoolTrip.StartDate,
                        EndDate = schoolTripCombined.SchoolTrip.EndDate
                    };

                    //tworzenie nowych obiektów z relacją
                    var newSchoolTripTeachers = schoolTripCombined.Teachers.Select(t => new SchoolTripTeachers { SchoolTrip = newSchoolTrip, TeacherID = t.TeacherID }).ToList();
                    var newSchoolTripClasses = schoolTripCombined.Classes.Select(c => new SchoolTripClasses { SchoolTrip = newSchoolTrip, ClassID = c.ClassID }).ToList();

                    //przypisanie do obiektu
                    newSchoolTrip.SchoolTripTeachers = newSchoolTripTeachers;
                    newSchoolTrip.SchoolTripClasses = newSchoolTripClasses;
           
                    _context.SchoolTrip.Add(newSchoolTrip);
                    await _context.SaveChangesAsync();

                    //zapis w liscie ids klas i nauczycieli bioracych udzial w wycieczce
                    var teacherIds = schoolTripCombined.Teachers.Select(t => t.TeacherID).ToList();
                    var classIds = schoolTripCombined.Classes.Select(c => c.ClassID).ToList();

                    //aktualizacja statusow zastepstw, jesli dany nauczyciel bierze udzial w wycieczce
                    var updateSubstitute = await _context.Substitute
                        .Include(ss => ss.Subject)
                        .Where(s => teacherIds.Contains(s.Subject.TeacherID) &&
                                    (s.StartDate >= startDate && s.StartDate <= endDate || s.EndDate >= startDate && s.EndDate <= endDate))
                        .ToListAsync();

                    //aktualizacja rekordow
                    foreach (var substitute in updateSubstitute)
                    {
                        substitute.Status = "need action";
                        _context.Entry(substitute).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();

                    //nieaktywne zastępsta tzn lekcje, w ktorych bierze udzial klasa bedaca na wycieczce
                    //ogolnie nie powinny byc one widoczne, ale to pozniej
                    //istnieja w razie zmiany wycieczki
                    var inactiveSubstitutes = await _context.Enrollment
                        .Include(e => e.Subject)
                        .Include(e => e.Class)
                        .Where(e => (classIds.Contains(e.ClassID) || (classIds.Contains(e.ClassID)&&teacherIds.Contains(e.Subject.TeacherID))) &&
                                    (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate))
                        .ToListAsync();

                    //zastepstwa potrzebujace dzialania to takie, w ktorych trzeba przypisac nowego nauczyciela, poniewaz wczesniej przypisany znajduje sie na wyieczce
                    var needActionSubstitutes = await _context.Enrollment
                        .Include(e => e.Subject)
                        .Include(e => e.Class)
                        .Where(e => (teacherIds.Contains(e.Subject.TeacherID) && !classIds.Contains(e.ClassID)) &&
                                    (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate))
                        .ToListAsync();

                    //tworzenie obiektu
                    var newInactiveSubstitutes = inactiveSubstitutes.Select(s => new Substitute
                    {
                        StartDate = s.Date,
                        EndDate = s.EndDate,
                        SubjectID = s.SubjectID,
                        ClassRoom = s.ClassRoom,
                        ClassID = s.ClassID,
                        Status = "inactive"
                    }).ToList();

                    var newNeedActionSubstitutes = needActionSubstitutes.Select(s => new Substitute
                    {
                        StartDate = s.Date,
                        EndDate = s.EndDate,
                        SubjectID = s.SubjectID,
                        ClassRoom = s.ClassRoom,
                        ClassID = s.ClassID,
                        Status = "need action"
                    }).ToList();

                    //dodanie zastepstw
                    _context.Substitute.AddRange(newInactiveSubstitutes);
                    _context.Substitute.AddRange(newNeedActionSubstitutes);
                    await _context.SaveChangesAsync();

                    //usuniecie lekcji
                    _context.Enrollment.RemoveRange(inactiveSubstitutes);
                    _context.Enrollment.RemoveRange(needActionSubstitutes);
                    await _context.SaveChangesAsync();

                    //zatwierdzenie transakcji jesli wszystko git
                    transaction.Commit();
                    return CreatedAtAction("GetSchoolTrip", new { id = newSchoolTrip.SchoolTripID }, newSchoolTrip);
                }
                catch(Exception ex) 
                {
                    transaction.Rollback();
                    return BadRequest($"Error:{ex.Message}");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolTrip>>> GetSchoolTrip()
        {
            return await _context.SchoolTrip.Include(t => t.SchoolTripTeachers).ThenInclude(tt => tt.Teacher).Include(c => c.SchoolTripClasses).ThenInclude(cc => cc.Class).ToListAsync();
        }

        //usuniecie wycieczki oraz nieaktwynych zastepstw
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip.FindAsync(id);
            if (schoolTrip == null)
                return NotFound();

            DateTime startDate = schoolTrip.StartDate;
            DateTime endDate = schoolTrip.EndDate;

            var substitutes=await _context.Substitute.Where(s=>((s.StartDate>=startDate) && (s.EndDate>=endDate) ||(s.EndDate >= startDate && s.EndDate <= endDate)) && s.Status=="inactive").ToListAsync();

            _context.RemoveRange(substitutes);
            _context.Remove(schoolTrip);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolTrip>> GetSchoolTrip(int id)
        {
            var schoolTrip = await _context.SchoolTrip.Include(c => c.SchoolTripClasses).ThenInclude(cc => cc.Class).Include(t => t.SchoolTripTeachers).ThenInclude(tt => tt.Teacher).FirstOrDefaultAsync(st => st.SchoolTripID == id);

            if (schoolTrip == null)
            {
                return NotFound();
            }

            return schoolTrip;
        }

        //aktualizacja rekordu
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolTrip(int id, SchoolTripCombined schoolTripCombined)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //pobranie rekordu przed aktualizacja
                    var oldSchoolTrip = await _context.SchoolTrip.Include(sc => sc.SchoolTripClasses).ThenInclude(c => c.Class).ThenInclude(t => t.Teacher).FirstOrDefaultAsync(st => st.SchoolTripID == id);
                    var oldClassIds = oldSchoolTrip.SchoolTripClasses.Select(c => c.ClassID).ToList();

                    //aby wiedziec, ktore rekordy trzeba usunac
                    DateTime startDate = oldSchoolTrip.StartDate;
                    DateTime endDate = oldSchoolTrip.EndDate;

                    //pobranie listy nieaktwynych zastepstw
                    var oldInactiveSubstitutes = await _context.Substitute
                        .Include(e => e.Subject)
                        .Include(e => e.Class)
                        .Where(s => (oldClassIds.Contains(s.ClassID)) && (s.Status == "inactive") &&
                                    (s.StartDate >= startDate && s.StartDate <= endDate || s.EndDate >= startDate && s.EndDate <= endDate))
                        .ToListAsync();

                    //mozna ogolnie to zrobic w jednej operacji, ale spaghtetti potezne wychodzi
                    var enrollments = oldInactiveSubstitutes.Select(s => new Enrollment
                    {
                        Date = s.StartDate,
                        EndDate = s.EndDate,
                        SubjectID = s.SubjectID,
                        ClassRoom = s.ClassRoom,
                        ClassID = s.ClassID,
                    }).ToList();

                    //stare relacje
                    var oldSchoolTripTeachers = await _context.SchoolTripTeachers.Where(st => st.SchoolTripID == id).ToListAsync();
                    var oldSchoolTripClasses = await _context.SchoolTripClasses.Where(sc => sc.SchoolTripID == id).ToListAsync();

                    //usuniecie relacji 
                    _context.RemoveRange(oldSchoolTripTeachers);
                    _context.RemoveRange(oldSchoolTripClasses);
                    _context.RemoveRange(oldInactiveSubstitutes);

                    await _context.SaveChangesAsync();

                    //nieaktywne zastepstwa staja sie znowu zwyklymi lekcjami
                    _context.AddRange(enrollments);

                    await _context.SaveChangesAsync();

                    //klon obiektu, trzeba tak bo ef odpierdala z sledzeniem zmian obiektu
                    var newSchoolTrip = new SchoolTrip
                    {
                        Name = schoolTripCombined.SchoolTrip.Name,
                        Description = schoolTripCombined.SchoolTrip.Description,
                        StartDate = schoolTripCombined.SchoolTrip.StartDate,
                        EndDate = schoolTripCombined.SchoolTrip.EndDate
                    };

                    //jak przy tworzeniu
                    var newSchoolTripTeachers = schoolTripCombined.Teachers.Select(t => new SchoolTripTeachers { SchoolTrip = newSchoolTrip, TeacherID = t.TeacherID }).ToList();
                    var newSchoolTripClasses = schoolTripCombined.Classes.Select(c => new SchoolTripClasses { SchoolTrip = newSchoolTrip, ClassID = c.ClassID }).ToList();

                    //za pomoca klona przypisujemy wartosci staremu obiektowi 
                    oldSchoolTrip.Name = newSchoolTrip.Name;
                    oldSchoolTrip.Description = newSchoolTrip.Description;
                    oldSchoolTrip.StartDate = newSchoolTrip.StartDate;
                    oldSchoolTrip.EndDate = newSchoolTrip.EndDate;
                    oldSchoolTrip.SchoolTripTeachers = newSchoolTripTeachers;
                    oldSchoolTrip.SchoolTripClasses = newSchoolTripClasses;

                    await _context.SaveChangesAsync();
                    //nowe daty
                    startDate = newSchoolTrip.StartDate;
                    endDate = newSchoolTrip.EndDate;

                    //reszta jak przy tworzeniu
                    var teacherIds = schoolTripCombined.Teachers.Select(t => t.TeacherID).ToList();
                    var classIds = schoolTripCombined.Classes.Select(c => c.ClassID).ToList();

                    var updateSubstitute = await _context.Substitute
                        .Include(ss => ss.Subject)
                        .Where(s => teacherIds.Contains(s.Subject.TeacherID) &&
                                    (s.StartDate >= startDate && s.StartDate <= endDate || s.EndDate >= startDate && s.EndDate <= endDate))
                        .ToListAsync();

                    foreach (var substitute in updateSubstitute)
                    {
                        substitute.Status = "need action";
                        _context.Entry(substitute).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();

                    var inactiveSubstitutes = await _context.Enrollment
                        .Include(e => e.Subject)
                        .Include(e => e.Class)
                        .Where(e => (classIds.Contains(e.ClassID)) &&
                                    (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate))
                        .ToListAsync();

                    var needActionSubstitutes = await _context.Enrollment
                        .Include(e => e.Subject)
                        .Include(e => e.Class)
                        .Where(e => (teacherIds.Contains(e.Subject.TeacherID) && !classIds.Contains(e.ClassID)) &&
                                    (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate))
                        .ToListAsync();

                    var newInactiveSubstitutes = inactiveSubstitutes.Select(s => new Substitute
                    {
                        StartDate = s.Date,
                        EndDate = s.EndDate,
                        SubjectID = s.SubjectID,
                        ClassRoom = s.ClassRoom,
                        ClassID = s.ClassID,
                        Status = "inactive"
                    }).ToList();

                    var newNeedActionSubstitutes = needActionSubstitutes.Select(s => new Substitute
                    {
                        StartDate = s.Date,
                        EndDate = s.EndDate,
                        SubjectID = s.SubjectID,
                        ClassRoom = s.ClassRoom,
                        ClassID = s.ClassID,
                        Status = "need action"
                    }).ToList();

                    _context.Substitute.AddRange(newInactiveSubstitutes);
                    _context.Substitute.AddRange(newNeedActionSubstitutes);
                    await _context.SaveChangesAsync();

                    _context.Enrollment.RemoveRange(inactiveSubstitutes);
                    _context.Enrollment.RemoveRange(needActionSubstitutes);
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
    }
}
