using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Book
{
    [Key,Required]
    public int BookId { get; set; }
    [Required]
    [StringLength(40)]
    public required string BookName { get; set; }
    [Required]
    [StringLength(40)]
    public required string Author { get; set; }
    [Required]
    [StringLength(40)]
    public required string Publisher { get; set; }
    [Required]
    [StringLength(40)]
    public required string Isbn { get; set; }
  
    [StringLength(255)]
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public float Price { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public int Quantity { get; set; }
    
    //relations
    //one book belongs to one category
    [ForeignKey("CategoryId")] public Category? Category { get; set; }
}

