using Library_Management_System.DTOs.Rent;
using Library_Management_System.Enum;
using Library_Management_System.Services.Student;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers.Student;

public class RentRequestController(IRentService service):ControllerBase
{
    public async Task<IActionResult> Index(RentRequest request)
    {
        var userId=User.FindFirst("userID")?.Value;
        if (userId == null)
        {
            return Unauthorized(new
            {
                error = "Unauthorized",
                message="Please login before you make a request",
            });
        }
        
        if (request.BookId <= 0)
            return BadRequest(new
            {
                status = "error",
                message = "please select a book",
            });
        if (request.RequestType is not BookRequestTypeEnum.PurchaseRequest or BookRequestTypeEnum.RentRequest)
            return BadRequest(new
            {
                status = "Error",
                message="Please sent a valid Request",
            });
        
        if (await service.CreateRentRequest(request,int.Parse(userId)))
            return Ok(new
            {
                status = "success",
                message = "Rent Request Created"
                
            });
        
        return StatusCode(500, new
        {
            status = "error",
            message = "Error Occured"
        });
    }
    
}