using Library_Management_System.Enum;

namespace Library_Management_System.DTOs.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}