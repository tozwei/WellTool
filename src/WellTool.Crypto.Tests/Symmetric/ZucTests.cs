using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// 祖冲之算法测试（中国商用密码流加密算法）
    /// </summary>
    public class ZucTests
    {
        [Fact]
        public void EncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var zuc = new ZUC(key, iv);

            var plaintext = "Hello, ZUC!";
            var encrypted = zuc.EncryptHex(plaintext);
            var decrypted = zuc.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void StaticEncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var plaintext = Encoding.UTF8.GetBytes("Hello, ZUC!");

            var encrypted = ZUC.Encrypt(plaintext, key, iv);
            var decrypted = ZUC.Decrypt(encrypted, key, iv);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
