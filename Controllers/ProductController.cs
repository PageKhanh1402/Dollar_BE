using DollarProject.DbConnection;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace DollarProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product/Index
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var user = await _context.Users
                .Include(u => u.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
                return NotFound();

            ViewBag.Categories = await _context.ProductCategories
                .Where(c => c.ParentCategoryID == null)
                .ToListAsync();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.ProductCategories
                .Where(c => c.ParentCategoryID == null)
                .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([FromForm] Product product, IFormFile? imageFile)
        {
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var store = await _context.SellerStores.FirstOrDefaultAsync(s => s.SellerID == userId);
            if (store == null)
            {
                TempData["Error"] = "You must register a store before posting products.";
                return RedirectToAction("RegisterSeller", "SellerStore");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.ProductCategories
                    .Where(c => c.ParentCategoryID == null).ToListAsync();

                TempData["Error"] = "Invalid data. Please check the form.";
                return PartialView("~/Views/Product/_CreateProductPartial.cshtml", product);
            }

            product.SellerID = userId;
            product.StoreID = store.StoreID;
            product.CreatedAt = DateTime.Now;
            product.IsApproved = false;

            // Merge custom description
            var sb = new StringBuilder();
            AppendField(sb, "Game", Request.Form["GameName"]);
            AppendField(sb, "Level", Request.Form["Level"]);
            AppendField(sb, "Skins/Items", Request.Form["Skins"]);
            if (!string.IsNullOrWhiteSpace(product.Description))
                sb.AppendLine($"\nOther Notes: {product.Description}");

            product.Description = sb.ToString();
            product.AccountInfomation = Request.Form["AccountInfomation"];

            // Upload image
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var folderPath = Path.Combine("wwwroot/images");
                Directory.CreateDirectory(folderPath);
                var savePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                product.ImageURL = "/images/" + fileName;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product created successfully and pending approval.";
            return RedirectToAction("Index", "SellerStore");
        }

        private void AppendField(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendLine($"{label}: {value}");
        }
    }
}
