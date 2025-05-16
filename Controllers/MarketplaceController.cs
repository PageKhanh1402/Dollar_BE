using AutoMapper;
using DollarProject.DbConnection;
using DollarProject.Dto;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Seller)
                .Where(p => p.IsApproved)
                .AsQueryable();

            // Lọc theo danh mục nếu có
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            // Sắp xếp
            query = sortOrder switch
            {
                "popular" => query.OrderBy(p => p.PriceXu), // Popular: Giá thấp đến cao
                "popular-desc" => query.OrderByDescending(p => p.PriceXu), // Popular: Giá cao đến thấp
                _ => query.OrderBy(p => p.PriceXu) // Mặc định: Giá thấp đến cao
            };

            // Ánh xạ sang ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(query);

            // Lấy danh sách danh mục để hiển thị tab
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
