using AutoMapper;
using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Models;
using DollarProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WishlistController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? categoryId, string? sortOrder = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { showLogin = true });
            }

            string userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Index", "Home", new { showLogin = true });
            }

            // Query Wishlist
            var wishlistQuery = _context.Wishlists
                .Where(w => w.UserID == userId)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Category)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Seller)
                .Where(w => w.Product.IsApproved && w.Product.Category != null && w.Product.Seller != null)
                .Select(w => w.Product)
                .AsQueryable();

            // Filter by category
            if (categoryId.HasValue)
            {
                wishlistQuery = wishlistQuery.Where(p => p.CategoryID == categoryId.Value);
            }

            // Sort
            wishlistQuery = sortOrder switch
            {
                "popular" => wishlistQuery.OrderBy(p => p.PriceXu),
                "popular-desc" => wishlistQuery.OrderByDescending(p => p.PriceXu),
                _ => wishlistQuery.OrderBy(p => p.PriceXu)
            };

            // Get products
            var products = await wishlistQuery.ToListAsync();

            // Map to ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            foreach (var dto in productDtos)
            {
                dto.IsInWishlist = true; // All products in Wishlist
            }

            // Get categories
            var categories = await _context.ProductCategories
                .Select(c => new { c.CategoryID, c.CategoryName })
                .ToListAsync();

            ViewData["Categories"] = categories;
            ViewData["SortOrder"] = sortOrder;
            ViewData["CategoryId"] = categoryId;

            return View(productDtos);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleWishlist(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { showLogin = true });
            }

            string userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Index", "Home", new { showLogin = true });
            }

            var wishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserID == userId && w.ProductID == productId);

            bool isInWishlist;
            if (wishlistItem == null)
            {
                // Add to Wishlist
                _context.Wishlists.Add(new Wishlist
                {
                    UserID = userId,
                    ProductID = productId,
                    CreatedAt = DateTime.Now
                });
                isInWishlist = true;
            }
            else
            {
                // Remove from Wishlist
                _context.Wishlists.Remove(wishlistItem);
                isInWishlist = false;
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                isInWishlist,
                message = isInWishlist ? "Added to wishlist" : "Removed from wishlist"
            });
        }
    }
}