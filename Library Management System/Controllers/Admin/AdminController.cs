using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminController:Controller
{
    /// <summary>
    /// Displays the Admin area's main Index view.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that renders the "Index" view.</returns>
    public IActionResult Index()
    {
        return View("Index");
    }
    
}