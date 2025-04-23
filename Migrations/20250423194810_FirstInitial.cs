using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class FirstInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastMessageAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsVerifiedSeller = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SellerRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    SellerDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConversationParticipants",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeftAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationParticipants", x => x.ParticipantID);
                    table.ForeignKey(
                        name: "FK_ConversationParticipants_Conversations_ConversationID",
                        column: x => x.ConversationID,
                        principalTable: "Conversations",
                        principalColumn: "ConversationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationParticipants_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyConversionRates",
                columns: table => new
                {
                    RateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VNDtoXuRate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SetByUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyConversionRates", x => x.RateID);
                    table.ForeignKey(
                        name: "FK_CurrencyConversionRates_Users_SetByUserID",
                        column: x => x.SetByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Followers",
                columns: table => new
                {
                    FollowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowerID = table.Column<int>(type: "int", nullable: false),
                    SellerID = table.Column<int>(type: "int", nullable: false),
                    FollowedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followers", x => x.FollowID);
                    table.ForeignKey(
                        name: "FK_Followers_Users_FollowerID",
                        column: x => x.FollowerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Followers_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationID",
                        column: x => x.ConversationID,
                        principalTable: "Conversations",
                        principalColumn: "ConversationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RelatedEntityID = table.Column<int>(type: "int", nullable: true),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    SellerID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPriceXu = table.Column<int>(type: "int", nullable: false),
                    IsPaidWithWallet = table.Column<bool>(type: "bit", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefundReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceXu = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByUserID = table.Column<int>(type: "int", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_ApprovedByUserID",
                        column: x => x.ApprovedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerStores",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerID = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BannerURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerStores", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK_SellerStores_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActivityDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivityLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_UserActivityLogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    XuBalance = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletID);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AmountXu = table.Column<int>(type: "int", nullable: false),
                    AmountVND = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_WithdrawRequests_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReadStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReadStatuses", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK_MessageReadStatuses_Messages_MessageID",
                        column: x => x.MessageID,
                        principalTable: "Messages",
                        principalColumn: "MessageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReadStatuses_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDisputes",
                columns: table => new
                {
                    DisputeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ReportedByUserID = table.Column<int>(type: "int", nullable: false),
                    DisputeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedToStaffID = table.Column<int>(type: "int", nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDisputes", x => x.DisputeID);
                    table.ForeignKey(
                        name: "FK_OrderDisputes_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDisputes_Users_AssignedToStaffID",
                        column: x => x.AssignedToStaffID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDisputes_Users_ReportedByUserID",
                        column: x => x.ReportedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusHistories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedByUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusHistories", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatusHistories_Users_ChangedByUserID",
                        column: x => x.ChangedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AmountXu = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPriceXu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    SellerServiceRating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.WishlistID);
                    table.ForeignKey(
                        name: "FK_Wishlists_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletID = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AmountVND = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    AmountXu = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletID",
                        column: x => x.WalletID,
                        principalTable: "Wallets",
                        principalColumn: "WalletID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDeliveryDetails",
                columns: table => new
                {
                    DeliveryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailID = table.Column<int>(type: "int", nullable: false),
                    AccountUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GameServer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CharacterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeliveryInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryFailReason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeliveryDetails", x => x.DeliveryID);
                    table.ForeignKey(
                        name: "FK_ProductDeliveryDetails_OrderDetails_OrderDetailID",
                        column: x => x.OrderDetailID,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "ConversationID", "CreatedAt", "LastMessageAt", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(1027), new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(1028), "Customer Support Conversation" },
                    { 2, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(1030), new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(1031), "Seller to Customer Inquiry" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryID", "CategoryName", "Description", "ParentCategoryID" },
                values: new object[,]
                {
                    { 1, "Game Accounts", "Full game accounts", null },
                    { 2, "In-Game Items", "Virtual items for games", null },
                    { 3, "Game Currency", "In-game money and tokens", null },
                    { 4, "Game Keys/Codes", "Digital game keys and activation codes", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Staff" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryID", "CategoryName", "Description", "ParentCategoryID" },
                values: new object[,]
                {
                    { 5, "MMORPG Accounts", "Accounts for MMORPGs", 1 },
                    { 6, "MOBA Accounts", "Accounts for MOBAs", 1 },
                    { 7, "FPS Accounts", "Accounts for FPS games", 1 },
                    { 8, "Mobile Game Accounts", "Accounts for mobile games", 1 },
                    { 9, "Weapons", "In-game weapons", 2 },
                    { 10, "Armor", "In-game armor", 2 },
                    { 11, "Skins", "Character and weapon skins", 2 },
                    { 12, "Mounts", "In-game mounts and vehicles", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreatedAt", "Email", "FirstName", "ImageURL", "LastName", "Password", "PhoneNumber", "RoleID", "SellerDescription", "Username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(533), "admin@gamemarketplace.com", "Admin", null, "User", "Admin@123", null, 1, null, "admin" },
                    { 2, null, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(535), "staff@gamemarketplace.com", "Staff", null, "User", "Staff@123", null, 2, null, "staff" },
                    { 3, null, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(537), "customer@gamemarketplace.com", "Customer", null, "User", "Customer@123", null, 3, null, "customer" }
                });

            migrationBuilder.InsertData(
                table: "CurrencyConversionRates",
                columns: new[] { "RateID", "EffectiveFrom", "SetByUserID", "VNDtoXuRate" },
                values: new object[] { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(725), 1, 1000m });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationID", "Content", "CreatedAt", "IsRead", "NotificationType", "RelatedEntityID", "RelatedEntityType", "UserID" },
                values: new object[,]
                {
                    { 1, "Your order has been received!", new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(994), false, null, null, "Order", 3 },
                    { 2, "A new product has been approved.", new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(996), false, null, null, "Product", 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "DeliveredAt", "DeliveryMethod", "DeliveryNotes", "DeliveryStatus", "IsPaidWithWallet", "OrderDate", "OrderStatus", "RefundReason", "RefundStatus", "SellerID", "TotalPriceXu", "UserID" },
                values: new object[,]
                {
                    { 1, null, "Automatic", "Leave at the front door", "Pending", false, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(881), "Pending", null, null, 3, 100, 1 },
                    { 2, null, "Automatic", "Deliver after 5 PM", "Pending", false, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(890), "Pending", null, null, 3, 50, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "ApprovedByUserID", "CategoryID", "CreatedAt", "Description", "ImageURL", "IsApproved", "PriceXu", "ProductName", "ProductType", "Rating", "RejectionReason", "SellerID", "Stock" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(798), "Full MMORPG account.", "", false, 100, "Game Account A", "GameAccount", null, null, 1, 0 },
                    { 2, null, 9, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(800), "In-game weapon for RPG.", "", false, 50, "Weapon A", "GameItem", null, null, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "WalletID", "LastUpdated", "UserID", "XuBalance" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(755), 1, 1000 },
                    { 2, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(757), 2, 500 },
                    { 3, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(758), 3, 100 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "CartID", "AddedAt", "IsChecked", "ProductID", "Quantity", "UserID" },
                values: new object[] { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(850), null, 2, 0, 3 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailID", "OrderID", "ProductID", "Quantity", "UnitPriceXu" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 100 },
                    { 2, 2, 2, 1, 50 }
                });

            migrationBuilder.InsertData(
                table: "OrderStatusHistories",
                columns: new[] { "HistoryID", "ChangedAt", "ChangedByUserID", "NewStatus", "OrderID", "PreviousStatus" },
                values: new object[] { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(970), 1, "Pending", 1, null });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentID", "AmountXu", "OrderID", "PaymentDate", "PaymentMethod", "PaymentStatus" },
                values: new object[,]
                {
                    { 1, 150, 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(946), "VNPay", "Processing" },
                    { 2, 50, 2, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(948), "Wallet", "Processing" }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "WishlistID", "CreatedAt", "ProductID", "UserID" },
                values: new object[] { 1, new DateTime(2025, 4, 24, 2, 48, 9, 834, DateTimeKind.Local).AddTicks(825), 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductID",
                table: "CartItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserID_ProductID",
                table: "CartItems",
                columns: new[] { "UserID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipants_ConversationID",
                table: "ConversationParticipants",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipants_UserID",
                table: "ConversationParticipants",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyConversionRates_SetByUserID",
                table: "CurrencyConversionRates",
                column: "SetByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_FollowerID_SellerID",
                table: "Followers",
                columns: new[] { "FollowerID", "SellerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Followers_SellerID",
                table: "Followers",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadStatuses_MessageID",
                table: "MessageReadStatuses",
                column: "MessageID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadStatuses_UserID",
                table: "MessageReadStatuses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationID",
                table: "Messages",
                column: "ConversationID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDisputes_AssignedToStaffID",
                table: "OrderDisputes",
                column: "AssignedToStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDisputes_OrderID",
                table: "OrderDisputes",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDisputes_ReportedByUserID",
                table: "OrderDisputes",
                column: "ReportedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SellerID",
                table: "Orders",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_ChangedByUserID",
                table: "OrderStatusHistories",
                column: "ChangedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_OrderID",
                table: "OrderStatusHistories",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderID",
                table: "Payments",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentCategoryID",
                table: "ProductCategories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDeliveryDetails_OrderDetailID",
                table: "ProductDeliveryDetails",
                column: "OrderDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApprovedByUserID",
                table: "Products",
                column: "ApprovedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerID",
                table: "Products",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderID",
                table: "Reviews",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductID",
                table: "Reviews",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerStores_SellerID",
                table: "SellerStores",
                column: "SellerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivityLogs_UserID",
                table: "UserActivityLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserID",
                table: "Wallets",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletID",
                table: "WalletTransactions",
                column: "WalletID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductID",
                table: "Wishlists",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserID_ProductID",
                table: "Wishlists",
                columns: new[] { "UserID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawRequests_UserID",
                table: "WithdrawRequests",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ConversationParticipants");

            migrationBuilder.DropTable(
                name: "CurrencyConversionRates");

            migrationBuilder.DropTable(
                name: "Followers");

            migrationBuilder.DropTable(
                name: "MessageReadStatuses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderDisputes");

            migrationBuilder.DropTable(
                name: "OrderStatusHistories");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductDeliveryDetails");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SellerStores");

            migrationBuilder.DropTable(
                name: "UserActivityLogs");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "WithdrawRequests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
