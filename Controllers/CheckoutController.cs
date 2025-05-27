using DollarProject.DbConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);

            ViewBag.ProductId = product?.ProductID;
            ViewBag.PriceXu = product?.PriceXu;
            ViewBag.WalletId = wallet?.WalletID;
            ViewBag.WalletBalance = wallet?.XuBalance;

            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
