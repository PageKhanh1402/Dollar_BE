using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DollarProject.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        //SellerID => UserID
        public int UserID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        public string? Description { get; set; }
        public string? AccountInfomation { get; set; }

        public int PriceXu { get; set; }

        public int Stock { get; set; }



        [StringLength(255)]
        public string ImageURL { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ProductType { get; set; } // From ProductTypeEnum

        public bool IsSold { get; set; } = false;

        public bool IsApproved { get; set; } = false;

        public bool IsRejected { get; set; } = false;

        public int? ApprovedByUserID { get; set; }

        public string? RejectionReason { get; set; }

        [Column(TypeName = "decimal(3, 2)")]
        public decimal? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ValidateNever]
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ValidateNever]
        [ForeignKey("CategoryID")]
        public virtual ProductCategory Category { get; set; }

        [ValidateNever]
        [ForeignKey("ApprovedByUserID")]
        public virtual User ApprovedByUser { get; set; }

        [ValidateNever]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        [ValidateNever]
        public virtual ICollection<Cart> CartItems { get; set; }
        [ValidateNever]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [ValidateNever]
        public virtual ICollection<Review> Reviews { get; set; }

        //Xoa Store
        //public int? StoreID { get; set; }
        // Navigation properties
        //[ValidateNever]
        //[ForeignKey("StoreID")]
        //public virtual SellerStore Store { get; set; }
    }
}
