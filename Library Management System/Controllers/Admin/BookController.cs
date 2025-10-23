using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers.Admin;
public class BookController(ApplicationDbContext dbContext):Controller
{
    private readonly ApplicationDbContext _dbContext=dbContext;
    
    public async Task<IActionResult> Index()
    {
       var books = await _dbContext.Books.ToListAsync();
        
        return View($"~/Views/Admin/Book/Index.cshtml",books);
    }

    public IActionResult Create()
    {
        var  categories = _dbContext.Category.ToList();
        return View("~/Views/Admin/Book/Create.cshtml",categories);
    }
    
}