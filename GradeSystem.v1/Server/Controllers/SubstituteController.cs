using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<Substitute>>> GetSubstitute()
        {
            return await _context.Substitute.Include(c => c.Class).Include(s => s.Subject).ThenInclude(ss => ss.Teacher).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Substitute>> PostSubstitute(Substitute substitute)
        {
            _context.Substitute.Add(substitute);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSubstitute", new { id = substitute.SubstituteID }, substitute);
        }

        //Pobiera dostępnych nauczycieli, lekkie spaghetti wyszło, ale cóż. Sprawdza czy w danym odstepie czasowym nauczyciel nie ma lekcji,
        //zastepstwa i nie bierze udzialu w wycieczce. Jesli nie bierze to mozna go przypisac do zastepstwa.
        // Jesli bedzie to zalosnie wolno dzialac przy wiekszej ilosci rekordow to bedzie trzeba jednak pokombinowac z tymi procedurami, ew 
        //cos innego niz linq.
        [HttpGet("available_teachers/{startDate}/{endDate}")]
        public async Task<ActionResult<List<Subject>>> GetAvailableTeachers(DateTime startDate,DateTime endDate)
        {
            return await _context.Subject.Include(t => t.Teacher)
                .Where(s => !_context.Enrollment
                .Any(e => e.Subject.Teacher.TeacherID == s.TeacherID &&
                (e.Date >= startDate && e.Date <= endDate || e.EndDate >= startDate && e.EndDate <= endDate)))
                .Where(s => !_context.Substitute
                .Any(su => su.Subject.TeacherID == s.TeacherID &&
                (su.StartDate >= startDate && su.StartDate <= endDate || su.EndDate >= startDate && su.EndDate <= endDate))).
                Where(s => !_context.SchoolTripTeachers
                .Any(st => st.TeacherID == s.TeacherID &&
                (st.SchoolTrip.StartDate >= startDate && st.SchoolTrip.EndDate <= endDate || st.SchoolTrip.EndDate >= startDate && st.SchoolTrip.StartDate <= endDate))).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubstitute(int id)
        {
            var substitute = await _context.Substitute.FindAsync(id);
            if (substitute == null)
                return NotFound();

            _context.Substitute.Remove(substitute);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Substitute>> GetSubstituteByID(int id)
        {
            var substitute= await _context.Substitute.Include(s=>s.Subject).Include(c=>c.Class).FirstOrDefaultAsync(s=>s.SubstituteID==id);
            if (substitute ==null)
                return NotFound();
            return substitute;
        }

        //Kradzione od was to nie opisuje
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubstitute(int id, Substitute substitute)
        {
            if(id!=substitute.SubstituteID)
            {
                return BadRequest();
            }
            _context.Entry(substitute).State=EntityState.Modified;

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
            return _context.Substitute.Any(e => e.SubstituteID == id);
        }
    }
}
