using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests.Asymmetric
{
    public class IssueID1EIKTests
    {
        [Fact]
        public void TestIssueID1EIK()
        {
            // 测试基本的 RSA 加密和解密功能
            var (publicKey, privateKey) = RSA.GenerateKeyPair();
            var rsa = new RSA(publicKey, privateKey);

            var plaintext = "Hello, IssueID1EIK!";
            var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext));
            var decrypted = rsa.Decrypt(encrypted);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(plaintext, Encoding.UTF8.GetString(decrypted));
        }
    }
}