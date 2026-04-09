using System.Text;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// 国密 SM4 流模式测试
    /// </summary>
    public class Sm4StreamTests
    {
        [Fact]
        public void SM4CBCModeTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(CipherMode.CBC, Padding.PKCS5Padding, key, iv);

            var plaintext = "Hello, SM4 CBC Mode!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM4CFBModeTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(CipherMode.CFB, Padding.PKCS5Padding, key, iv);

            var plaintext = "Hello, SM4 CFB Mode!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM4OFBModeTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(CipherMode.OFB, Padding.PKCS5Padding, key, iv);

            var plaintext = "Hello, SM4 OFB Mode!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void SM4ECBModeTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(CipherMode.ECB, Padding.PKCS5Padding, key);

            var plaintext = "Hello, SM4 ECB Mode!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
