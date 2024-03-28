using static System.Net.WebRequestMethods;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.BookService
{
    public interface IBookService
    {
        IList<Book> Books { get; set; }
        IList<Student> Students { get; set; }

        Book LastBookAdded { get; set; }

        Task<Book> GetBookByQR(int QR);
        Task GetStudents();
        Task GetBooks();
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);

        Task GetLastBookAdded();

    }
}
