using AutoMapper;
using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MarketplaceController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? categoryId, string? sortOrder = null)
        {
            string userIdClaim = User.FindFirstValue("UserId");
            int userId = Int32.Parse(userIdClaim);

            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.IsApproved)
                .AsQueryable();

            // Filter by category if provided
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            // Sort
            query = sortOrder switch
            {
                "popular" => query.OrderBy(p => p.PriceXu),
                "popular-desc" => query.OrderByDescending(p => p.PriceXu),
                _ => query.OrderBy(p => p.PriceXu)
            };

            // Get products
            var products = await query.ToListAsync();

            // Map to ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Update Wishlist status
            var wishlistProductIds = await _context.Wishlists
                .Where(w => w.UserID == userId)
                .Select(w => w.ProductID)
                .ToListAsync();

            foreach (var dto in productDtos)
            {
                dto.IsInWishlist = wishlistProductIds.Contains(dto.ProductID);
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
    }
}