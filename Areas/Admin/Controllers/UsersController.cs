using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DollarProject.DbConnection;
using DollarProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace DollarProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string searchString, int? roleId, int pageNumber = 1, int pageSize = 5)
        {
            // Prepare the query
            var usersQuery = _context.Users.Include(u => u.Role).AsQueryable();

            // Search by name or email
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                usersQuery = usersQuery.Where(u => u.FirstName.ToLower().Contains(searchString) ||
                                                  u.LastName.ToLower().Contains(searchString) ||
                                                  u.Email.ToLower().Contains(searchString));
            }

            // Filter by role
            if (roleId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.RoleID == roleId.Value);
            }

            // Pagination
            int totalRecords = await usersQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages)); // Ensure pageNumber is within valid range

            var users = await usersQuery
                .OrderBy(u => u.UserID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Pass pagination and filter data to the view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchString = searchString;
            ViewBag.RoleId = roleId;

            // Populate roles for the dropdown
            ViewData["Roles"] = new SelectList(_context.Roles, "RoleID", "RoleName", roleId);

            return View(users);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,ImageURL,FirstName,LastName,Email,Username,Password,PhoneNumber,Address,RoleID,IsVerifiedSeller,SellerRating,SellerDescription,CreatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,ImageURL,FirstName,LastName,Email,Username,Password,PhoneNumber,Address,RoleID,IsVerifiedSeller,SellerRating,SellerDescription,CreatedAt")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Admin/Users/Block/5
        [HttpGet]
        public async Task<IActionResult> Block(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                user.IsBlock = true;
                _context.Update(user);
                await _context.SaveChangesAsync();
                TempData["UpdateUserSuccess"] = $"User [{user.FirstName} {user.LastName}] has been blocked successfully.";
            }
            catch (Exception ex)
            {
                TempData["UpdateUserError"] = $"An error occurred while blocking the user: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users/Unlock/5
        [HttpGet]
        public async Task<IActionResult> Unlock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                user.IsBlock = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
                TempData["UpdateUserSuccess"] = $"User [{user.FirstName} {user.LastName}] has been unblocked successfully.";
            }
            catch (Exception ex)
            {
                TempData["UpdateUserError"] = $"An error occurred while unblocking the user: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
