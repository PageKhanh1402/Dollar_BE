using DollarProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer; // Default role

        public UserStatus Status { get; set; } = UserStatus.Active; // Default status

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
