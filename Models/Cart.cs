using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;

        public bool? IsChecked { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
