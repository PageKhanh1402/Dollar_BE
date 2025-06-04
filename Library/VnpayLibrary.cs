﻿using DollarProject.Models.VNPay;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

//namespace DollarProject.Library
//{
//    public class VnpayLibrary
//    {
//        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
//        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());
//        public PaymentResponseModel GetFullResponseData(IQueryCollection collection, string hashSecret)
//        {
//            var vnPay = new VnpayLibrary();
//            foreach (var (key, value) in collection)
//            {
//                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
//                {
//                    vnPay.AddResponseData(key, value);
//                }
//            }
//            var orderId = Convert.ToInt64(vnPay.GetResponseData("vnp_TxnRef"));
//            var vnPayTranId = Convert.ToInt64(vnPay.GetResponseData("vnp_TransactionNo"));
//            var vnpResponseCode = vnPay.GetResponseData("vnp_ResponseCode");
//            var vnpSecureHash =
//                collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về
//            var orderInfo = vnPay.GetResponseData("vnp_OrderInfo");
//            var checkSignature =
//                vnPay.ValidateSignature(vnpSecureHash, hashSecret); //check Signature
//            if (!checkSignature)
//                return new PaymentResponseModel()
//                {
//                    Success = false
//                };
//            return new PaymentResponseModel()
//            {
//                Success = true,
//                PaymentMethod = "VnPay",
//                OrderDescription = orderInfo,
//                OrderId = orderId.ToString(),
//                PaymentId = vnPayTranId.ToString(),
//                TransactionId = vnPayTranId.ToString(),
//                Token = vnpSecureHash,
//                VnPayResponseCode = vnpResponseCode
//            };
//        }
//        public string GetIpAddress(HttpContext context)
//        {
//            var ipAddress = string.Empty;
//            try
//            {
//                var remoteIpAddress = context.Connection.RemoteIpAddress;

//                if (remoteIpAddress != null)
//                {
//                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
//                    {
//                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
//                            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
//                    }

//                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

//                    return ipAddress;
//                }
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }

//            return "127.0.0.1";
//        }
//        public void AddRequestData(string key, string value)
//        {
//            if (!string.IsNullOrEmpty(value))
//            {
//                _requestData.Add(key, value);
//            }
//        }
//        public void AddResponseData(string key, string value)
//        {
//            if (!string.IsNullOrEmpty(value))
//            {
//                _responseData.Add(key, value);
//            }
//        }
//        public string GetResponseData(string key)
//        {
//            return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
//        }
//        public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
//        {
//            var data = new StringBuilder();

//            foreach (var (key, value) in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
//            {
//                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
//            }

//            var querystring = data.ToString();

//            baseUrl += "?" + querystring;
//            var signData = querystring;
//            if (signData.Length > 0)
//            {
//                signData = signData.Remove(data.Length - 1, 1);
//            }

//            var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
//            baseUrl += "vnp_SecureHash=" + vnpSecureHash;

//            return baseUrl;
//        }
//        public bool ValidateSignature(string inputHash, string secretKey)
//        {
//            var rspRaw = GetResponseData();
//            var myChecksum = HmacSha512(secretKey, rspRaw);
//            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
//        }
//        private string HmacSha512(string key, string inputData)
//        {
//            var hash = new StringBuilder();
//            var keyBytes = Encoding.UTF8.GetBytes(key);
//            var inputBytes = Encoding.UTF8.GetBytes(inputData);
//            using (var hmac = new HMACSHA512(keyBytes))
//            {
//                var hashValue = hmac.ComputeHash(inputBytes);
//                foreach (var theByte in hashValue)
//                {
//                    hash.Append(theByte.ToString("x2"));
//                }
//            }

//            return hash.ToString();
//        }
//        private string GetResponseData()
//        {
//            var data = new StringBuilder();
//            if (_responseData.ContainsKey("vnp_SecureHashType"))
//            {
//                _responseData.Remove("vnp_SecureHashType");
//            }

//            if (_responseData.ContainsKey("vnp_SecureHash"))
//            {
//                _responseData.Remove("vnp_SecureHash");
//            }

//            foreach (var (key, value) in _responseData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
//            {
//                data.Append(WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(value) + "&");
//            }

//            //remove last '&'
//            if (data.Length > 0)
//            {
//                data.Remove(data.Length - 1, 1);
//            }

//            return data.ToString();
//        }


//    }

//}
//public class VnPayCompare : IComparer<string>
//{
//    public int Compare(string x, string y)
//    {
//        if (x == y) return 0;
//        if (x == null) return -1;
//        if (y == null) return 1;
//        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
//        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
//    }
//}


namespace DollarProject.Library
{
    public class VnpayLibrary
    {
        private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnpayCompare());
        private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnpayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
        }

        public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
        {
            var data = new StringBuilder();
            foreach (var kv in _requestData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            var queryString = data.ToString();

            baseUrl += "?" + queryString;
            var signData = queryString;
            if (signData.Length > 0)
            {
                signData = signData.Remove(data.Length - 1, 1);
            }

            var vnpSecureHash = HmacSha512(vnpHashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnpSecureHash;

            return baseUrl;
        }

        public bool ValidateSignature(string inputHash, string secretKey)
        {
            var rspRaw = GetResponseData();
            var myChecksum = HmacSha512(secretKey, rspRaw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private string HmacSha512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        private string GetResponseData()
        {
            var data = new StringBuilder();
            if (_responseData.ContainsKey("vnp_SecureHashType"))
            {
                _responseData.Remove("vnp_SecureHashType");
            }

            if (_responseData.ContainsKey("vnp_SecureHash"))
            {
                _responseData.Remove("vnp_SecureHash");
            }

            foreach (var kv in _responseData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }

            return data.ToString();
        }

        public string GetIpAddress(HttpContext context)
        {
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = System.Net.Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    }

                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                    return ipAddress;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting IP: {ex.Message}");
                return "127.0.0.1";
            }

            return "127.0.0.1";
        }

        public PaymentResponseModel GetFullResponseData(IQueryCollection collection, string hashSecret)
        {
            var vnpayData = new PaymentResponseModel();
            try
            {
                foreach (var (key, value) in collection)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        AddResponseData(key, value);
                    }
                }

                var orderId = Convert.ToString(GetResponseData("vnp_TxnRef"));
                var vnpayTranId = Convert.ToString(GetResponseData("vnp_TransactionNo"));
                var vnpResponseCode = GetResponseData("vnp_ResponseCode");
                var vnpSecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
                var orderInfo = GetResponseData("vnp_OrderInfo");
                var amount = Convert.ToInt64(GetResponseData("vnp_Amount")) / 100;

                var checkSignature = ValidateSignature(vnpSecureHash, hashSecret);

                if (checkSignature)
                {
                    vnpayData.OrderId = orderId;
                    vnpayData.PaymentId = vnpayTranId;
                    vnpayData.TransactionId = vnpayTranId;
                    vnpayData.OrderDescription = orderInfo;
                    vnpayData.VnPayResponseCode = vnpResponseCode;
                    vnpayData.Success = vnpResponseCode == "00";
                }
                else
                {
                    vnpayData.Success = false;
                    vnpayData.VnPayResponseCode = "97"; // Invalid signature
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"VNPay Response Error: {ex.Message}");
                vnpayData.Success = false;
                vnpayData.VnPayResponseCode = "99";
            }

            return vnpayData;
        }
    }

    public class VnpayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}