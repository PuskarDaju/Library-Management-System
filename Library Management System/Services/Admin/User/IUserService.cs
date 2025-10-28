namespace Library_Management_System.Services.Admin.User;

public interface IUserService
{
    Task<bool> UpgradeToAdmin(int id);
    Task<bool> DowngradeToStudent(int id);
    Task<bool>BlackList(int id);
}