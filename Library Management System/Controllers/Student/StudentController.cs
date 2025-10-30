using Library_Management_System.Models;
using Library_Management_System.Services.Admin.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library_Management_System.Controllers.Student;

public class StudentController(IBookService service):Controller
{
    private readonly IBookService _service=service ?? throw new ArgumentNullException(nameof(service));
    public async Task<IActionResult> Index()
    {
        var books= await _service.GetAllBooksAsync();
        return View("Index",books);
    }
}