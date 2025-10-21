using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Book
{
    [Key,Required]
    public int Book_id { get; set; }
    public string Book_Name { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public string ISBN { get; set; }
    public string? Image_Url { get; set; }
    public int Category_Id { get; set; }
    public float Price { get; set; }
    public DateOnly? publication_Date { get; set; }
    
    //relations
    //one book belongs to one category
    [ForeignKey("Category_Id")]
    public Category Category { get; set; }
}

