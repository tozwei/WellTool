using System;
using System.Text;
using WellTool.JWT;
using WellTool.JWT.Signers;

namespace TestJwt
{
    class Program
    {
        static void Main(string[] args)
        {
            // 测试 JWTUtilTest.VerifyTokenTest 中的令牌和密钥
            var token1 = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                    "eyJ1c2VyX25hbWUiOiJhZG1pbiIsInNjb3BlIjpbImFsbCJdLCJleHAiOjE2MjQwMDQ4MjIsInVzZXJJZCI6MSwiYXV0aG9yaXRpZXMiOlsiUk9MRV_adminIiwiU1lfTUVOVV8xIiwiUk9MRV91c2VyIiwiU1lfTUVOVV8yIl0sImp0aSI6ImQ0YzVlYjgwLTA5ZTctNGU0ZC1hZTg3LTVkNGI5M2FhNmFiNiIsImNsaWVudF9pZCI6ImhhbmR5LXNob3AifQ." +
                    "aixF1eKlAKS_k3ynFnStE7-IRGiD5YaqznvK2xEjBew";
            var secret1 = "123456";

            // 测试 JWTValidatorTest.ValidateTest 中的令牌和密钥
            var token2 = "eyJhbGciOiJIUzI1NiJ9." +
                    "eyJzdWIiOiIxMjM0NTY3ODkwIiwiYWRtaW4iOnRydWUsIm5hbWUiOiJsb29seSJ9." +
                    "U2aQkC2THYV9L0fTN-yBBI7gmo5xhmvMhATtu8v0zEA";
            var secret2 = "1234567890";

            Console.WriteLine("Testing JWTUtilTest.VerifyTokenTest...");
            TestToken(token1, secret1);

            Console.WriteLine("\nTesting JWTValidatorTest.ValidateTest...");
            TestToken(token2, secret2);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void TestToken(string token, string secret)
        {
            try
            {
                var jwt = JWT.Of(token);
                Console.WriteLine($"Algorithm: {jwt.GetAlgorithm()}");
                
                var signer = JwtSignerUtil.CreateSigner(jwt.GetAlgorithm() ?? "HS256", secret);
                Console.WriteLine($"Signer algorithm: {signer.GetAlgorithmId()}");

                var tokens = jwt.GetTokens();
                var headerBase64 = tokens[0];
                var payloadBase64 = tokens[1];
                var signatureBase64 = tokens[2];

                Console.WriteLine($"Header: {headerBase64}");
                Console.WriteLine($"Payload: {payloadBase64}");
                Console.WriteLine($"Signature: {signatureBase64}");

                var expectedSignature = signer.Sign(headerBase64, payloadBase64);
                Console.WriteLine($"Expected signature: {expectedSignature}");
                Console.WriteLine($"Signature match: {expectedSignature == signatureBase64}");

                var verify = jwt.SetKey(secret).Verify();
                Console.WriteLine($"Verify result: {verify}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}