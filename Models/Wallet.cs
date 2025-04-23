using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletID { get; set; }

        public int UserID { get; set; }

        public int XuBalance { get; set; } = 0;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual ICollection<WalletTransaction> Transactions { get; set; }
    }
}
