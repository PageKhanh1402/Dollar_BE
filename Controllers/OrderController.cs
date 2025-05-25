using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Enums;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateOrder([FromBody] OrderDetailDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid order data.");
            }

            int buyerId = 1; // Lấy từ User.Identity hoặc session trong thực tế
            int sellerId = 2; // Cố định hoặc lấy từ logic khác

            var order = new Order
            {
                UserID = buyerId,
                SellerID = sellerId,
                OrderDate = DateTime.Now,
                TotalPriceXu = dto.TotalPriceXu,
                IsPaidWithWallet = false,
                OrderStatus = StatusEnum.Pending.ToString(),
                DeliveryStatus = StatusEnum.Pending.ToString(),
                DeliveryMethod = DeliveryMethod.Automatic.ToString(),
                DeliveryNotes = dto.ShippingAddress
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order created successfully" });
        }

    }
}
