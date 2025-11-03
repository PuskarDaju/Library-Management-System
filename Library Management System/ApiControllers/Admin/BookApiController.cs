using Library_Management_System.DTOs.Book;
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.ApiControllers.Admin;
[ApiController]
[Route("api/book-api/[action]")]
[Authorize(Roles = "Admin")]
[AutoValidateAntiforgeryToken]
public class BookApiController(IBookService service) :ControllerBase {
    [HttpPost]
    
    public async Task<IActionResult> Create(CreateBookDto dto) {
        
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
        
        var validExtension = new List<string> { ".jpg", ".jpeg", ".png", "webp" };
        var extension=Path.GetExtension(dto.Image.FileName).ToLower();
        
        if (!validExtension.Contains(extension))
            return BadRequest(new
            {
                status = "error",
                message = "Only JPG, JPEG, and WEBP image formats are allowed"
            });
        
        if (dto.Image.Length is > 2 * 1024 * 1024 or 0)
            return BadRequest(new
            {
                status = "error",
                message = "Image must be a valid and no more than 2mb"
            });
        
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Books");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        var uniqueFileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(path, uniqueFileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Image.CopyToAsync(stream); 
        }
        dto.ImageUrl ="/Images/Books/"+uniqueFileName;
        
        if (await service.CreateBookAsync(dto))
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

        if (await service.UpdateBookAsync(dto))
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
        if (id < 1)
            return BadRequest(new
            {
                status = "error",
                message = "Invalid book id"
            });
        if (await service.DeleteBookAsync(id))
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