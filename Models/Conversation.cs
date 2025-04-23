using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConversationID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastMessageAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<ConversationParticipant> Participants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
