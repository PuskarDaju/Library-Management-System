using Library_Management_System.Enum;
using Library_Management_System.Services.Admin.Exception;
using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Services.Admin.User;
using Microsoft.AspNetCore.Authorization;


namespace Library_Management_System.ApiControllers.Admin;
[Route("api/users")]
[ApiController]
[AutoValidateAntiforgeryToken]
[Authorize(Roles = UserRoleEnum.Admin)]
public class UserApiController(IUserControlService userControlService):ControllerBase
{
    private readonly IUserControlService _userControlService=userControlService;
    
    /// <summary>
    /// Upgrades the specified user to the Admin role.
    /// </summary>
    /// <param name="id">The identifier of the user to upgrade.</param>
    /// <returns>An <see cref="IActionResult"/> that is a 200 response with a success status and message when the upgrade succeeds; otherwise a 500 response with an error status and message (the error message includes the user-not-found message when applicable).</returns>
    [HttpPatch("upgrade/{id}")]
    public async Task<IActionResult> UpgradeToAdmin(int id)
    {
        try
        {
            if (await _userControlService.UpgradeToAdmin(id))
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

    /// <summary>
    /// Downgrades the specified user to the student role.
    /// </summary>
    /// <param name="id">The identifier of the user to downgrade; must be greater than or equal to 1.</param>
    /// <returns>
    /// An IActionResult:
    /// - `200 OK` with a success message when the downgrade succeeds.
    /// - `400 Bad Request` when `id` is less than 1.
    /// - `500 Internal Server Error` when the user is not found or the downgrade fails.
    /// </returns>
    [HttpPatch("downgrade/{id}")]
    public async Task<IActionResult> DowngradeToStudent(int id)
    {
        if (id<1)
            return BadRequest(new
            {
                status = "Error",
                message = "Id cannot be null"
            });
        try
        {
            if (await _userControlService.DowngradeToStudent(id))
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
    /// <summary>
    /// Blacklists a user by their identifier.
    /// </summary>
    /// <param name="id">The user identifier to blacklist; must be greater than or equal to 1.</param>
    /// <returns>
    /// `200 OK` with a success message when the user is blacklisted;
    /// `400 Bad Request` if `id` is less than 1;
    /// `500 Internal Server Error` with an error message if the user is not found or blacklisting fails.
    /// </returns>
    [HttpPatch("blacklist/{id}")]

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BlackListUser(int id)
    {
        if (id <1)
            return BadRequest(new
            {
                status = "Error",
                mesage = "Id cannot be null"
            });
        try
        {
            if (await _userControlService.BlackList(id))
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