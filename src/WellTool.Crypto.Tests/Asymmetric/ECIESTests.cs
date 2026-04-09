using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;
using Org.BouncyCastle.Crypto;

namespace WellTool.Crypto.Tests.Asymmetric
{
    /// <summary>
    /// ECIES 椭圆曲线集成加密方案测试
    /// </summary>
    public class ECIESTests
    {
        [Fact]
        public void TestECIES()
        {
            // 创建 ECIES 实例
            var ecies = new ECIES();

            // 生成密钥对
            var keyPair = ecies.GenerateKeyPair();

            // 测试数据
            var plaintext = "Hello, ECIES!";
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            // 加密
            var encrypted = ecies.Encrypt(plaintextBytes, keyPair.Public);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            // 解密
            var decrypted = ecies.Decrypt(encrypted, keyPair.Private);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);

            // 验证解密结果
            var decryptedText = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plaintext, decryptedText);
        }

        [Fact]
        public void TestECIESWithDifferentCurve()
        {
            // 创建 ECIES 实例，使用 P-384 曲线
            var ecies = new ECIES("P-384");

            // 生成密钥对
            var keyPair = ecies.GenerateKeyPair();

            // 测试数据
            var plaintext = "Hello, ECIES with P-384 curve!";
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            // 加密
            var encrypted = ecies.Encrypt(plaintextBytes, keyPair.Public);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            // 解密
            var decrypted = ecies.Decrypt(encrypted, keyPair.Private);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);

            // 验证解密结果
            var decryptedText = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plaintext, decryptedText);
        }
    }
}