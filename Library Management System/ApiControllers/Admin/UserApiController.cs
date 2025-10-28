
using Library_Management_System.Services.Admin.Exception;
using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Services.Admin.User;


namespace Library_Management_System.ApiControllers.Admin;
[Microsoft.AspNetCore.Components.Route("api/users")]
[ApiController]
public class UserApiController(IUserService userService):ControllerBase
{
    private readonly IUserService _userService=userService;
    public async Task<IActionResult> UpgradeToAdmin(int id)
    {
        if (id == null)
            return BadRequest(new
            {
                status = "Error",
                message="Id cannot be null"
            });
        try
        {
            if (await _userService.UpgradeToAdmin(id))
                return Ok(new
                {
                    status = "success",
                    message = "User upgraded to admin"
                });
            return StatusCode(500, new
            {
                status = "Error",
                message = "User cannot upgraded to admin"
            });

        }
        catch (UserNotFoundException e)
        {
            return StatusCode(500, new
            {
                status = "Error",
                message = e.Message
            });
        }
    }

    public async Task<IActionResult> DowngradeToStudent(int id)
    {
        if (id==null||id<1)
            return BadRequest(new
            {
                status = "Error",
                message = "Id cannot be null"
            });
        try
        {
            if (await _userService.DowngradeToStudent(id))
                return Ok(new
                {
                    status = "success",
                    message = "User Downgraded to student"
                });
            return StatusCode(500, new
            {
                status = "Error",
                message = "User cannot be downgraded to student"
            });
        }
        catch (UserNotFoundException e)
        {
            return StatusCode(500, new
                {
                    status = "Error",
                    message = e.Message
                }
            );
        }
    }

    public async Task<IActionResult> BlackListUser(int id)
    {
        if (id == null)
            return BadRequest(new
            {
                status = "Error",
                mesage = "Id cannot be null"
            });
        try
        {
            if (await _userService.BlackList(id))
                return Ok(new
                {
                    status = "success",
                    message = "User Blacklisted"
                });
            return StatusCode(500, new
            {
                status = "Error",
                message = "User cannot be blacklisted"
            });
        }
        catch (UserNotFoundException e)
        {
            return StatusCode(500, new
                {
                    status = "Error",
                    message = e.Message
                }
            );
        }
    }
}