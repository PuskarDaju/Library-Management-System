using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models;

public class Category
{
    [Key]
    public int Category_Id { get; set; }
    public string Category_Name { get; set; }
    
    //relations
    //one category has many books
    public List<Book> Books { get; set; }=new List<Book>();
}