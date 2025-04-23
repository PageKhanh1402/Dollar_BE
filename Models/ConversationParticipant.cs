using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class ConversationParticipant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParticipantID { get; set; }

        public int ConversationID { get; set; }

        public int UserID { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        public DateTime? LeftAt { get; set; }

        // Navigation properties
        [ForeignKey("ConversationID")]
        public virtual Conversation Conversation { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
