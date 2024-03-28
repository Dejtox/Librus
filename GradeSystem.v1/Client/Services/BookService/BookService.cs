using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.BookService
{
    public class BookService : IBookService
    {
        public BookService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Book> Books { get; set; } = new List<Book>();

        public Book LastBookAdded { get; set; }
        public IList<Student> Students { get; set; } = new List<Student>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task<Book> GetBookByQR(int QR)
        {
            var result = await _http.GetFromJsonAsync<Book>($"api/Book/{QR}");
            var TypeService = new BookTypeService.BookTypeService(_http, _navigationManager);


            if (result != null)
            {
                BookType bookType = new BookType();
                int i  =(int) result.BookTypeID;
                if(i!=null)
                {
                    result.BookType = await TypeService.GetBookTypeByID(i);
                }    
                return result;
            }
            throw new Exception("Book Not Find");
        }

        public Task GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task GetBooks()
        {
            var TypeService = new BookTypeService.BookTypeService(_http, _navigationManager);
            var result = await _http.GetFromJsonAsync<List<Book>>("api/Book");
            if (result != null)
            {
                for(int j=0; j<result.Count ; j++)
                {

                    int i;
                    i = (int)result[j].BookTypeID;
                    result[j].BookType = await TypeService.GetBookTypeByID(i);

                }

                Books = result;
            }
        }

        public async Task CreateBook(Book book)
        {
            var result = await _http.PostAsJsonAsync("api/Book", book);
        }

        public async Task UpdateBook(Book book)
        {
            var result = await _http.PutAsJsonAsync($"api/Book/{book.QRCode}", book);

        }

        public async Task DeleteBook(int id)
        {
            var result = await _http.DeleteAsync($"api/Book/{id}");

        }

        public async Task GetLastBookAdded()
        {
            await GetBooks();
            int temp = Books.Count - 1;
            LastBookAdded = Books[temp];
        }


    }
}