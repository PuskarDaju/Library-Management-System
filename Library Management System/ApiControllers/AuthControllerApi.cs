using Library_Management_System.DTOs.User;
using Library_Management_System.Helpers;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers;

[ApiController]
[Route("api/auth")]
public class AuthControllerApi(IUserService userService,JwtService jwtService) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly JwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));


    [HttpPost("register")]
    [AllowAnonymous]
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
    [AllowAnonymous]
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

        if (user != null)
        {
            // Generate JWT token
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role);
            Response.Cookies.Append("jwt_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(4)
            });

            return Ok(new
            {
                status = "success",
                message = "Login successful",
                role = user.Role,
                token = token
            });
        }

        return BadRequest(new
        {
            status = "error",
            message = "Invalid credentials"
        });
    }
    
        [HttpPost("logout")]
        [Authorize] 
        public IActionResult Logout()
        {
          
            if (Request.Cookies.ContainsKey("jwt"))
            {
                Response.Cookies.Delete("jwt");
            }
            return Ok(new { message = "Logged out successfully" });
        }
}




