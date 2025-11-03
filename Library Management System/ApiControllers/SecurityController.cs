using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.ApiControllers;
[Authorize]
[Route("api/security")]
public class SecurityController(IAntiforgery antiforgery):Controller
{
    [HttpGet("csrf-token")]
    public IActionResult GetCsrfToken()
    {
        var token = antiforgery.GetAndStoreTokens(HttpContext);
        Response.Cookies.Append("XSRF-TOKEN", token.RequestToken!,new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
        return Ok(new
        {
            message = "CSRF token set",
        });
    }
    
    
}