using AutoMapper;
using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Models;
using DollarProject.ViewModels;
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

            string userIdClaim = User.FindFirstValue("UserId");
            if (userIdClaim != null)
            {
                int userId = Int32.Parse(userIdClaim);
                // Update Wishlist status
                var wishlistProductIds = await _context.Wishlists
                    .Where(w => w.UserID == userId)
                    .Select(w => w.ProductID)
                    .ToListAsync();

                foreach (var dto in productDtos)
                {
                    dto.IsInWishlist = wishlistProductIds.Contains(dto.ProductID);
                }
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductID == id && p.IsApproved);

            if (product == null)
                return NotFound();

            var relatedProducts = await _context.Products
                .Where(p => p.UserID == product.UserID && p.ProductID != id && p.IsApproved)
                .Take(4)
                .ToListAsync();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                Seller = product.User,
                RelatedProducts = relatedProducts
            };

            return View("~/Views/Marketplace/ProductDetail.cshtml", viewModel);
        }
    }
}