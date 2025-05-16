using DollarProject.Enums;
using DollarProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DollarProject.DbConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet properties for all entities
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SellerStore> SellerStores { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CurrencyConversionRate> CurrencyConversionRates { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<WithdrawRequest> WithdrawRequests { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductDeliveryDetail> ProductDeliveryDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderDispute> OrderDisputes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReadStatus> MessageReadStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships

            // User - Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Wallet relationship (one-to-one)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserID);

            // User - SellerStore relationship (one-to-one)
            modelBuilder.Entity<User>()
                .HasOne(u => u.SellerStore)
                .WithOne(s => s.Seller)
                .HasForeignKey<SellerStore>(s => s.SellerID);

            // User - Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.SellerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Product approval relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ApprovedByUser)
                .WithMany()
                .HasForeignKey(p => p.ApprovedByUserID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Order - User relationships (buyer and seller)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Buyer)
                .WithMany(u => u.BuyerOrders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Seller)
                .WithMany(u => u.SellerOrders)
                .HasForeignKey(o => o.SellerID)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderDetail - Order relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDispute relationships
            modelBuilder.Entity<OrderDispute>()
                .HasOne(d => d.ReportedByUser)
                .WithMany(u => u.ReportedDisputes)
                .HasForeignKey(d => d.ReportedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDispute>()
                .HasOne(d => d.AssignedToStaff)
                .WithMany(u => u.AssignedDisputes)
                .HasForeignKey(d => d.AssignedToStaffID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Message - User relationship
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            // Follower relationships
            modelBuilder.Entity<Follower>()
                .HasOne(f => f.FollowerUser)
                .WithMany(u => u.Following)
                .HasForeignKey(f => f.FollowerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.SellerUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.SellerID)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderStatusHistory - User relationship
            modelBuilder.Entity<OrderStatusHistory>()
                .HasOne(h => h.ChangedByUser)
                .WithMany(u => u.StatusChanges)
                .HasForeignKey(h => h.ChangedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // CurrencyConversionRate - User relationship
            modelBuilder.Entity<CurrencyConversionRate>()
                .HasOne(r => r.SetByUser)
                .WithMany(u => u.SetRates)
                .HasForeignKey(r => r.SetByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Set up unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => new { w.UserID, w.ProductID })
                .IsUnique();

            modelBuilder.Entity<Cart>()
                .HasIndex(c => new { c.UserID, c.ProductID })
                .IsUnique();

            modelBuilder.Entity<Follower>()
                .HasIndex(f => new { f.FollowerID, f.SellerID })
                .IsUnique();

            // Configure default values and constraints
            modelBuilder.Entity<User>()
                .Property(u => u.IsVerifiedSeller)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.SellerRating)
                .HasDefaultValue(0);

            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Staff" },
                new Role { RoleID = 3, RoleName = "User" }
            );

            // Seed Users for each Role
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@gamemarketplace.com",
                    Username = "admin",
                    Password = "Admin@123", // Should be hashed in production
                    RoleID = 1,  // Admin role
                    CreatedAt = DateTime.Now,
                    IsVerifiedSeller = true
                },
                new User
                {
                    UserID = 2,
                    FirstName = "Staff",
                    LastName = "User",
                    Email = "staff@gamemarketplace.com",
                    Username = "staff",
                    Password = "Staff@123", // Should be hashed in production
                    RoleID = 2,  // Staff role
                    CreatedAt = DateTime.Now,
                    IsVerifiedSeller = true
                },
                new User
                {
                    UserID = 3,
                    FirstName = "Customer",
                    LastName = "User",
                    Email = "customer@gamemarketplace.com",
                    Username = "customer",
                    Password = "Customer@123", // Should be hashed in production
                    RoleID = 3,  // Customer role
                    CreatedAt = DateTime.Now,
                    IsVerifiedSeller = true
                }
            );

            // Seed initial product categories
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryID = 1, CategoryName = "Game Accounts", Description = "Full game accounts" },
                new ProductCategory { CategoryID = 2, CategoryName = "In-Game Items", Description = "Virtual items for games" },
                new ProductCategory { CategoryID = 3, CategoryName = "Game Currency", Description = "In-game money and tokens" },
                new ProductCategory { CategoryID = 4, CategoryName = "Game Keys/Codes", Description = "Digital game keys and activation codes" }
            );

            // Create sub-categories for Game Accounts
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryID = 5, CategoryName = "MMORPG Accounts", Description = "Accounts for MMORPGs", ParentCategoryID = 1 },
                new ProductCategory { CategoryID = 6, CategoryName = "MOBA Accounts", Description = "Accounts for MOBAs", ParentCategoryID = 1 },
                new ProductCategory { CategoryID = 7, CategoryName = "FPS Accounts", Description = "Accounts for FPS games", ParentCategoryID = 1 },
                new ProductCategory { CategoryID = 8, CategoryName = "Mobile Game Accounts", Description = "Accounts for mobile games", ParentCategoryID = 1 }
            );

            // Create sub-categories for In-Game Items
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryID = 9, CategoryName = "Weapons", Description = "In-game weapons", ParentCategoryID = 2 },
                new ProductCategory { CategoryID = 10, CategoryName = "Armor", Description = "In-game armor", ParentCategoryID = 2 },
                new ProductCategory { CategoryID = 11, CategoryName = "Skins", Description = "Character and weapon skins", ParentCategoryID = 2 },
                new ProductCategory { CategoryID = 12, CategoryName = "Mounts", Description = "In-game mounts and vehicles", ParentCategoryID = 2 }
            );

            // Initial currency conversion rate
            modelBuilder.Entity<CurrencyConversionRate>().HasData(
                new CurrencyConversionRate
                {
                    RateID = 1,
                    VNDtoXuRate = 1000, // 1000 VND = 1 Xu
                    EffectiveFrom = DateTime.Now,
                    SetByUserID = 1
                }
            );

            // Create wallets for users
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    WalletID = 1,
                    UserID = 1,  // Admin's wallet
                    XuBalance = 1000,
                    LastUpdated = DateTime.Now
                },
                new Wallet
                {
                    WalletID = 2,
                    UserID = 2,  // Staff's wallet
                    XuBalance = 500,
                    LastUpdated = DateTime.Now
                },
                new Wallet
                {
                    WalletID = 3,
                    UserID = 3,  // Customer's wallet
                    XuBalance = 100,
                    LastUpdated = DateTime.Now
                }
            );

            

            // Seed some initial products (for both Admin and Staff)
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "Valorant Account",
                    Description = "Full MMORPG account.",
                    PriceXu = 100,
                    ProductType = Enums.ProductType.GameAccount.ToString(),
                    SellerID = 1, // Admin's store
                    CategoryID = 1, // Game Accounts category
                    CreatedAt = DateTime.Now,
                    IsApproved = true,
                    ImageURL = "marketplace5.png",

                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Mastering Mindset",
                    Description = "In-game weapon for RPG.",
                    PriceXu = 50,
                    ProductType = Enums.ProductType.GameItem.ToString(),
                    SellerID = 2, // Staff's store
                    CategoryID = 9, // Weapons category
                    CreatedAt = DateTime.Now,
                    IsApproved = true,
                    ImageURL = "marketplace6.png",
                }
            );

            // Seed Wishlist for customer
            modelBuilder.Entity<Wishlist>().HasData(
                new Wishlist
                {
                    WishlistID = 1,
                    UserID = 3,  // Customer's wishlist
                    ProductID = 1  // Game Account A
                }
            );

            // Seed Cart for customer
            modelBuilder.Entity<Cart>().HasData(
                new Cart
                {
                    CartID = 1,
                    UserID = 3,  // Customer's cart
                    ProductID = 2  // Weapon A
                }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderID = 1,
                    UserID = 1,  // Assuming UserID 1 exists (a Customer)
                    OrderStatus = StatusEnum.Pending.ToString(),
                    OrderDate = DateTime.Now,
                    TotalPriceXu = 100,
                    DeliveryNotes = "Leave at the front door",  // Providing the required value
                    SellerID = 3
                },
                new Order
                {
                    OrderID = 2,
                    UserID = 2,  // Assuming UserID 2 exists (another Customer)
                    OrderStatus = StatusEnum.Pending.ToString(),
                    OrderDate = DateTime.Now,
                    TotalPriceXu = 50,
                    DeliveryNotes = "Deliver after 5 PM",  // Providing the required value
                    SellerID = 3
                }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailID = 1, OrderID = 1, ProductID = 1, Quantity = 1, UnitPriceXu = 100 },
                new OrderDetail { OrderDetailID = 2, OrderID = 2, ProductID = 2, Quantity = 1, UnitPriceXu = 50 }
            );

            // Seed Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment { 
                    PaymentID = 1, 
                    OrderID = 1, 
                    PaymentMethod = PaymentMethod.VNPay.ToString(), 
                    PaymentStatus = StatusEnum.Processing.ToString(),
                    AmountXu = 150, 
                    PaymentDate = DateTime.Now },
                new Payment { 
                    PaymentID = 2, 
                    OrderID = 2, 
                    PaymentMethod = PaymentMethod.Wallet.ToString(),
                    PaymentStatus = StatusEnum.Processing.ToString(),
                    AmountXu = 50, 
                    PaymentDate = DateTime.Now }
            );

           

            // Seed initial order status history
            modelBuilder.Entity<OrderStatusHistory>().HasData(
                new OrderStatusHistory
                {
                    HistoryID = 1,
                    OrderID = 1,
                    NewStatus = StatusEnum.Pending.ToString(),
                    ChangedByUserID = 1, // Admin changed the status
                    ChangedAt = DateTime.Now
                }
            );

            // Seed Notifications (ensure all required properties are provided)
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    NotificationID = 1,
                    UserID = 3, // Customer (the user who receives the notification)
                    Content = "Your order has been received!",
                    CreatedAt = DateTime.Now,
                    RelatedEntityType = "Order" // Providing the required value
                },
                new Notification
                {
                    NotificationID = 2,
                    UserID = 2, // Admin (another user receiving notification)
                    Content = "A new product has been approved.",
                    CreatedAt = DateTime.Now,
                    RelatedEntityType = "Product" // Providing the required value
                }
            );
           

            // Seed Conversations
            modelBuilder.Entity<Conversation>().HasData(
                new Conversation
                {
                    ConversationID = 1,
                    Title = "Customer Support Conversation",
                    CreatedAt = DateTime.Now,
                    LastMessageAt = DateTime.Now
                },
                new Conversation
                {
                    ConversationID = 2,
                    Title = "Seller to Customer Inquiry",
                    CreatedAt = DateTime.Now,
                    LastMessageAt = DateTime.Now
                }
            );

            
        }


    }
}
