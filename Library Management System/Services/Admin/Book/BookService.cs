using Library_Management_System.Data;
using Library_Management_System.DTOs.Book;
using Library_Management_System.Services.Admin.Exception;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Services.Admin.Book;

public class BookService(ApplicationDbContext dbContext) : IBookService
{
    private readonly ApplicationDbContext _context = dbContext;

    public async Task<bool> CreateBookAsync(CreateBookDto createBookDto)
    {
        Models.Book book = new Models.Book();
        book.Author = createBookDto.Author;
        book.Category_Id = createBookDto.CategoryId;
        book.Book_Name = createBookDto.BookName;
        book.Quantity = createBookDto.Quantity;
        book.Price = createBookDto.Price;
        book.Publisher = createBookDto.Publisher;
        book.Image_Url = createBookDto.ImageUrl;
        book.publication_Date = createBookDto.PublishDate;
        if (!string.IsNullOrEmpty(createBookDto.Author))
            book.Author = createBookDto.Author;
        if (!string.IsNullOrEmpty(createBookDto.ISBN))
            book.ISBN = createBookDto.ISBN;
        _context.Books.Add(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        var book = await _context.Books.FindAsync(updateBookDto.BookId);
        if (book == null) return false;
        //here it must have a try catch
        if (book.Image_Url != null)
        {
            var oldImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.Image_Url.TrimStart('/'));
            if (File.Exists(oldImage))
                File.Delete(oldImage);
        }

        book.Author = updateBookDto.Author;
        book.Book_id = updateBookDto.BookId;
        book.Category_Id = updateBookDto.CategoryId;
        book.Book_Name = updateBookDto.BookName;
        book.Quantity = updateBookDto.Quantity;
        book.Price = updateBookDto.Price;
        if(updateBookDto.ImageUrl!=null)
            book.Image_Url = updateBookDto.ImageUrl;
        book.Publisher = updateBookDto.Publisher;
        if (!string.IsNullOrEmpty(updateBookDto.Author))
            book.Author = updateBookDto.Author;
        if (!string.IsNullOrEmpty(updateBookDto.ISBN))
            book.ISBN = updateBookDto.ISBN;
        book.publication_Date = updateBookDto.PublishDate;
        _context.Books.Update(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return false;
        _context.Books.Remove(book);
        if (await _context.SaveChangesAsync() > 0)
            return true;
        return false;
    }

    public async Task<Models.Book> GetBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book ?? throw new BookNotFoundException(id);
    }

    public async Task<List<Models.Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }
}