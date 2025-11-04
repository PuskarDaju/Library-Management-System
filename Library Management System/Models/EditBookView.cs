namespace Library_Management_System.Models;

public class EditBookView
{
    public required Book Book { get; set; }
    public required List<Category> Categories { get; set; }
}