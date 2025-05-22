using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5379));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5597), new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5598) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5601), new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5601) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5251));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5569));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5544));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5410));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5417));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5517));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5520));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5325));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsBlock" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5137), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsBlock" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5140), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsBlock" },
                values: new object[] { new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5142), null });

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5283));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5285));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5287));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 22, 15, 8, 27, 539, DateTimeKind.Local).AddTicks(5352));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(520));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(776), new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(780), new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(384));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(745));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "OrderStatusHistories",
                keyColumn: "HistoryID",
                keyValue: 1,
                column: "ChangedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(721));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(611));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(616));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(466));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(253));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(256));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(421));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 11, 31, 53, 246, DateTimeKind.Local).AddTicks(493));
        }
    }
}
