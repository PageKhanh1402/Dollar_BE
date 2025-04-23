using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class OrderStatusHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryID { get; set; }

        public int OrderID { get; set; }

        [StringLength(50)]
        public string? PreviousStatus { get; set; }

        [StringLength(50)]
        public string NewStatus { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;

        public int ChangedByUserID { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ChangedByUserID")]
        public virtual User ChangedByUser { get; set; }
    }
}
