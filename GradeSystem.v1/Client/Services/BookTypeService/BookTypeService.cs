using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

namespace GradeSystem.v1.Client.Services.BookTypeService
{
    public class BookTypeService : IBookTypeService
    {
        public BookTypeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<BookType> BookTypes { get; set; } = new List<BookType>();
        public BookType LastBookTypeAdded { get; set; }
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public async Task GetBookTypes()
        {
            var result = await _http.GetFromJsonAsync<List<BookType>>("api/BookType");
            if (result != null)
            {
                BookTypes = result;
            }
        }
        public async Task<BookType> GetBookTypeByID(int ID)
        {
            var result = await _http.GetFromJsonAsync<BookType>($"api/BookType/{ID}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("BookType Not Find");
        }

        public async Task CreateBookType(BookType booktype)
        {
            var result = await _http.PostAsJsonAsync("api/BookType", booktype);

        }

        public async Task UpdateBookType(BookType booktype)
        {
            var result = await _http.PutAsJsonAsync($"api/BookType/{booktype.BookTypeID}", booktype);

        }

        public async Task DeleteBookType(int id)
        {
            var result = await _http.DeleteAsync($"api/BookType/{id}");

        }

        public async Task LastAddedBookType()
        {
            await GetBookTypes();
            if(BookTypes!=null)
            {
                int temp = BookTypes.Count - 1;
                LastBookTypeAdded = BookTypes[temp];
            }
        }


    }
}
