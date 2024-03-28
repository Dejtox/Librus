using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using GradeSystem.v1.Client.Services;
using GradeSystem.v1.Client.Services.BookServiceSupport;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.BookServiceSupport
{

    public class BookServiceSupport : IBookServiceSupport
    { 
        
        public BookServiceSupport(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
            
        }
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public IList<BookType> BookList { get; set; } = new List<BookType>();

        public IList<BookType> LiblarianBookList { get; set; } = new List<BookType>();

        public async Task UpdateList(bool isawailable, string btnText)
        {
            var TypeService = new BookTypeService.BookTypeService(_http, _navigationManager);
            await TypeService.GetBookTypes();
            BookList.Clear();
            Regex rg = new Regex(btnText);

            if (btnText.Length == 0)
            {

                if (isawailable != true)
                {
                    BookList = TypeService.BookTypes;
                }
                else
                {
                    foreach (var BookType in TypeService.BookTypes)
                    {
                        if (BookType.AmountOfBooks > 0)
                        {
                            BookList.Add(BookType);

                        }
                        else
                        {
                            continue;
                        }
                    }

                }
            }
            else
            {
                foreach (var BookType in TypeService.BookTypes)
                {
                    if (isawailable == true)
                    {
                        if (BookType.AmountOfBooks == 0)
                        {
                            continue;
                        }
                    }

                    MatchCollection machedTitle = rg.Matches(BookType.Title);
                    MatchCollection machedAuthor = rg.Matches(BookType.Author);
                    MatchCollection machedEdition = rg.Matches(BookType.Edition);

                    if (machedTitle.Count > 0 || machedAuthor.Count > 0 || machedEdition.Count > 0)
                    {
                        BookList.Add(BookType);
                    }
                }
            }
        }
        public async Task BooktypeAddEditUpdateList(string title, string author, string editon)
        {
            var TypeService = new BookTypeService.BookTypeService(_http, _navigationManager);
            await TypeService.GetBookTypes();
            LiblarianBookList.Clear();
            Regex rg1 = new Regex(title);
            Regex rg2 = new Regex(author);
            Regex rg3 = new Regex(editon);
            int n1 = 0;
            int n2 = 0;
            int n3 = 0;
            int par = 0;
            if (title.Length != 0)
            {
                n1 = 1;
                par = par + n1;
            }
            if (author.Length != 0)
            {
                n2 = 1;
                par = par + n2;

            }
            if (editon.Length != 0)
            {
                n3 = 1;
                par = par + n3;
            }
            if (par == 0)
            {
                await Console.Out.WriteLineAsync(title);
                LiblarianBookList = TypeService.BookTypes;
            }
            else
            {
                foreach (var BookType in TypeService.BookTypes)
                {
                   int tempar = par; 
                    
                    
                    if (n1 != 0)
                    {
                        MatchCollection machedTitle = rg1.Matches(BookType.Title);
                        if (machedTitle.Count > 0)
                        {
                            tempar--;
                        }
                    }

                    if(n2 != 0) 
                    {
                        MatchCollection machedAuthor = rg2.Matches(BookType.Author);
                        if(machedAuthor.Count > 0)
                        {
                            tempar--;
                        }

                    }

                    if(n3 != 0)
                    {
                        MatchCollection machedEdition = rg3.Matches(BookType.Edition);
                        if(machedEdition.Count > 0)
                        {
                            tempar--;
                        }

                    }
                    if(tempar == 0) 
                    {
                        LiblarianBookList.Add(BookType);
                    }
                }

            }
        }

    }





}
