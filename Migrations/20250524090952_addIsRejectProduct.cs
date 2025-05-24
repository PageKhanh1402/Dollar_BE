using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class addIsRejectProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(336));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(506), new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(508) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(510), new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(205));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(482));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(458));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(367));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(372));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(432));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(435));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsRejected" },
                values: new object[] { new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(281), false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsRejected" },
                values: new object[] { new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(285), false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(65));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(239));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(240));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(242));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(313));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Products");

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
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9702));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 10, 22, 6, 537, DateTimeKind.Local).AddTicks(9705));

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
        }
    }
}
