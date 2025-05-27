using DollarProject.DbConnection;
using DollarProject.Models;
using DollarProject.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var user = await _context.Users
                .Include(u => u.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            var categories = await _context.ProductCategories
                .Where(p => p.ParentCategoryID == null)
                .ToListAsync();


            var viewModel = new ProductFormViewModel
            {
                Product = new Product(),
                Categories = categories
            };

            ViewBag.Categories = await _context.ProductCategories
                .Where(p => p.ParentCategoryID == null)
                .ToListAsync();

            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _context.ProductCategories
                .Where(p => p.ParentCategoryID == null)
                .ToListAsync();

            var viewModel = new ProductFormViewModel
            {
                Product = new Product(),
                Categories = categories
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([FromForm] Product product, IFormFile? imageFile)
        {
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var categories = await _context.ProductCategories
                .Where(p => p.ParentCategoryID == null)
                .ToListAsync();

            if (!ModelState.IsValid)
            {
                var vm = new ProductFormViewModel
                {
                    Product = product,
                    Categories = categories
                };

                return PartialView("~/Views/Product/_CreateProductPartial.cshtml", vm);
            }


            product.UserID = userId;
            product.CreatedAt = DateTime.Now;
            product.IsApproved = false;

            // Merge custom description
            var sb = new StringBuilder();
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
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public async Task<IActionResult> EditModal(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var categories = await _context.ProductCategories
                .Where(c => c.ParentCategoryID == null)
                .ToListAsync();

            var vm = new ProductFormViewModel
            {
                Product = product,
                Categories = categories
            };

            return PartialView("_EditProductPartial", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Product product, IFormFile? imageFile)
        {
            if (id != product.ProductID) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.ProductCategories
                    .Where(c => c.ParentCategoryID == null).ToListAsync();
                return View(product);
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            // Update basic fields
            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryID = product.CategoryID;
            existingProduct.PriceXu = product.PriceXu;
            existingProduct.Stock = product.Stock;
            existingProduct.ProductType = product.ProductType;
            existingProduct.AccountInfomation = product.AccountInfomation;

            // Update description with merge logic
            var sb = new StringBuilder();
            AppendField(sb, "Level", Request.Form["Level"]);
            AppendField(sb, "Skins/Items", Request.Form["Skins"]);
            if (!string.IsNullOrWhiteSpace(product.Description))
                sb.AppendLine($"\nOther Notes: {product.Description}");
            existingProduct.Description = sb.ToString();

            // Update image if provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var folderPath = Path.Combine("wwwroot/images");
                Directory.CreateDirectory(folderPath);
                var savePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(savePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                existingProduct.ImageURL = "/images/" + fileName;
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product deleted successfully.";
            return RedirectToAction("Index", "Profile");
        }

        private void AppendField(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendLine($"{label}: {value}");
        }
    }
}
