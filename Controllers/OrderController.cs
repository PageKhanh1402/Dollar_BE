using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Enums;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Transactions;

namespace DollarProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] OrderDetailDto dto)
        {
            var userId = int.Parse(User.FindFirstValue("UserId")); // Giả định user đã đăng nhập

            if (dto == null || dto.ProductId <= 0)
            {
                TempData["Error"] = "Invalid order request.";
                return RedirectToAction("Index", "Checkout");
            }

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == dto.ProductId);

            if (wallet == null || product == null)
            {
                TempData["Error"] = "Wallet or product not found.";
                return RedirectToAction("Index", "Checkout");
            }

            if (wallet.XuBalance < product.PriceXu)
            {
                TempData["Error"] = "Not enough Xu in wallet.";
                return Json(new { success = false });
            }

            // Trừ xu và cập nhật ví
            wallet.XuBalance -= product.PriceXu;
            wallet.LastUpdated = DateTime.Now;

            // Tạo đơn hàng
            var order = new Order
            {
                UserID = userId,
                SellerID = product.UserID,
                OrderDate = DateTime.Now,
                TotalPriceXu = product.PriceXu,
                IsPaidWithWallet = true,
                OrderStatus = StatusEnum.Pending.ToString(),
                DeliveryStatus = StatusEnum.Pending.ToString(),
                DeliveryMethod = DeliveryMethod.Automatic.ToString(),
                DeliveryNotes = dto.ShippingAddress
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Tạo chi tiết đơn hàng (OrderDetail)
            var orderDetail = new OrderDetail
            {
                OrderID = order.OrderID,
                ProductID = product.ProductID,
                Quantity = dto.Quantity > 0 ? dto.Quantity : 1, // fallback nếu chưa truyền
                UnitPriceXu = product.PriceXu
            };

            _context.OrderDetails.Add(orderDetail);

            // Thêm OrderHistory
            var orderHistory = new OrderHistory
            {
                OrderID = order.OrderID,
                PreviousStatus = null,
                NewStatus = order.OrderStatus,
                ChangedAt = DateTime.Now,
                ChangedByUserID = userId,
                TotalPriceXu = order.TotalPriceXu
            };

            _context.OrderHistories.Add(orderHistory);

            // Ghi nhận giao dịch ví
            var transaction = new WalletTransaction
            {
                WalletID = wallet.WalletID,
                TransactionType = TransactionType.Purchase.ToString(),
                AmountXu = product.PriceXu,
                Description = $"Purchase of {product.ProductName}",
                PaymentMethod = dto.PaymentMethod,
                Status = StatusEnum.Completed.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            _context.WalletTransactions.Add(transaction);

            // Save tất cả
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }


    }
}
