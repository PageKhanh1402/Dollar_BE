using DollarProject.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class OrderDispute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DisputeID { get; set; }

        public int OrderID { get; set; }

        public int ReportedByUserID { get; set; }

        [StringLength(50)]
        public string DisputeType { get; set; } // From DisputeTypeEnum

        public string Description { get; set; }

        public string Evidence { get; set; } // URLs to screenshots or other evidence

        [StringLength(50)]
        public string Status { get; set; } = StatusEnum.Open.ToString();

        public int? AssignedToStaffID { get; set; }

        public string Resolution { get; set; }

        public DateTime? ResolutionDate { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ReportedByUserID")]
        public virtual User ReportedByUser { get; set; }

        [ForeignKey("AssignedToStaffID")]
        public virtual User AssignedToStaff { get; set; }
    }
}
