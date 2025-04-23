using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class MessageReadStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }

        public int MessageID { get; set; }

        public int UserID { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime? ReadAt { get; set; }

        // Navigation properties
        [ForeignKey("MessageID")]
        public virtual Message Message { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
