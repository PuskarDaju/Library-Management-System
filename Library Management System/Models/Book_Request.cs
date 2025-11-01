using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Book_Request
{
    [Key]
    public int Book_request_id { get; set; }
    [Required]
    public string Request_type { get; set; }
    [Required]
    public int User_id { get; set; }
    [Required]
    public int Book_id { get; set; }

   [ForeignKey("Book_id")]
   public Book Book { get; set; }
   [ForeignKey("User_id")]
   public User User{ get; set; }
}