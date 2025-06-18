using DollarProject.DbConnection;
using DollarProject.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class OrderHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderHistory()
        {
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Json(new List<OrderDetailDto>());
            }

            var orderDetails = await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                    .ThenInclude(o => o.Payments)
                .Where(od => od.Order.UserID == userId)
                .OrderByDescending(od => od.Order.OrderDate)
                .ToListAsync();

            var result = orderDetails.Select((od, index) => new OrderDetailDto
            {
                Index = index + 1,
                ProductId = od.ProductID,
                ProductName = od.Product?.ProductName ?? "Unknown Product",
                UnitPriceXu = od.UnitPriceXu,
                Quantity = od.Quantity,
                TotalPriceXu = od.UnitPriceXu * od.Quantity,
                OrderDate = od.Order.OrderDate,
                OrderStatus = od.Order.OrderStatus,
                DeliveryStatus = od.Order.DeliveryStatus,
                FullName = od.Order.Buyer?.FirstName ?? "Unknown",
                Email = od.Order.Buyer?.Email ?? "Unknown",
                ShippingAddress = od.Order.DeliveryNotes ?? "N/A",
                PaymentMethod = od.Order.Payments.FirstOrDefault()?.PaymentMethod ?? "Unknown",
                AccountInfomation = od.Product?.AccountInfomation ?? "N/A"
            }).ToList();

            return Json(result);
        }
    }
}
