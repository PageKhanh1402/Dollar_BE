using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int OrderID { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } // From PaymentMethodEnum

        public int AmountXu { get; set; }

        [StringLength(50)]
        public string PaymentStatus { get; set; } // From StatusEnum

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
