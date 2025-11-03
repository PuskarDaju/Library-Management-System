using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Book
{
    [Key,Required]
    public int BookId { get; set; }
    public string BookName { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string Isbn { get; set; }
    public string? ImageUrl { get; set; }
    public int Category_Id { get; set; }
    public float Price { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public int Quantity { get; set; }
    
    //relations
    //one book belongs to one category
    [ForeignKey("Category_Id")]
    public Category Category { get; set; }
}

