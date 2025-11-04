using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models;

public class Fine
{
    [Key]
    public int FineId { get; set; }
    [Required]
    [StringLength(50)]
    public required string Cause { get; set; }
    [StringLength(10000)]
    public string? Description { get; set; }
    
}