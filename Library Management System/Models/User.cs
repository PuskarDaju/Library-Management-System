
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public required string Email { get; set; }
        [Required]
        [StringLength(255)]
        public required string PasswordHash { get; set; }
        [Required]
        [StringLength(25)]
        public required string Role { get; set; }
    
    }
    
}
