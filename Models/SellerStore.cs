using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DollarProject.Models
{
    public class SellerStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }

        public int SellerID { get; set; }
        public string StoreName { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string LogoURL { get; set; }

        [StringLength(255)]
        public string BannerURL { get; set; }

        public bool IsVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("SellerID")]
        public virtual User Seller { get; set; }

        [ValidateNever]
        public virtual ICollection<Product> Products { get; set; }

    }
}
