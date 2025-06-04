using Azure.Core;
using DollarProject.Models.VNPay;
using DollarProject.Services.Vnpay;
using Microsoft.AspNetCore.Mvc;

namespace DollarProject.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }

        //public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        //{
        //    var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

        //    return Redirect(url);
        //}

        //[HttpGet]
        //public IActionResult PaymentCallbackVnpay()
        //{
        //    var response = _vnPayService.PaymentExecute(Request.Query);

        //    return Json(response);
        //}
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(string FullName, string Email, decimal AmountVND, string OrderDescription, string OrderType, string Name)
        {
            // Debug log
            System.Diagnostics.Debug.WriteLine($"AmountVND: {AmountVND}");
            System.Diagnostics.Debug.WriteLine($"FullName: {FullName}");
            System.Diagnostics.Debug.WriteLine($"Email: {Email}");

            // Validate input data
            if (AmountVND <= 0)
            {
                TempData["ErrorMessage"] = "Please enter a valid amount.";
                return RedirectToAction("Index", "Transaction");
            }

            if (string.IsNullOrEmpty(FullName))
            {
                TempData["ErrorMessage"] = "Full name is required.";
                return RedirectToAction("Index", "Transaction");
            }

            try
            {
                var model = new PaymentInformationModel
                {
                    Amount = (double)AmountVND,
                    OrderDescription = string.IsNullOrEmpty(OrderDescription)
                        ? $"Deposit {AmountVND:N0} VND - {FullName}"
                        : OrderDescription,
                    OrderType = string.IsNullOrEmpty(OrderType) ? "deposit" : OrderType,
                    Name = FullName
                };

                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Payment service error. Please try again later.";
                return RedirectToAction("Index", "Transaction");
            }
        }

    }

}
