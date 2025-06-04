using DollarProject.DbConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class WalletController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);
            if (wallet == null)
                return Json(new { balance = 0 });

            return Json(new { balance = wallet.XuBalance });
        }

    }
}
