using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers.Admin;

public class RequestController :Controller
{
    /// <summary>
    /// Displays the "Index" view for managing requests; access is restricted to users in the "Admin" role.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that renders the "Index" view.</returns>
    [Authorize(Roles = "Admin")] 
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(View("Index"));
    }
    
}