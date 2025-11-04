using Library_Management_System.DTOs.Rent;

namespace Library_Management_System.Services.Student;

public interface IRentService
{
    Task<bool> CreateRentRequest(RentRequest rentRequest, int userId);
}