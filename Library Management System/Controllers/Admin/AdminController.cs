using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers.Admin;

public class AdminController:Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
    
}