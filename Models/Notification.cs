using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        public string Content { get; set; }

        [StringLength(50)]
        public string? NotificationType { get; set; } // From NotificationTypeEnum

        public int? RelatedEntityID { get; set; }

        [StringLength(50)]
        public string? RelatedEntityType { get; set; } // 'Product', 'Order', etc.

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
