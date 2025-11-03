using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers.Admin;

public class RequestController :Controller
{
    [Authorize(Roles = "Admin")] 
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(View("Index"));
    }
    
}