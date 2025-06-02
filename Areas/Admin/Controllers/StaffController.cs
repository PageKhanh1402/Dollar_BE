using DollarProject.DbConnection;
using DollarProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DollarProject.Areas.Admin.Controllers
{
    // StaffController.cs
    [Authorize(Roles = "Staff")]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SalesStatistics()
        {
            // Get all completed orders (based on DeliveryStatus)
            var soldOrders = await _context.OrderDetails
                .Include(od => od.Product)
                .ThenInclude(p => p.User)
                .Include(od => od.Order)
                .ThenInclude(o => o.Buyer)
                .Where(od => od.Product.IsSold && od.Order.DeliveryStatus == "Delivered")
                .ToListAsync();

            // Group by month
            var monthlySales = soldOrders
                .GroupBy(x => x.Order.OrderDate.ToString("yyyy-MM"))
                .Select(g => new
                {
                    Month = g.Key,
                    SoldCount = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();

            var chartLabels = monthlySales.Select(x => x.Month).ToList();
            var chartValues = monthlySales.Select(x => x.SoldCount).ToList();

            var viewModel = new SalesStatsViewModel
            {
                Labels = chartLabels,
                Values = chartValues,
                SaleRecords = soldOrders.Select(o => new SalesStatsViewModel.SaleRecord
                {
                    ProductName = o.Product.ProductName,
                    BuyerName = o.Order.Buyer.Username,
                    SellerName = o.Product.User.Username,
                    PriceXu = o.Product.PriceXu,
                    Date = o.Order.OrderDate,
                    Status = o.Order.DeliveryStatus
                }).ToList()
            };

            return View(viewModel);
        }
    }

}
