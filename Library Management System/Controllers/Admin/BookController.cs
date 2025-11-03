using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers.Admin;
[Authorize (Roles = "Admin")]
public class BookController(ApplicationDbContext dbContext):Controller
{
    /// <summary>
    /// Displays the admin book index page and sets the active menu to "Book".
    /// </summary>
    /// <returns>An IActionResult that renders the Admin Book Index view populated with the list of books (including their Category data).</returns>
    public async Task<IActionResult> Index()
    {
        ViewBag.ActiveMenu="Book";
       var books = await dbContext.Books.Include(b=>b.Category).ToListAsync();
        
        return View($"~/Views/Admin/Book/Index.cshtml",books);
    }

    /// <summary>
    /// Displays the admin book creation view and provides the list of categories for the form.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that renders the Admin/Book Create view with the list of categories as the model.</returns>
    public IActionResult Create()
    {
        ViewBag.ActiveMenu="Book";
        var  categories = dbContext.Category.ToList();
        return View("~/Views/Admin/Book/Create.cshtml",categories);
    }

    /// <summary>
    /// Displays the edit form for the book with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the book to edit.</param>
    /// <returns>An <see cref="IActionResult"/> containing the Edit view with an <see cref="EditBookView"/> model when the book exists, or a NotFound result with an error object when the book does not exist.</returns>
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