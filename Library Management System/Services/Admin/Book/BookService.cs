using Library_Management_System.Data;
using Library_Management_System.DTOs.Book;
using Microsoft.AspNetCore.Http.HttpResults;

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
        book.Publisher = createBookDto.Publisher;
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

    public async Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        var book = await _context.Books.FindAsync(updateBookDto.BookId);
        if (book == null) return false;
        book.Author = updateBookDto.Author;
        book.Book_id=updateBookDto.BookId;
        book.Category_Id = updateBookDto.CategoryId;
        book.Book_Name = updateBookDto.BookName;
        book.Quantity = updateBookDto.Quantity;
        book.Price = updateBookDto.Price;
        book.Publisher = updateBookDto.Publisher;
        if(!string.IsNullOrEmpty(updateBookDto.Author))
            book.Author = updateBookDto.Author;
        if(!string.IsNullOrEmpty(updateBookDto.ISBN))
            book.ISBN = updateBookDto.ISBN;
        if(updateBookDto.PublishDate != null)
            book.publication_Date = updateBookDto.PublishDate;
        _context.Books.Update(book);
       if( await _context.SaveChangesAsync()>0)
           return true;
       return false;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
       var book= await _context.Books.FindAsync(id);
       if (book == null)
           return false;
       _context.Books.Remove(book);
       if(await _context.SaveChangesAsync()>0)
           return true;
       return false;
    }

    public Task<Models.Book> GetBookAsync(int id)
    {
        throw new NotImplementedException();
    }
}