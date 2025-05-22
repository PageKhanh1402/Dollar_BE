using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DollarProject.DbConnection;
using DollarProject.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class SellerStoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SellerStoreController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirstValue("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return Challenge();

            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (currentUser == null)
                return Challenge();

            var sellerStore = await _context.SellerStores
                .Include(s => s.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(s => s.SellerID == userId);

            if (sellerStore == null)
                return RedirectToAction("RegisterSeller");

            ViewBag.Categories = await _context.ProductCategories
                .Where(c => c.ParentCategoryID == null)
                .ToListAsync();

            return View(sellerStore);
        }

        [HttpGet]
        [Authorize]
        public IActionResult RegisterSeller()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> RegisterSeller(SellerStore model, IFormFile LogoFile, IFormFile BannerFile)
        {
            var userIdStr = User.FindFirstValue("UserId");
            if (!int.TryParse(userIdStr, out int userId))
                return Challenge();

            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (currentUser == null)
                return Challenge();

            var existingStore = await _context.SellerStores.FirstOrDefaultAsync(s => s.SellerID == userId);
            if (existingStore != null)
            {
                ModelState.AddModelError("", "You have already registered as a seller.");
                return View(model);
            }

            // Save logo
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(LogoFile.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await LogoFile.CopyToAsync(stream);

                model.LogoURL = "/images/" + fileName;
            }

            // Save banner
            if (BannerFile != null && BannerFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(BannerFile.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await BannerFile.CopyToAsync(stream);

                model.BannerURL = "/images/" + fileName;
            }

            model.SellerID = userId;
            model.CreatedAt = DateTime.Now;
            model.IsVerified = false;

            _context.SellerStores.Add(model);
            currentUser.IsVerifiedSeller = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Store registered successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
