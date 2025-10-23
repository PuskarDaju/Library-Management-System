using Library_Management_System.DTOs.Book;
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.ApiControllers.Admin;
[ApiController]
[Route($"api/book-api/[action]")]
public class BookApiController(IBookService service) :ControllerBase {
    private readonly IBookService _service=service;
    [HttpPost]
    public async Task<IActionResult> Create( CreateBookDto dto) {
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

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBookDto dto)
    {
        if (dto.BookId == null || dto.BookId <1)
            return BadRequest(new
            {
                status = "error",
                message = "Invalid book id"
            });
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

        if (await _service.UpdateBookAsync(dto))
            return Ok(new
            {
                status = "success",
                message = "Book Updated Successfully"
            });
        return StatusCode(500, new
        {
            status = "error",
            message = "Internal Server Error just to check"
        });
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id == null || id < 1)
            return BadRequest(new
            {
                status = "error",
                message = "Invalid book id"
            });
        if (await _service.DeleteBookAsync(id))
            return Ok(new
            {
                status = "success",
                message = "Book Deleted Successfully",
            });
        return StatusCode(500, new
        {
            status = "error",
            message = "Internal Server Error just to check"
        });
    }
}