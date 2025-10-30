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
        if (dto.Image == null)
            return BadRequest(new
            {
                status = "error",
                message = "Image is Required"
            });
        
        var validExtension = new List<string> { ".jpg", ".jpeg", ".png", "webp" };
        var extension=Path.GetExtension(dto.Image.FileName).ToLower();
        
        if (!validExtension.Contains(extension))
            return BadRequest(new
            {
                status = "error",
                message = "Only JPG, JPEG, and WEBP image formats are allowed"
            });
        
        if (dto.Image.Length > 2 * 1024 * 1024)
            return BadRequest(new
            {
                status = "error",
                message = "Image size must not exceed 2MB"
            });
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Books");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        var uniqueFileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(path, uniqueFileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Image.CopyToAsync(stream);  // <-- Missing line added here
        }
        dto.ImageUrl ="/Images/Books/"+uniqueFileName;
        
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
    [HttpPut]
    public async Task<IActionResult> Update( UpdateBookDto dto)
    {
        if (dto.BookId <1)
            return BadRequest(new
            {
                status = "error",
                message = "Invalid book id"
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
        if (dto.Image != null)
        {

            var validExtension = new List<string> { ".jpg", ".jpeg", ".png", "webp" };
            var extension = Path.GetExtension(dto.Image.FileName).ToLower();
            if (!validExtension.Contains(extension))
                return BadRequest(new
                    { status = "error", message = "Only JPG, JPEG, and WEBP image formats are allowed" });
            if (dto.Image.Length > 2 * 1024 * 1024)
                return BadRequest(new { status = "error", message = "Image size must not exceed 2MB" });
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Books");
            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(path, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            dto.ImageUrl = "/Images/Books/" + uniqueFileName;
        }

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