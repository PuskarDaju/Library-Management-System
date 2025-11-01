using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;

namespace Library_Management_System.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Book_Request> BookRequests { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Rent_log>  Rent_logs { get; set; }
    }
}