using Library_Management_System.Data;
using Library_Management_System.DTOs.Book;

namespace Library_Management_System.Services.Admin.Book;

public class BookService(ApplicationDbContext dbContext):IBookService
{
    private readonly ApplicationDbContext _context=dbContext;
    public async Task<bool> CreateBookAsync(CreateBookDto createBookDto)
    {
        Models.Book book = new Models.Book();
        book.Author = createBookDto.Author;
        book.Category_Id = createBookDto.CategoryId;
        book.Book_Name = createBookDto.BookName;
        book.Quantity = createBookDto.Quantity;
        book.Price = createBookDto.Price;
        if(!string.IsNullOrEmpty(createBookDto.Author))
            book.Author = createBookDto.Author;
        if(!string.IsNullOrEmpty(createBookDto.ISBN))
            book.ISBN = createBookDto.ISBN;
        if(createBookDto.PublishDate != null)
            book.publication_Date = createBookDto.PublishDate;
        
        _context.Books.Add(book);
        if (await _context.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;

        //throw new NotImplementedException();
    }

    public Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Book> GetBookAsync(int id)
    {
        throw new NotImplementedException();
    }
}