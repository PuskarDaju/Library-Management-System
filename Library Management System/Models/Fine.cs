using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Fine
{
    [Key]
    public int FineId { get; set; }
    [Required]
    public string Cause { get; set; }
    
    public string Description { get; set; }
    
}