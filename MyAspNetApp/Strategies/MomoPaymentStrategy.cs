using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MyAspNetApp.Strategies
{
    public class MomoPaymentStrategy : IPaymentStrategy
    {
        private readonly IConfiguration _configuration;

        public MomoPaymentStrategy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<object> CreatePaymentIntent(decimal amount, int orderId)
        {
            string endpoint = _configuration["Momo:MomoApiUrl"];
            string partnerCode = _configuration["Momo:PartnerCode"];
            string accessKey = _configuration["Momo:AccessKey"];
            string secretKey = _configuration["Momo:SecretKey"];
            string requestId = Guid.NewGuid().ToString();
            string redirectUrl = _configuration["Momo:RedirectUrl"];
            string ipnUrl = _configuration["Momo:IpnUrl"];
            string orderInfo = $"Payment for order #0000{orderId}";
            string amountStr = ((int)amount).ToString();
            string momoOrderId = $"{orderId}_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";

            string rawHash = $"accessKey={accessKey}&amount={amountStr}&extraData=&ipnUrl={ipnUrl}&orderId={momoOrderId}&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType=captureWallet";

            string signature = HmacSHA256(rawHash, secretKey);

            var body = new
            {
                partnerCode,
                accessKey,
                requestId,
                amount = amountStr,
                orderId = momoOrderId,
                orderInfo,
                redirectUrl,
                ipnUrl,
                extraData = "",
                requestType = "captureWallet",
                signature,
                lang = "en"
            };

            using var http = new HttpClient();
            var response = await http.PostAsJsonAsync(endpoint, body);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Momo API call failed: {response.StatusCode}, Content: {errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Momo Raw Response: " + json);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("payUrl", out var payUrlElement))
                throw new Exception("payUrl not found in Momo response.");

            return new
            {
                PayUrl = payUrlElement.GetString(),
                Message = root.TryGetProperty("message", out var messageElement) ? messageElement.GetString() : null,
                Code = root.TryGetProperty("resultCode", out var codeElement) ? codeElement.GetInt32() : (int?)null
            };
        }

        private string HmacSHA256(string message, string key)
        {
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            using var hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
        }

        private class MomoResponse
        {
            public string PayUrl { get; set; }
        }
    }
}
