using DollarProject.Library;
using DollarProject.Models;
using DollarProject.Models.VNPay;

namespace DollarProject.Services.Vnpay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var pay = new VnpayLibrary();

            var urlCallBack = _configuration["Vnpay:PaymentBackReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);

            // Chuyển amount sang VND (không có phần thập phân)
            var amount = ((long)model.Amount).ToString();
            pay.AddRequestData("vnp_Amount", amount + "00"); // Thêm 00 để convert sang xu

            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);

            var orderInfo = $"{model.Name} - {model.OrderDescription}";
            if (orderInfo.Length > 255)
            {
                orderInfo = orderInfo.Substring(0, 255);
            }
            pay.AddRequestData("vnp_OrderInfo", orderInfo);
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);

            // Sử dụng OrderId từ model làm TxnRef
            pay.AddRequestData("vnp_TxnRef", model.OrderId);

            var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            // Log thông tin để debug
            System.Diagnostics.Debug.WriteLine($"VNPay Request Data:");
            System.Diagnostics.Debug.WriteLine($"Amount: {amount}");
            System.Diagnostics.Debug.WriteLine($"OrderInfo: {orderInfo}");
            System.Diagnostics.Debug.WriteLine($"TxnRef: {model.OrderId}");
            System.Diagnostics.Debug.WriteLine($"ReturnUrl: {urlCallBack}");
            System.Diagnostics.Debug.WriteLine($"PaymentUrl: {paymentUrl}");

            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var pay = new VnpayLibrary();
                var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

                // Log response data
                System.Diagnostics.Debug.WriteLine("VNPay Response:");
                foreach (var key in collections.Keys)
                {
                    System.Diagnostics.Debug.WriteLine($"{key}: {collections[key]}");
                }

                return response;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"VNPay Error: {ex.Message}");
                return new PaymentResponseModel
                {
                    Success = false,
                    PaymentMethod = "VNPay",
                    OrderDescription = "Error processing payment",
                    OrderId = collections["vnp_TxnRef"].ToString(),
                    PaymentId = "",
                    TransactionId = "",
                    Token = "",
                    VnPayResponseCode = collections["vnp_ResponseCode"].ToString()
                };
            }
        }
    }
}