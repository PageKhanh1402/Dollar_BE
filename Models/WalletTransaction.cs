using DollarProject.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class WalletTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        public int WalletID { get; set; }

        [StringLength(50)]
        public string TransactionType { get; set; } // From TransactionTypeEnum

        [Column(TypeName = "decimal(12, 2)")]
        public decimal AmountVND { get; set; }

        public int AmountXu { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } // From PaymentMethodEnum

        [StringLength(50)]
        public string Status { get; set; } = StatusEnum.Pending.ToString();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("WalletID")]
        public virtual Wallet Wallet { get; set; }
    }
}
