using Library_Management_System.Services;
using Library_Management_System.Services.Admin.Book;
using Library_Management_System.Services.Admin.Category;
using Library_Management_System.Services.Admin.User;

namespace Library_Management_System.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();
         services.AddScoped<IUserControlService,UserControlService>();
    }
}
