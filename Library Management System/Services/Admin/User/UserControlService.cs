using Library_Management_System.Data;
using Library_Management_System.Enum;
using Library_Management_System.Services.Admin.Exception;

namespace Library_Management_System.Services.Admin.User;

public class UserControlService(ApplicationDbContext context):IUserControlService
{
 private readonly ApplicationDbContext _context=context;
    public async Task<bool> UpgradeToAdmin(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new UserNotFoundException(id);
        user.Role = "Admin";
        _context.Users.Update(user);
        if (await _context.SaveChangesAsync()>0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> DowngradeToStudent(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new UserNotFoundException(id);
        user.Role = UserRoleEnum.Student;
        _context.Users.Update(user);
        if (await _context.SaveChangesAsync()>0)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> BlackList(int id)
    {
         var user = await _context.Users.FindAsync(id);
        if (user == null) throw new UserNotFoundException(id);
        user.Role ="Blacklist";
        _context.Users.Update(user);
        if (await _context.SaveChangesAsync()>0)
        {
            return true;
        }
        return false;
    }
}