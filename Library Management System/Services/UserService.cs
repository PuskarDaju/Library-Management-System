

using Library_Management_System.Data;
using Library_Management_System.DTOs.User;
using Library_Management_System.Helpers;
using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Services;

public class UserService(ApplicationDbContext dbContext) : IUserService
{
    private  readonly ApplicationDbContext _context=dbContext;

    public async Task<UserResponseDto> RegisterAsync(UserDto userDto)
    {
        // Hash the password
        var hashedPassword = PasswordHelper.HashPassword(userDto.Password);

        var user = new User
        {
            Full_Name = userDto.FullName,
            Email = userDto.Email,
            Password_Hash = hashedPassword,
            Role = "Student" 
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            FullName = user.Full_Name,
            Email = user.Email,
            Role = user.Role
        };
        
    }

    public async Task<bool> GetByEmailAsync(string email)
    {

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return true;

        return false;

    }

    public async Task<UserResponseDto> LoginAsync(LoginDto loginDto)
    {
       var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
        {
            return null;
        }

        if (!PasswordHelper.VerifyPassword(loginDto.Password, user.Password_Hash))
        {
            return null;
        }
        UserResponseDto userResponse=new UserResponseDto
        {
            Id = user.Id,
            FullName = user.Full_Name,
            Email = user.Email,
            Role = user.Role
        };
        return userResponse;
    }

}
