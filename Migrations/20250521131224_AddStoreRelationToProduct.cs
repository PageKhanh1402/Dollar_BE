using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreRelationToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StoreID",
                table: "Products",
                type: "int",
                nullable: true);

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
                columns: new[] { "CreatedAt", "StoreID" },
                values: new object[] { new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3017), null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "StoreID" },
                values: new object[] { new DateTime(2025, 5, 21, 20, 12, 23, 933, DateTimeKind.Local).AddTicks(3020), null });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreID",
                table: "Products",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellerStores_StoreID",
                table: "Products",
                column: "StoreID",
                principalTable: "SellerStores",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellerStores_StoreID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoreID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
