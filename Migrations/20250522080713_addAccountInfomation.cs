using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class addAccountInfomation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountInfomation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "AccountInfomation", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5471) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "AccountInfomation", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 5, 22, 15, 7, 11, 123, DateTimeKind.Local).AddTicks(5475) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountInfomation",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3213), new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3214) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3216), new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3217) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2950));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3190));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3191));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3174));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3094));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3149));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3017));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2834));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2980));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(2982));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3047));
        }
    }
}
