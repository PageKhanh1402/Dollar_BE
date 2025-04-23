using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public int OrderID { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal ProductRating { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? SellerServiceRating { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
