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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var userId = int.Parse(User.FindFirstValue("UserId"));

                    if (dto == null || dto.ProductId <= 0)
                    {
                        TempData["Error"] = "Invalid order request.";
                        return RedirectToAction("Index", "Checkout");
                    }

                    // Lấy thông tin user wallet, product và admin
                    var userWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == dto.ProductId);
                    var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == 1); // Admin có ID = 1

                    if (userWallet == null || product == null || adminUser == null)
                    {
                        TempData["Error"] = "Required data not found.";
                        return RedirectToAction("Index", "Checkout");
                    }

                    if (userWallet.XuBalance < product.PriceXu)
                    {
                        TempData["Error"] = "Not enough Xu in wallet.";
                        return Json(new { success = false });
                    }

                    // Lấy hoặc tạo admin wallet
                    var adminWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == 1);
                    if (adminWallet == null)
                    {
                        adminWallet = new Wallet
                        {
                            UserID = 1,
                            XuBalance = 0,
                            LastUpdated = DateTime.Now
                        };
                        _context.Wallets.Add(adminWallet);
                        await _context.SaveChangesAsync();
                    }

                    // Lấy hoặc tạo seller wallet
                    var sellerWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == product.UserID);
                    if (sellerWallet == null)
                    {
                        sellerWallet = new Wallet
                        {
                            UserID = product.UserID,
                            XuBalance = 0,
                            LastUpdated = DateTime.Now
                        };
                        _context.Wallets.Add(sellerWallet);
                        await _context.SaveChangesAsync();
                    }

                    // Tính toán phân chia tiền
                    int totalAmount = product.PriceXu;
                    int adminShare = (int)(totalAmount * 0.3); // 30% cho admin
                    int sellerShare = totalAmount - adminShare; // 70% cho seller

                    // Trừ tiền từ ví user
                    userWallet.XuBalance -= totalAmount;
                    userWallet.LastUpdated = DateTime.Now;

                    // Cộng tiền vào ví admin và seller
                    adminWallet.XuBalance += adminShare;
                    adminWallet.LastUpdated = DateTime.Now;

                    sellerWallet.XuBalance += sellerShare;
                    sellerWallet.LastUpdated = DateTime.Now;

                    // Tạo đơn hàng
                    var order = new Order
                    {
                        UserID = userId,
                        SellerID = product.UserID,
                        OrderDate = DateTime.Now,
                        TotalPriceXu = totalAmount,
                        IsPaidWithWallet = true,
                        OrderStatus = StatusEnum.Completed.ToString(), // Đặt thành Completed ngay
                        DeliveryStatus = StatusEnum.Delivered.ToString(), // Đặt thành Delivered ngay
                        DeliveryMethod = DeliveryMethod.Automatic.ToString(),
                        DeliveryNotes = dto.ShippingAddress
                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    // Tạo chi tiết đơn hàng
                    var orderDetail = new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ProductID = product.ProductID,
                        Quantity = dto.Quantity > 0 ? dto.Quantity : 1,
                        UnitPriceXu = product.PriceXu
                    };
                    _context.OrderDetails.Add(orderDetail);

                    // Tạo transaction cho user (trừ tiền)
                    var userTransaction = new WalletTransaction
                    {
                        WalletID = userWallet.WalletID,
                        TransactionType = TransactionType.Purchase.ToString(),
                        AmountXu = -totalAmount, // Số âm vì trừ tiền
                        Description = $"Purchase of {product.ProductName}",
                        PaymentMethod = dto.PaymentMethod,
                        Status = StatusEnum.Completed.ToString(),
                        CreatedAt = DateTime.Now
                    };
                    _context.WalletTransactions.Add(userTransaction);

                    // Tạo transaction cho admin (cộng tiền)
                    var adminTransaction = new WalletTransaction
                    {
                        WalletID = adminWallet.WalletID,
                        TransactionType = TransactionType.AdminAdjust.ToString(),
                        AmountXu = adminShare,
                        Description = $"Admin commission from Order #{order.OrderID} - {product.ProductName}",
                        PaymentMethod = "System",
                        Status = StatusEnum.Completed.ToString(),
                        CreatedAt = DateTime.Now
                    };
                    _context.WalletTransactions.Add(adminTransaction);

                    // Tạo transaction cho seller (cộng tiền)
                    var sellerTransaction = new WalletTransaction
                    {
                        WalletID = sellerWallet.WalletID,
                        TransactionType = TransactionType.CompleteProduct.ToString(), // Hoặc có thể tạo enum Sale
                        AmountXu = sellerShare,
                        Description = $"Payment received for Order #{order.OrderID} - {product.ProductName}",
                        PaymentMethod = "System",
                        Status = StatusEnum.Completed.ToString(),
                        CreatedAt = DateTime.Now
                    };
                    _context.WalletTransactions.Add(sellerTransaction);

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

                    // Đánh dấu sản phẩm đã bán
                    product.IsSold = true;

                    // Lưu tất cả thay đổi
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Json(new
                    {
                        success = true,
                        message = $"Order completed successfully. Total: {totalAmount} Xu (Admin: {adminShare} Xu, Seller: {sellerShare} Xu)"
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            // Method này có thể không cần thiết nữa vì đã xử lý complete trong CreateOrder
            // Hoặc có thể dùng cho các trường hợp đặc biệt khác

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            if (order.OrderStatus == StatusEnum.Completed.ToString())
            {
                return Json(new { success = false, message = "Order already completed" });
            }

            // Cập nhật trạng thái nếu cần
            order.OrderStatus = StatusEnum.Completed.ToString();
            order.DeliveryStatus = StatusEnum.Delivered.ToString();

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Order status updated successfully" });
        }
    }
}