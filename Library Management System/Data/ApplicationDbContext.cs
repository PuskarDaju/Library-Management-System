using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;

namespace Library_Management_System.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}