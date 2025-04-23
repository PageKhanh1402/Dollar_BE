using DollarProject.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class ProductDeliveryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliveryID { get; set; }

        public int OrderDetailID { get; set; }

        // Loại bỏ trường DeliveryType và enum tương ứng
        [StringLength(255)]
        public string AccountUsername { get; set; }

        [StringLength(255)]
        public string AccountPassword { get; set; } // Should be encrypted in real application

        [StringLength(100)]
        public string GameServer { get; set; }

        [StringLength(100)]
        public string CharacterName { get; set; }

        [StringLength(255)]
        public string ItemCode { get; set; }

        public string DeliveryInstructions { get; set; }

        public DateTime? DeliveredAt { get; set; }

        [StringLength(50)]
        public string DeliveryStatus { get; set; } = StatusEnum.Pending.ToString();

        public string DeliveryFailReason { get; set; }

        // Navigation property
        [ForeignKey("OrderDetailID")]
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
