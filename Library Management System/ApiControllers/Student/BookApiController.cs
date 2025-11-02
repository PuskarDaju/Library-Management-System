using Library_Management_System.Models;
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Management_System.ApiControllers.Student;
[ApiController]
[Route("api/student")]
public class BookApiController(IBookService service):ControllerBase
{
    private readonly IBookService _service=service ?? throw new ArgumentNullException(nameof(service));
   
    [HttpGet("paginated")]
    public async Task<IActionResult> Index(int page, int size)
    {
        if(size<=0) size=6;
        if(size>10) size=10;
        var paginatedBooks = await _service.GetPaginatedBooks(page==0?1:page, size==0?6:size);
        return Ok(paginatedBooks);
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search(string? searchTerm,int page=1)
    {
        var books = new PaginatedBook<Book>();
        if (searchTerm.IsNullOrEmpty())
        {
             books =   await _service.GetPaginatedBooks(page);
            return Ok(new
            {
                items = books.Items,
                CurrentPage = books.CurrentPage,
                pageSize = books.PageSize,
                totalCount = books.TotalCount,
                totalPages = books.TotalPages,
            });
        }
             books = await _service.GetSearchedBook(searchTerm, page);
            return Ok(new
            {
                items = books.Items,
                CurrentPage = books.CurrentPage,
                pageSize = books.PageSize,
                totalCount = books.TotalCount,
                totalPages = books.TotalPages,
            });

    }
    
}