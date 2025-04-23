using DollarProject.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public int SellerID { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int TotalPriceXu { get; set; }

        public bool IsPaidWithWallet { get; set; } = false;

        [StringLength(50)]
        public string OrderStatus { get; set; } // From StatusEnum

        [StringLength(50)]
        public string DeliveryStatus { get; set; } = StatusEnum.Pending.ToString();

        [StringLength(50)]
        public string DeliveryMethod { get; set; } = Enums.DeliveryMethod.Automatic.ToString();

        public string DeliveryNotes { get; set; }

        public DateTime? DeliveredAt { get; set; }

        [StringLength(50)]
        public string? RefundStatus { get; set; } // NULL, or from StatusEnum

        public string? RefundReason { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User Buyer { get; set; }

        [ForeignKey("SellerID")]
        public virtual User Seller { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderStatusHistory> StatusHistories { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<OrderDispute> Disputes { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
