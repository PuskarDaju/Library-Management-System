namespace Library_Management_System.Services.Admin.Exception;

public class NotEnoughException(string bookName) : System.Exception("We dont have enough" + bookName);
