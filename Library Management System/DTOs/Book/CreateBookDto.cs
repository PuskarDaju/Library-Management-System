namespace Library_Management_System.DTOs.Book;

public class CreateBookDto
{
    public string BookName { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public DateOnly PublishDate { get; set; } 
    public int CategoryId { get; set; }
    public string Publisher { get; set; }
}