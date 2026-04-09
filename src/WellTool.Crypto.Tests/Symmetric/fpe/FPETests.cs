using Xunit;
using WellTool.Crypto.Symmetric.Fpe;
using System.Text;

namespace WellTool.Crypto.Tests.Symmetric.FPE
{
    /// <summary>
    /// 格式保留加密测试
    /// </summary>
    public class FPETests
    {
        [Fact]
        public void EncryptDecryptTest()
        {
            var fpe = new FPEFF1(10); // 10进制
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var tweak = Encoding.UTF8.GetBytes("tweak");
            var data = Encoding.UTF8.GetBytes("123456");

            var encrypted = fpe.Encrypt(data, key, tweak);
            var decrypted = fpe.Decrypt(encrypted, key, tweak);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(data.Length, encrypted.Length);
            Assert.Equal(data.Length, decrypted.Length);
        }

        [Fact]
        public void EncryptDecryptStringTest()
        {
            var fpe = new FPEFF1(10); // 10进制
            var key = "1234567890123456";
            var tweak = "tweak";
            var data = "123456";

            var encrypted = fpe.Encrypt(data, key, tweak);
            var decrypted = fpe.Decrypt(encrypted, key, tweak);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(data.Length, encrypted.Length);
        }

        [Fact]
        public void DifferentRadixTest()
        {
            // 测试不同进制
            var fpe10 = new FPEFF1(10); // 10进制
            var fpe16 = new FPEFF1(16); // 16进制
            var key = "1234567890123456";
            var tweak = "tweak";
            var data = "123456";

            var encrypted10 = fpe10.Encrypt(data, key, tweak);
            var encrypted16 = fpe16.Encrypt(data, key, tweak);

            Assert.NotNull(encrypted10);
            Assert.NotNull(encrypted16);
            Assert.Equal(data.Length, encrypted10.Length);
            Assert.Equal(data.Length, encrypted16.Length);
        }
    }
}
