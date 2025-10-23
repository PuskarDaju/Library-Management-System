using Library_Management_System.DTOs.Book;
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.ApiControllers.Admin;
[ApiController]
[Route($"api/book-api/[action]")]
public class BookApiController(IBookService service) :ControllerBase {
    private readonly IBookService _service=service;
    public async Task<IActionResult> Create(CreateBookDto dto) {
        if (dto == null)
            return BadRequest(new
            {
                status = "error",
                message = "Book object is null"
            });
        

        if (string.IsNullOrEmpty(dto.BookName))
                return BadRequest(new
                {
                    status = "error",
                    message = "bookname cannot be empty",
                });

        if (string.IsNullOrEmpty(dto.Author))
            return BadRequest(new
            {
                status = "error",
                message = "author cannot be empty",
            });

        if (int.IsNegative(dto.Quantity))
            return BadRequest(new
            {
                status = "error",
                message = "quantity cannot be negative",
            });
        if (dto.CategoryId < 0)
            return BadRequest(new
            {
                status = "error",
                message = "select a valid category",
            });

        if (await _service.CreateBookAsync(dto))
        {
            return Ok(new
            {
                status = "success",
                message = "Book created"
            });
        }

        return StatusCode(500,
            new
            {
                status = "error",
                message = "Book creation failed"
            });
    }
    
}