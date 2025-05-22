using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using DollarProject.DbConnection;
using DollarProject.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;
using DollarProject.Models;

namespace DollarProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Login
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_LoginPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Json(new { success = false, errors = new[] { "Invalid email or password." } });
            }

            if (model.Password != user.Password)
            {
                return Json(new { success = false, errors = new[] { "Invalid email or password." } });
            }

            try
            {
                var claims = new List<Claim>
                {
                    new Claim("UserId", user.UserID.ToString()),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim("ImageUrl", user.ImageURL ?? "avatar3.png"),
                    new Claim(ClaimTypes.Role, user.RoleID == 1 ? "Admin" : user.RoleID == 2 ? "Staff" : "Customer")
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return Json(new { success = true, redirectUrl = user.RoleID == 1 ? "/Admin/Dashboard" : user.RoleID == 2 ? "/Staff/Dashboard" : (returnUrl ?? "/") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { "An error occurred during sign in." } });
            }
        }
        #endregion


        [HttpGet]
        public IActionResult LoginFull(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("LoginFull", new LoginViewModel());
        }


        #region External Login
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string? returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError = null)
        {
            if (remoteError != null)
            {
                return Json(new { success = false, errors = new[] { $"Error from external provider: {remoteError}" } });
            }

            var info = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (info == null || !info.Succeeded)
            {
                return Json(new { success = false, errors = new[] { "External authentication failed." } });
            }

            // Lấy thông tin từ Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var givenName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var familyName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var picture = info.Principal.FindFirstValue("urn:google:picture");

            // Tìm hoặc tạo người dùng
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    FirstName = givenName,
                    LastName = familyName,
                    ImageURL = picture,
                    RoleID = 3, // Customer role
                    Password = null 
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            // Tạo claims
            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserID.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim("ImageUrl", user.ImageURL ?? "avatar3.png"),
                new Claim(ClaimTypes.Role, user.RoleID == 1 ? "Admin" : user.RoleID == 2 ? "Staff" : "Customer")
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            var redirectUrl = user.RoleID == 1 ? "/Admin/Dashboard" : user.RoleID == 2 ? "/Staff/Dashboard" : (returnUrl ?? "/");
            return Redirect(redirectUrl);
        }
        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}