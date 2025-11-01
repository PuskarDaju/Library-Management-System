
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.Controllers.Student;

[Route("student")]
public class StudentController(IBookService service):Controller
{
    private readonly IBookService _service=service ?? throw new ArgumentNullException(nameof(service));
    public async Task<IActionResult> Index()
    {
        ViewData["ActiveMenu"] = "Student";
        var books= await _service.GetAllBooksAsync();
        return View("Index",books);
    }
    [Route("Requests")]

    public async Task<IActionResult> Requests()
    {
        ViewData["ActiveMenu"] = "Requests";
        return View("Requests");  
    }

    [Route("Rentals")]
    public async Task<IActionResult> Rentals()
    {
        ViewData["ActiveMenu"] = "Rentals";
        return View("Rentals");
    }

    [Route("History")]
    public async Task<IActionResult> History()
    {
        ViewData["ActiveMenu"] = "History";
        return View("History");  
    }
    
    [Route("Profile")]
    public async Task<IActionResult> Profile()
    {
        ViewData["ActiveMenu"] = "History";
        return View("Profile/Index");  
    }
}