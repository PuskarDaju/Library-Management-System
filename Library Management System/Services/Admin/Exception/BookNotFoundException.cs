namespace Library_Management_System.Services.Admin.Exception;

public class BookNotFoundException(int id) : System.Exception("Book with ID: " + id + " not found.");