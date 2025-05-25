using DollarProject.DbConnection;
using DollarProject.Enums;
using DollarProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị form
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string FullName, string Email, decimal AmountVND, int AmountXu, string TransactionType, string PaymentMethod)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);
            if (wallet == null)
            {
                return BadRequest("Wallet not found.");
            }

            int xuAmount = 0;
            decimal vndAmount = 0;

            if (TransactionType == "Deposit")
            {
                // Nhận VND, quy đổi sang Xu
                xuAmount = (int)(AmountVND / 1000);
                vndAmount = AmountVND;

                // Cộng vào số dư
                wallet.XuBalance += xuAmount;
            }
            else if (TransactionType == "Withdraw")
            {
                // Nhận Xu, quy đổi sang VND
                xuAmount = AmountXu;
                vndAmount = xuAmount * 1000;

                if (wallet.XuBalance < xuAmount)
                {
                    ModelState.AddModelError("", "Not enough Xu balance to withdraw.");
                    return View();
                }

                // Trừ khỏi số dư
                wallet.XuBalance -= xuAmount;
            }
            else
            {
                return BadRequest("Invalid transaction type.");
            }

            var transaction = new WalletTransaction
            {
                WalletID = wallet.WalletID,
                TransactionType = TransactionType,
                AmountVND = vndAmount,
                AmountXu = xuAmount,
                Description = $"Transaction by {FullName} ({Email})",
                PaymentMethod = PaymentMethod,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.WalletTransactions.Add(transaction);
            wallet.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["ShowThankYou"] = "True";
            return RedirectToAction("Index");
        }
    }
}
