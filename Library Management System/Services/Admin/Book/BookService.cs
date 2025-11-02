using Library_Management_System.Data;
using Library_Management_System.DTOs.Book;
using Library_Management_System.Models;
using Library_Management_System.Services.Admin.Exception;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Services.Admin.Book;

public class BookService(ApplicationDbContext dbContext) : IBookService
{
    private readonly ApplicationDbContext _context = dbContext;

    public async Task<bool> CreateBookAsync(CreateBookDto createBookDto)
    {
        var book = new Models.Book
        {
            Author = createBookDto.Author,
            Category_Id = createBookDto.CategoryId,
            Book_Name = createBookDto.BookName,
            Quantity = createBookDto.Quantity,
            Price = createBookDto.Price,
            Publisher = createBookDto.Publisher,
            Image_Url = createBookDto.ImageUrl,
            publication_Date = createBookDto.PublishDate
        };
        if (!string.IsNullOrEmpty(createBookDto.Author))
            book.Author = createBookDto.Author;
        if (!string.IsNullOrEmpty(createBookDto.ISBN))
            book.ISBN = createBookDto.ISBN;
        _context.Books.Add(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        var book = await GetBookAsync(updateBookDto.BookId);
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
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Models.Book> GetBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return book ?? throw new BookNotFoundException(id);
    }

    public async Task<List<Models.Book>> GetNewBooks()
    {
        var newBooks = await _context.Books.OrderByDescending(b => b.Book_id).Take(4).ToListAsync();
        return newBooks;
    }

    public async Task<PaginatedBook<Models.Book>> GetSearchedBook(string searchString, int page = 1, int pageSize = 6)
    {
        
        var totalBooks = await _context.Books.Where(b => b.Book_Name.Contains(searchString)
                                                   || b.Author.Contains(searchString) 
                                                   || b.ISBN.Contains(searchString)
                                                   ||(b.Category!=null && b.Category.Category_Name.Contains(searchString))
                                                   || b.Publisher.Contains(searchString)).CountAsync();
        
        var books = await _context.Books.Where(b => b.Book_Name.Contains(searchString)
                                                            || b.Author.Contains(searchString)
                                                            || b.ISBN.Contains(searchString)
                                                            || b.Category.Category_Name.Contains(searchString)
                                                            || b.Publisher.Contains(searchString))
            .OrderBy(b => b.Book_Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            
        return MakePaginatedModel(books, page, pageSize, totalBooks);

    }

    public async Task<List<Models.Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<PaginatedBook<Models.Book>> GetPaginatedBooks(int page=1,int pageSize=6)
    {
        var skip = (page - 1) * pageSize;
        var totalBooks = await _context.Books.CountAsync();

        var books = await _context.Books
            .OrderBy(b => b.Book_Name)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
       return MakePaginatedModel(books, page, pageSize, totalBooks);
    }

    private PaginatedBook<Models.Book> MakePaginatedModel(List<Models.Book> books, int page, int pageSize,int totalBooks)
    {
        var totalPages = (int)Math.Ceiling((double) totalBooks / pageSize);

        return new PaginatedBook<Models.Book>()
        {
            Items = books,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalCount = totalBooks
        };
        
    }
}