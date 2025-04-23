using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DollarProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string? ImageURL { get; set; } 

 
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public int RoleID { get; set; }

        public bool IsVerifiedSeller { get; set; } = false;

        [Column(TypeName = "decimal(3, 2)")]
        public decimal SellerRating { get; set; } = 0;

        public string? SellerDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        public virtual Wallet Wallet { get; set; }
        public virtual SellerStore SellerStore { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> BuyerOrders { get; set; }
        public virtual ICollection<Order> SellerOrders { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Cart> CartItems { get; set; }
        public virtual ICollection<WithdrawRequest> WithdrawRequests { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; }
        public virtual ICollection<MessageReadStatus> MessageReadStatuses { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Follower> Followers { get; set; }
        public virtual ICollection<Follower> Following { get; set; }
        public virtual ICollection<UserActivityLog> ActivityLogs { get; set; }
        public virtual ICollection<OrderDispute> ReportedDisputes { get; set; }
        public virtual ICollection<OrderDispute> AssignedDisputes { get; set; }
        public virtual ICollection<OrderStatusHistory> StatusChanges { get; set; }
        public virtual ICollection<CurrencyConversionRate> SetRates { get; set; }
    }
}
