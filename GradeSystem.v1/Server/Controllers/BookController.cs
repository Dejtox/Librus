using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public BookController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            return await _context.Book.Include(u => u.Student).Include(bt => bt.BookType).ToListAsync();
            
        }

        [HttpGet("{QRCode}")]
        public async Task<ActionResult<Book>> GetBook(int QRCode)
        {
            var Book = await _context.Book.Include(u => u.Student).Include(bt => bt.BookType).FirstOrDefaultAsync(b => b.QRCode == QRCode);

            if (Book == null)
            {
                return NotFound();
            }

            return Book;
        }
        [HttpPut("{QRCode}")]
        public async Task<IActionResult> PutBook(Book book, int QRCode)
        {

            if (QRCode != book.QRCode)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(QRCode))
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
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            book.Student = null;
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookID }, book);
        }
        [HttpDelete("{QRCode}")]
        public async Task<IActionResult> DeleteBook(int QRCode)
        {
            var dbBook = await _context.Book.FirstOrDefaultAsync(b => b.QRCode == QRCode);
            if (dbBook == null)
            {
                return NotFound("Sorry no Book...");
            }
            else
            {

                _context.Book.Remove(dbBook);
                await _context.SaveChangesAsync();
            }


            return Ok();
        }

        private bool BookExists(int QRCode)
        {
            return _context.Book.Any(e => e.QRCode == QRCode);
        }

    }
}
