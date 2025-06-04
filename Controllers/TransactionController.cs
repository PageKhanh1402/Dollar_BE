using DollarProject.DbConnection;
using DollarProject.Enums;
using DollarProject.Models;
using DollarProject.Models.VNPay;
using DollarProject.Services.Vnpay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DollarProject.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVnPayService _vnPayService;

        public TransactionController(ApplicationDbContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);
            var transactions = await _context.WalletTransactions
                .Where(t => t.WalletID == wallet.WalletID)
                .OrderByDescending(t => t.CreatedAt)
                .Take(10)
                .ToListAsync();

            ViewBag.CurrentBalance = wallet?.XuBalance ?? 0;
            ViewBag.RecentTransactions = transactions;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string FullName, string Email, decimal AmountVND, int AmountXu, string TransactionType, string PaymentMethod)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue("UserId"));
                var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);

                if (wallet == null)
                {
                    TempData["ErrorMessage"] = "Wallet not found.";
                    return RedirectToAction("Index");
                }

                int xuAmount = 0;
                decimal vndAmount = 0;

                if (TransactionType == "Deposit")
                {
                    xuAmount = (int)(AmountVND / 1000);
                    vndAmount = AmountVND;
                }
                else if (TransactionType == "Withdraw")
                {
                    xuAmount = AmountXu;
                    vndAmount = xuAmount * 1000;

                    if (wallet.XuBalance < xuAmount)
                    {
                        TempData["ErrorMessage"] = "Not enough Xu balance to withdraw.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid transaction type.";
                    return RedirectToAction("Index");
                }

                var transaction = new WalletTransaction
                {
                    WalletID = wallet.WalletID,
                    TransactionType = TransactionType,
                    AmountVND = vndAmount,
                    AmountXu = xuAmount,
                    Description = $"{TransactionType} by {FullName} ({Email})",
                    PaymentMethod = PaymentMethod,
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                _context.WalletTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                if (PaymentMethod == "VNPay" && TransactionType == "Deposit")
                {
                    // Tạo OrderId duy nhất cho giao dịch
                    var orderId = $"{transaction.TransactionID}_{DateTime.Now:yyyyMMddHHmmss}";
                    HttpContext.Session.SetString("CurrentTransactionId", transaction.TransactionID.ToString());

                    var paymentModel = new PaymentInformationModel
                    {
                        Amount = (double)vndAmount, // Số tiền đã là VND
                        OrderDescription = transaction.Description,
                        OrderType = TransactionType.ToLower(),
                        Name = FullName,
                        OrderId = orderId
                    };

                    try
                    {
                        var url = _vnPayService.CreatePaymentUrl(paymentModel, HttpContext);
                        return Redirect(url);
                    }
                    catch (Exception ex)
                    {
                        transaction.Status = "Failed";
                        await _context.SaveChangesAsync();
                        TempData["ErrorMessage"] = "Failed to create payment URL. Please try again.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    await CompleteTransaction(transaction.TransactionID, true);
                    TempData["SuccessMessage"] = $"Transaction completed successfully! Amount: {(TransactionType == "Deposit" ? xuAmount + " Xu added" : xuAmount + " Xu withdrawn")}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your transaction.";
                return RedirectToAction("Index");
            }
        }

        private async Task<bool> CompleteTransaction(int transactionId, bool success)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var walletTransaction = await _context.WalletTransactions
                    .Include(t => t.Wallet)
                    .FirstOrDefaultAsync(t => t.TransactionID == transactionId);

                if (walletTransaction == null) return false;

                var wallet = await _context.Wallets
                    .FirstOrDefaultAsync(w => w.WalletID == walletTransaction.WalletID);

                if (wallet == null) return false;

                if (success)
                {
                    if (walletTransaction.TransactionType == "Deposit")
                    {
                        wallet.XuBalance += walletTransaction.AmountXu;
                    }
                    else if (walletTransaction.TransactionType == "Withdraw")
                    {
                        if (wallet.XuBalance < walletTransaction.AmountXu)
                        {
                            throw new Exception("Insufficient balance");
                        }
                        wallet.XuBalance -= walletTransaction.AmountXu;
                    }

                    walletTransaction.Status = "Completed";
                    walletTransaction.CreatedAt = DateTime.Now;
                }
                else
                {
                    walletTransaction.Status = "Failed";
                }

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                return false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentBalance()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue("UserId"));
                var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == userId);

                return Json(new { success = true, balance = wallet?.XuBalance ?? 0 });
            }
            catch
            {
                return Json(new { success = false, balance = 0 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCallbackVnpay()
        {
            try
            {
                var vnPayResponse = _vnPayService.PaymentExecute(Request.Query);

                // Log response data
                System.Diagnostics.Debug.WriteLine("VNPay Callback Response:");
                foreach (var key in Request.Query.Keys)
                {
                    System.Diagnostics.Debug.WriteLine($"{key}: {Request.Query[key]}");
                }

                var transactionIdStr = HttpContext.Session.GetString("CurrentTransactionId");
                if (!string.IsNullOrEmpty(transactionIdStr) && int.TryParse(transactionIdStr, out int transactionId))
                {
                    var transaction = await _context.WalletTransactions
                        .FirstOrDefaultAsync(t => t.TransactionID == transactionId);

                    if (transaction != null)
                    {
                        // Kiểm tra response code từ VNPay
                        var vnpResponseCode = Request.Query["vnp_ResponseCode"].ToString();
                        var isSuccess = vnpResponseCode == "00";

                        if (isSuccess)
                        {
                            var success = await CompleteTransaction(transactionId, true);
                            if (success)
                            {
                                TempData["SuccessMessage"] = "Payment completed successfully!";
                                HttpContext.Session.Remove("CurrentTransactionId");
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            transaction.Status = "Failed";
                            await _context.SaveChangesAsync();
                            TempData["ErrorMessage"] = $"Payment failed. VNPay Response Code: {vnpResponseCode}";
                        }
                    }
                }

                TempData["ErrorMessage"] = "Payment verification failed. Please contact support.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Payment Callback Error: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing the payment.";
                return RedirectToAction("Index");
            }
        }
    }
}