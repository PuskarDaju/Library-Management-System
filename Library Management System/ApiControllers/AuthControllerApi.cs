using Library_Management_System.DTOs.User;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers;

[ApiController]
[Route("api/auth")]
public class AuthControllerApi(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                status = "error",
                message = "Invalid request body"
            });
        }

        if (dto.Password != dto.ConfirmPassword)
        {
            return BadRequest(new
            {
                status = "error",
                message = "Passwords do not match"
            });
        }
        if (!await _userService.GetByEmailAsync(dto.Email))
            return BadRequest(new
            {
                status = "error",
                message = "User already exists"
            });
        
        await _userService.RegisterAsync(dto);
        return Ok(new
        {
            status = "success",
            message = "Registration successful"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                status = "Invalid",
                message = "Invalid request body"
            });
        }

        var user = await _userService.LoginAsync(dto);

        if (user!=null)
        {
            return Ok(new
            {
                status = "success",
                message = "Login successful",
                role = user.Role,
            });
        }

        return BadRequest(new
        {
            status = "error",
            message = "Invalid credentials"
        });
    }


}