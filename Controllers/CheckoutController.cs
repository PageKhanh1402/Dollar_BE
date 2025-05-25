using DollarProject.DbConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var product = await _context.Products
                .Where(p => p.ProductID == productId)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.PriceXu
                }).FirstOrDefaultAsync();
            
            if (product == null) return NotFound();
          
            ViewBag.ProductId = product.ProductID;
            ViewBag.ProductName = product.ProductName;
            ViewBag.PriceXu = product.PriceXu;

            return View(product);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
