using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models;

public class Income
{

    [Key]
    public int IncomeId { get; set; }
    [Required]
    [StringLength(25)]
    public required string Type { get; set; }
    [Required]
    public int BookId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public float Amount { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public int? FineId { get; set; }
    
    [ForeignKey("BookId")]
    public Book? Book { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    [ForeignKey("FineId")]
    public Fine? Fine { get; set; }
}