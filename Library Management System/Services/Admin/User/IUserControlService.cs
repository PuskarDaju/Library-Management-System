namespace Library_Management_System.Services.Admin.User;

public interface IUserControlService
{
    Task<bool> UpgradeToAdmin(int id);
    Task<bool> DowngradeToStudent(int id);
    Task<bool>BlackList(int id);
}