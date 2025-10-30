using Library_Management_System.DTOs.Book;

namespace Library_Management_System.Services.Admin.Book;

public interface IBookService
{
    Task<bool> CreateBookAsync(CreateBookDto createBookDto);
    Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto);
    Task<bool> DeleteBookAsync(int id);
    Task<Models.Book> GetBookAsync(int id);
    Task<List<Models.Book>> GetAllBooksAsync();

}