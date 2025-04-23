using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class Follower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowID { get; set; }

        public int FollowerID { get; set; } // User who is following

        public int SellerID { get; set; } // Seller being followed

        public DateTime FollowedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("FollowerID")]
        public virtual User FollowerUser { get; set; }

        [ForeignKey("SellerID")]
        public virtual User SellerUser { get; set; }
    }
}
