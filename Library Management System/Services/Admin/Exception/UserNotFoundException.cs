namespace Library_Management_System.Services.Admin.Exception;

public class UserNotFoundException:System.Exception
{
    public UserNotFoundException(int id) : base("User with ID: " + id + " not found.")
    {
    }
}