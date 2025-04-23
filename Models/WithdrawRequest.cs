using DollarProject.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class WithdrawRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        public int UserID { get; set; }

        public int AmountXu { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal AmountVND { get; set; }

        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string BankAccountNumber { get; set; }

        [StringLength(100)]
        public string AccountHolderName { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = StatusEnum.Pending.ToString();

        public DateTime RequestedAt { get; set; } = DateTime.Now;

        public DateTime? ProcessedAt { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
