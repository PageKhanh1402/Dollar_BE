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
            var currentUser = await _context.Users
                .Include(u => u.SellerStore)
                .ThenInclude(s => s.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(u => u.Username == User.Identity.Name);

            if (currentUser == null || currentUser.SellerStore == null)
            {
                return RedirectToAction("RegisterSeller");
            }
            ViewBag.Categories = await _context.ProductCategories
            .Where(c => c.ParentCategoryID == null)
            .ToListAsync();

            return View(currentUser.SellerStore);
        }

        [HttpGet]
        public IActionResult RegisterSeller()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSeller(SellerStore model, IFormFile LogoFile, IFormFile BannerFile)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Identity.Name);
            if (currentUser == null)
                return Unauthorized();

            var existingStore = await _context.SellerStores.FirstOrDefaultAsync(s => s.SellerID == currentUser.UserID);
            if (existingStore != null)
            {
                ModelState.AddModelError("", "You have already registered as a seller.");
                return View(model);
            }

            // Save logo image
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(LogoFile.FileName);
                var logoPath = Path.Combine("images", fileName);
                var savePath = Path.Combine(_environment.WebRootPath, logoPath);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }

                model.LogoURL = "/images/" + fileName;
            }

            // Save banner image
            if (BannerFile != null && BannerFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(BannerFile.FileName);
                var bannerPath = Path.Combine("images", fileName);
                var savePath = Path.Combine(_environment.WebRootPath, bannerPath);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await BannerFile.CopyToAsync(stream);
                }

                model.BannerURL = "/images/" + fileName;
            }


            model.SellerID = currentUser.UserID;
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
