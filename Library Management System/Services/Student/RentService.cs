using Library_Management_System.Data;
using Library_Management_System.DTOs.Rent;
using Library_Management_System.Models;
using Library_Management_System.Services.Admin.Book;

namespace Library_Management_System.Services.Student;

public class RentService(ApplicationDbContext dbContext, IBookService bookService) : IRentService
{
    public async Task<bool> CreateRentRequest(RentRequest rentRequest, int userId)
    { 
            var request = new BookRequest
            {
                BookId = rentRequest.BookId,
                UserId = userId,
                RequestType = rentRequest.RequestType,
            };
        
            await dbContext.BookRequests.AddAsync(request);
        

        if (await dbContext.SaveChangesAsync() <= 0) return false;
        return await bookService.DecreaseBookQuantity(rentRequest.BookId);
    }
}