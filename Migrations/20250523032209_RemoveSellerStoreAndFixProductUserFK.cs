using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSellerStoreAndFixProductUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellerStores_StoreID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SellerStores");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellerID",
                table: "Products",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerID",
                table: "Products",
                newName: "IX_Products_UserID");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9908), new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9912), new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9912) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9628));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9885));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9867));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9783));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9839));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "AccountInfomation", "CreatedAt" },
                values: new object[] { "product1@gmail.com + Pass: 123456", new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9702) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "AccountInfomation", "CreatedAt" },
                values: new object[] { "product2@gmail.com + Pass: 123456", new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9705) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9496));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9499));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9651));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9652));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9654));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9730));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Products",
                newName: "SellerID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserID",
                table: "Products",
                newName: "IX_Products_SellerID");

            migrationBuilder.AddColumn<int>(
                name: "StoreID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SellerStores",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerID = table.Column<int>(type: "int", nullable: false),
                    BannerURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5549));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5692), new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5693) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5696), new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5696) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5674));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5579));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5582));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5630));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "AccountInfomation", "CreatedAt", "StoreID" },
                values: new object[] { null, new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5471), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "AccountInfomation", "CreatedAt", "StoreID" },
                values: new object[] { null, new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5475), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5315));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5317));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5498));

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreID",
                table: "Products",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerStores_SellerID",
                table: "SellerStores",
                column: "SellerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellerStores_StoreID",
                table: "Products",
                column: "StoreID",
                principalTable: "SellerStores",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerID",
                table: "Products",
                column: "SellerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
