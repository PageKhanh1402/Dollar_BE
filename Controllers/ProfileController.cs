using DollarProject.DbConnection;
using DollarProject.Models;
using DollarProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfileController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdStr = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            int userId = int.Parse(userIdStr);

            var user = await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Include(u => u.Products)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.ProductCategories
                .Where(p => p.ParentCategoryID == null)
                .ToListAsync(); 

            return View(user);
        }


        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                SellerDescription = user.SellerDescription,
                ImageURL = user.ImageURL
            };

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Username = model.Username;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.SellerDescription = model.SellerDescription;

            if (model.ProfilePhoto != null && model.ProfilePhoto.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(model.ProfilePhoto.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ProfilePhoto", "Only image files (.jpg, .jpeg, .png, .gif) are allowed.");
                    return View(model);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await model.ProfilePhoto.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(user.ImageURL))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", user.ImageURL);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                user.ImageURL = fileName;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSeller(bool agreeTerms)
        {
            if (!agreeTerms)
            {
                TempData["ErrorMessage"] = "You must agree with terms to register.";
                return RedirectToAction("Index");
            }

            var userIdStr = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            int userId = int.Parse(userIdStr);
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            user.IsVerifiedSeller = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "You had register successful! Let sell something..";
            return RedirectToAction("Index");
        }

    }
}
