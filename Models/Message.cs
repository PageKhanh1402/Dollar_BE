using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        public int ConversationID { get; set; }

        public int SenderID { get; set; }

        public string MessageText { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("ConversationID")]
        public virtual Conversation Conversation { get; set; }

        [ForeignKey("SenderID")]
        public virtual User Sender { get; set; }

        public virtual ICollection<MessageReadStatus> ReadStatuses { get; set; }
    }
}
