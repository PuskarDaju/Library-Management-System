using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers.Student;
[ApiController]
[Route("api/student")]
public class BookApiController(IBookService service):ControllerBase
{
    private readonly IBookService _service=service ?? throw new ArgumentNullException(nameof(service));
   
    [HttpGet("paginated")]
    public async Task<IActionResult> Index(int page, int size)
    {
        var paginatedBooks = await _service.GetPaginatedBooks(page==0?1:page, size==0?6:size);
        return Ok(paginatedBooks);
    }
    
}