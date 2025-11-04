using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    public required string CategoryName { get; set; }
    
    //relations
    //one category has many books
    public List<Book> Books { get; set; }= [];
}