using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library_Management_System.Controllers.Student;

public class StudentController:Controller
{
    public IActionResult Index()
    {
        return View("Index");
    }
}