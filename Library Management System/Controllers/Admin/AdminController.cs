using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminController:Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
    
}