using System.Text;
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    public class Issue3512Tests
    {
        [Fact]
        public void TestIssue3512()
        {
            // 测试基本的加密功能
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);

            var plaintext = "Hello, Issue3512!";
            var encrypted = aes.EncryptHex(plaintext);
            var decrypted = aes.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }
    }
}