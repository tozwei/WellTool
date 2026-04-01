using WellTool.Crypto.Asymmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Asymmetric
{
    /// <summary>
    /// ECIES 椭圆曲线集成加密方案测试
    /// </summary>
    public class ECIESTests
    {
        [Fact]
        public void EncryptAndDecryptTest()
        {
            // ECIES 加密解密
            var keyPair = ECIES.GenerateKeyPair();
            var plaintext = "Hello ECIES!";

            var ciphertext = ECIES.Encrypt(keyPair.PublicKey, plaintext);
            var decrypted = ECIES.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void KeyPairGenerationTest()
        {
            // 生成密钥对
            var keyPair = ECIES.GenerateKeyPair();

            Assert.NotNull(keyPair);
            Assert.NotNull(keyPair.PrivateKey);
            Assert.NotNull(keyPair.PublicKey);
        }

        [Fact]
        public void LargeDataEncryptTest()
        {
            // 大数据加密测试
            var keyPair = ECIES.GenerateKeyPair();
            var largeData = new string('A', 1000);

            var ciphertext = ECIES.Encrypt(keyPair.PublicKey, largeData);
            var decrypted = ECIES.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void ChineseTextEncryptTest()
        {
            // 中文文本加密测试
            var keyPair = ECIES.GenerateKeyPair();
            var chineseText = "你好，ECIES 加密！";

            var ciphertext = ECIES.Encrypt(keyPair.PublicKey, chineseText);
            var decrypted = ECIES.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void EmptyDataEncryptTest()
        {
            // 空数据加密测试
            var keyPair = ECIES.GenerateKeyPair();
            var emptyData = "";

            var ciphertext = ECIES.Encrypt(keyPair.PublicKey, emptyData);
            var decrypted = ECIES.Decrypt(keyPair.PrivateKey, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void DifferentCurvesTest()
        {
            // 不同椭圆曲线测试
            var keyPairP256 = ECIES.GenerateKeyPair("P-256");
            var plaintext = "Test P-256 curve";

            var ciphertext = ECIES.Encrypt(keyPairP256.PublicKey, plaintext);
            var decrypted = ECIES.Decrypt(keyPairP256.PrivateKey, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
