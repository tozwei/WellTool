using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests.Asymmetric
{
    public class IssueI6OQJATests
    {
        [Fact]
        public void TestIssueI6OQJA()
        {
            // 测试基本的 RSA 加密和解密功能
            var (publicKey, privateKey) = RSA.GenerateKeyPair();
            var rsa = new RSA(publicKey, privateKey);

            var plaintext = "Hello, IssueI6OQJA!";
            var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext));
            var decrypted = rsa.Decrypt(encrypted);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(plaintext, Encoding.UTF8.GetString(decrypted));
        }
    }
}