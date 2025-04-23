using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public int SellerID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public int PriceXu { get; set; }

        public int Stock { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ProductType { get; set; } // From ProductTypeEnum

        public bool IsApproved { get; set; } = false;

        public int? ApprovedByUserID { get; set; }

        public string? RejectionReason { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("SellerID")]
        public virtual User Seller { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory Category { get; set; }

        [ForeignKey("ApprovedByUserID")]
        public virtual User ApprovedByUser { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Cart> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
