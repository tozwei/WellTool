using System;
using System.Text;
using System.Security.Cryptography;

namespace WellTool.Jwt.Tests
{
    class VerifyJwtSignature
    {
        static void Main(string[] args)
        {
            // 测试使用的 JWT 令牌
            var token = "eyJhbGciOiJIUzI1NiJ9." +
                    "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
                    "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";

            // 测试使用的密钥
            var key = "1234567890";

            // 拆分令牌
            var parts = token.Split('.');
            var headerBase64 = parts[0];
            var payloadBase64 = parts[1];
            var signatureBase64 = parts[2];

            // 计算预期的签名
            var unsignedToken = $"{headerBase64}.{payloadBase64}";
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedToken));
                var expectedSignature = Base64UrlEncode(signatureBytes);

                // 比较签名
                Console.WriteLine($"Expected signature: {expectedSignature}");
                Console.WriteLine($"Actual signature: {signatureBase64}");
                Console.WriteLine($"Signatures match: {expectedSignature == signatureBase64}");
            }
        }

        private static string Base64UrlEncode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }
    }
}