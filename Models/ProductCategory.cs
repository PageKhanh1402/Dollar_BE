using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int? ParentCategoryID { get; set; }

        // Navigation properties
        [ForeignKey("ParentCategoryID")]
        public virtual ProductCategory ParentCategory { get; set; }

        public virtual ICollection<ProductCategory> ChildCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
