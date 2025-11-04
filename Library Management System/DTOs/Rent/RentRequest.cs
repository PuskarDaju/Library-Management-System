namespace Library_Management_System.DTOs.Rent;

public class RentRequest
{
    public int? BookRequestId { get; set; }
    public int BookId { get; set; }
    public required string RequestType { get; set; }
}