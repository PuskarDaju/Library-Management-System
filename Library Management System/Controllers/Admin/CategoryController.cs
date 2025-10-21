
using Library_Management_System.Models;
using Library_Management_System.Services.Admin.Category;
using Microsoft.AspNetCore.Mvc;


namespace Library_Management_System.Controllers.Admin;

public class CategoryController(ICategoryService service):Controller
{

    private readonly ICategoryService _service=service ?? throw new ArgumentNullException(nameof(service));
   

    public async Task<IActionResult> Index()
    {
        var category = await _service.GetAllCategoriesAsync();
        return View($"~/Views/Admin/Category/Index.cshtml",category);
    }

    public IActionResult Create()
    {
        return View("~/Views/Admin/Category/Create.cshtml");
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var category=await _service.GetCategoryByIdAsync(id);
        if (category==null) return NotFound(
        new
        {
            status = "error",
            message = "Category does not exist"
        });
     
        return View("~/Views/Admin/Category/Edit.cshtml",category);
    }
    
    
}