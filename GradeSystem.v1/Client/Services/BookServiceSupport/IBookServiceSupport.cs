namespace GradeSystem.v1.Client.Services.BookServiceSupport
{
    public interface IBookServiceSupport
    {
        public Task UpdateList(bool isawailable, string btnText);

        public Task BooktypeAddEditUpdateList(string title, string author, string editon);

        IList<BookType> BookList { get; set; }
        IList<BookType> LiblarianBookList { get; set; }
    }
}
