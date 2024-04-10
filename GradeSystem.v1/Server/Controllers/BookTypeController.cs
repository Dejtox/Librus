using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;
using GradeSystem.v1.Shared;
using static System.Reflection.Metadata.BlobBuilder;
using Smart.Blazor;


namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTypeController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public BookTypeController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookType>>> GetBookType()
        {
            return await _context.BookType.Include(b => b.Books).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookType>> GetBookTypeById(int id)
        {
            var BookType = await _context.BookType.Include(b => b.Books).FirstOrDefaultAsync(b => b.BookTypeID == id);
            if (BookType == null)
            {
                return NotFound();
            }

            return BookType;
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> PutBookType( int id, BookType booktype)
        {
            if (id != booktype.BookTypeID)
            {
                return BadRequest();
            }
            _context.Entry(booktype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookTypeExists(id))
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
        [ActionName(nameof(PostBookType))]
        public async Task<ActionResult<Book>> PostBookType(BookType booktype)
        {
            booktype.Books = new List<Book>();
            booktype.BookIds = new List<int>();
            _context.BookType.Add(booktype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = booktype.BookTypeID }, booktype);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookType(int BookTypeID)
        {
            var dbBookType = await _context.Book.FirstOrDefaultAsync(b => b.BookTypeID == BookTypeID);
            if (dbBookType == null)
            {
                return NotFound("Sorry no Book...");
            }
            else
            {

                _context.Book.Remove(dbBookType);
                await _context.SaveChangesAsync();
            }


            return Ok();
        }
        private bool BookTypeExists(int id)
        {
            return _context.BookType.Any(e => e.BookTypeID == id);
        }

    }



}

