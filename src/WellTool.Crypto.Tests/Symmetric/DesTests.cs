using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// DES/3DES 对称加密测试
    /// </summary>
    public class DesTests
    {
        [Fact]
        public void DESEncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("12345678"); // 8 bytes key
            var iv = Encoding.UTF8.GetBytes("12345678"); // 8 bytes IV
            var des = new DES(key, iv);

            var plaintext = "Hello, DES!";
            var encrypted = des.EncryptHex(plaintext);
            var decrypted = des.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void DESedeEncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("123456789012345678901234"); // 24 bytes key
            var iv = Encoding.UTF8.GetBytes("12345678"); // 8 bytes IV
            var desede = new DESede(key, iv);

            var plaintext = "Hello, 3DES!";
            var encrypted = desede.EncryptHex(plaintext);
            var decrypted = desede.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
