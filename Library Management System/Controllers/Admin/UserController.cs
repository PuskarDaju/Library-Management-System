using Library_Management_System.Data;
using Library_Management_System.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers.Admin;

public class UserController(ApplicationDbContext context):Controller
{
    private readonly ApplicationDbContext _context=context;
    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.Where(u=>u.Role!=UserRoleEnum.Blacklist).ToListAsync();
        return View("/Views/Admin/User/Index.cshtml", users);
    }
}