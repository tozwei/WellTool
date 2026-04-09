using System.Text;
using WellTool.Crypto;
using WellTool.Crypto.Digest;
using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// 国密综合测试（SM1/SM2/SM3/SM4）
    /// </summary>
    public class SmTests
    {
        [Fact]
        public void SM3Test()
        {
            var sm3 = new SM3();
            var data = "Hello, SM3!";
            var digest = sm3.DigestHex(data);
            Assert.NotNull(digest);
            Assert.NotEmpty(digest);
            Assert.Equal(64, digest.Length); // SM3 摘要长度为 32 字节，64 个十六进制字符
        }

        [Fact]
        public void SM3WithSaltTest()
        {
            var salt = Encoding.UTF8.GetBytes("salt");
            var sm3 = new SM3(salt);
            var data = "Hello, SM3 with salt!";
            var digest = sm3.DigestHex(data);
            Assert.NotNull(digest);
            Assert.NotEmpty(digest);
            Assert.Equal(64, digest.Length); // SM3 摘要长度为 32 字节，64 个十六进制字符
        }

        [Fact]
        public void SM4Test()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var sm4 = new SM4(key, iv);

            var plaintext = "Hello, SM4!";
            var encrypted = sm4.EncryptHex(plaintext);
            var decrypted = sm4.DecryptStr(encrypted);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
