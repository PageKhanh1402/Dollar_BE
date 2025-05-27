using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DollarProject.DbConnection;
using DollarProject.Models;
using System.Security.Claims;

namespace DollarProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Pending Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products
                .Include(p => p.ApprovedByUser).Include(p => p.Category).Include(p => p.User)
                .Where(p => !p.IsApproved && !p.IsRejected);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/All Posts
        public async Task<IActionResult> AllPost()
        {
            var applicationDbContext = _context.Products
                .Include(p => p.ApprovedByUser).Include(p => p.Category).Include(p => p.User)
                .Where(p => p.IsApproved);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Posts/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Approve the product
            product.IsApproved = true;
            product.IsRejected = false;
            product.RejectionReason = null;
            var userId = int.Parse(User.FindFirstValue("UserId"));
            product.ApprovedByUserID = userId;

            await _context.SaveChangesAsync();

            TempData["success"] = "Product approved successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Posts/Reject/5
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Posts/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string rejectionReason)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Reject the product
            product.IsApproved = false;
            product.IsRejected = true;
            product.RejectionReason = rejectionReason;
            var userId = int.Parse(User.FindFirstValue("UserId"));
            product.ApprovedByUserID = userId;

            await _context.SaveChangesAsync();

            TempData["success"] = "Product rejected successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                TempData["error"] = "Product not found.";
                return RedirectToAction(nameof(AllPost));
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction(nameof(AllPost));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
