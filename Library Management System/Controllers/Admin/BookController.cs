using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers.Admin;
[Authorize (Roles = "Admin")]
public class BookController(ApplicationDbContext dbContext):Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.ActiveMenu="Book";
       var books = await dbContext.Books.Include(b=>b.Category).ToListAsync();
        
        return View($"~/Views/Admin/Book/Index.cshtml",books);
    }

    public IActionResult Create()
    {
        ViewBag.ActiveMenu="Book";
        var  categories = dbContext.Category.ToList();
        return View("~/Views/Admin/Book/Create.cshtml",categories);
    }

    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.ActiveMenu="Book";
        var book=await dbContext.Books.FindAsync(id);
        if (book==null) return NotFound(
        new
        {
            status = "error",
            message = "Book does not exist"
        });
        var categories = dbContext.Category.ToList();
        var model = new EditBookView
        {
            Book = book,
            Categories = categories
        };
     
        return View("~/Views/Admin/Book/Edit.cshtml",model);
    }
    
}