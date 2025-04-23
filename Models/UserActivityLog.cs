using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class UserActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        public int UserID { get; set; }

        [StringLength(50)]
        public string ActivityType { get; set; } // From ActivityTypeEnum

        public string ActivityDetails { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public DateTime ActivityTime { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
