using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DollarProject.Migrations
{
    /// <inheritdoc />
    public partial class updateorderhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStatusHistories");

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    TotalPriceXu = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedByUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Users_ChangedByUserID",
                        column: x => x.ChangedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "CartID",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7568));

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7777), new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7778) });

            migrationBuilder.UpdateData(
                table: "Conversations",
                keyColumn: "ConversationID",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LastMessageAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7781), new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7782) });

            migrationBuilder.UpdateData(
                table: "CurrencyConversionRates",
                keyColumn: "RateID",
                keyValue: 1,
                column: "EffectiveFrom",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "NotificationID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7748));

            migrationBuilder.InsertData(
                table: "OrderHistories",
                columns: new[] { "HistoryID", "ChangedAt", "ChangedByUserID", "NewStatus", "OrderID", "PreviousStatus", "TotalPriceXu" },
                values: new object[] { 1, new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7725), 1, "Pending", 1, null, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderID",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7700));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentID",
                keyValue: 2,
                column: "PaymentDate",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7702));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7522));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7486));

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "WalletID",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7487));

            migrationBuilder.UpdateData(
                table: "Wishlists",
                keyColumn: "WishlistID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 21, 49, 852, DateTimeKind.Local).AddTicks(7545));

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_ChangedByUserID",
                table: "OrderHistories",
                column: "ChangedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderID",
                table: "OrderHistories",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.CreateTable(
                name: "OrderStatusHistories",
                columns: table => new
                {
                    HistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangedByUserID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreviousStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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

            migrationBuilder.InsertData(
                table: "OrderStatusHistories",
                columns: new[] { "HistoryID", "ChangedAt", "ChangedByUserID", "NewStatus", "OrderID", "PreviousStatus" },
                values: new object[] { 1, new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(458), 1, "Pending", 1, null });

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
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(281));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 16, 9, 51, 921, DateTimeKind.Local).AddTicks(285));

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_ChangedByUserID",
                table: "OrderStatusHistories",
                column: "ChangedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusHistories_OrderID",
                table: "OrderStatusHistories",
                column: "OrderID");
        }
    }
}
