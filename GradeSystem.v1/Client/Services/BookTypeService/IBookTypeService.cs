namespace GradeSystem.v1.Client.Services.BookTypeService
{
    public interface IBookTypeService
    {
        Task<BookType> GetBookTypeByID(int BookTypeID);
        Task GetBookTypes();
        Task CreateBookType(BookType booktype);
        Task UpdateBookType(BookType booktype);
        Task DeleteBookType(int id);
        
        BookType LastBookTypeAdded { get; set; }

        IList<BookType> BookTypes { get; set; }

        Task LastAddedBookType();
    }
}
