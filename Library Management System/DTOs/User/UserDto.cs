namespace Library_Management_System.DTOs.User;

public class UserDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Role { get; set; }
}