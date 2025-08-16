using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public SchoolController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        // GET: api/School
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchool()
        {
            return await _context.School.Include(p => p.Principal).ToListAsync();
        }

        // GET: api/School/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.School.Include(p => p.Principal).FirstOrDefaultAsync(s => s.ID == id);

            if (school == null)
            {
                return NotFound();
            }
            return school;
        }
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            _context.School.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.ID }, school);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.School.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.ID)
            {
                return BadRequest();
            }

            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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
        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.ID == id);
        }
        [HttpGet("access_code")]
        public async Task<ActionResult<IEnumerable<AccessCode>>> GetAccessCodes()
        {
            return await _context.AccessCode.ToListAsync();
        }
        [HttpGet("access_code/{id}")]
        public async Task<ActionResult<AccessCode>> GetAccessCode(int id)
        {
            var accessCode = await _context.AccessCode.FindAsync(id);
            if (accessCode == null)
            {
                return NotFound();
            }
            return accessCode;
        }
        [HttpPost("access_code")]
        public async Task<ActionResult<AccessCode>> PostAccessCode(AccessCode accessCode)
        {
            _context.AccessCode.Add(accessCode);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAccessCode", new { id = accessCode.ID }, accessCode);
        }
        [HttpPut("use_access_code/{accessCode}")]
        public async Task<IActionResult> UseAccessCode(string accessCode)
        {
            var code = await _context.AccessCode.FirstOrDefaultAsync(c => (c.Code == accessCode)&&(c.IsUsed==false));//jeszcze data 
            if (code == null)
            {
                return NotFound();
            }
            //code.IsUsed = true;
            _context.Entry(code).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("access_code/{id}")]
        public async Task<IActionResult> PutAccessCode(int id, AccessCode accessCode)
        {
            if (id != accessCode.ID)
            {
                return BadRequest();
            }
            _context.Entry(accessCode).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessCodeExists(id))
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
        [HttpDelete("access_code/{id}")]
        public async Task<IActionResult> DeleteAccessCode(int id)
        {
            var accessCode = await _context.AccessCode.FindAsync(id);
            if (accessCode == null)
            {
                return NotFound();
            }
            _context.AccessCode.Remove(accessCode);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool AccessCodeExists(int id)
        {
            return _context.AccessCode.Any(e => e.ID == id);
        }

        [HttpPost("create_dayoff")]
        public async Task<ActionResult<DayOff>> CreateDayOff(DayOff dayOff)
        {
            _context.DayOff.Add(dayOff);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDayOff", new { id = dayOff.DayOffID }, dayOff);
        }
        [HttpGet("get_dayoff/{id}")]
        public async Task<ActionResult<DayOff>> GetDayOff(int id)
        {
            var dayOff = await _context.DayOff.FindAsync(id);
            if (dayOff == null)
            {
                return NotFound();
            }
            return dayOff;
        }

        [HttpPost("create_many_dayoffs")]
        public async Task<ActionResult<IEnumerable<DayOff>>> CreateManyDayOffs(IEnumerable<DayOff> dayOffs)
        {
            //albo transaakcja
            _context.DayOff.AddRange(dayOffs);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDayOffs", dayOffs);
        }
        [HttpGet("get_dayoffs")]
        public async Task<ActionResult<IEnumerable<DayOff>>> GetDayOffs()
        {
            return await _context.DayOff.ToListAsync();
        }
    }
}
