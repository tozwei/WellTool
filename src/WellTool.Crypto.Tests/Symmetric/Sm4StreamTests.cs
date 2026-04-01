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
        public void EncryptAndDecryptTest()
        {
            // SM4 流模式加密解密
            var key = SM4.GenerateKey();
            var plaintext = "Hello SM4 Stream!";

            var ciphertext = SM4.EncryptStream(key, plaintext);
            var decrypted = SM4.DecryptStream(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void KeySizeTest()
        {
            // SM4 密钥大小测试（128 位）
            var key = SM4.GenerateKey();

            Assert.NotNull(key);
            Assert.Equal(16, key.Length); // 128 位=16 字节
        }

        [Fact]
        public void EmptyDataTest()
        {
            // SM4 流模式空数据测试
            var key = SM4.GenerateKey();
            var emptyData = "";

            var ciphertext = SM4.EncryptStream(key, emptyData);
            var decrypted = SM4.DecryptStream(key, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void LargeDataTest()
        {
            // SM4 流模式大数据测试
            var key = SM4.GenerateKey();
            var largeData = new string('A', 10000);

            var ciphertext = SM4.EncryptStream(key, largeData);
            var decrypted = SM4.DecryptStream(key, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void ChineseTextTest()
        {
            // SM4 流模式中文文本测试
            var key = SM4.GenerateKey();
            var chineseText = "你好，SM4 国密流加密！";

            var ciphertext = SM4.EncryptStream(key, chineseText);
            var decrypted = SM4.DecryptStream(key, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void CFBModeTest()
        {
            // SM4 CFB 模式测试
            var key = SM4.GenerateKey();
            var iv = SM4.GenerateIV();
            var plaintext = "CFB mode test";

            var ciphertext = SM4.EncryptCFB(key, iv, plaintext);
            var decrypted = SM4.DecryptCFB(key, iv, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void OFBModeTest()
        {
            // SM4 OFB 模式测试
            var key = SM4.GenerateKey();
            var iv = SM4.GenerateIV();
            var plaintext = "OFB mode test";

            var ciphertext = SM4.EncryptOFB(key, iv, plaintext);
            var decrypted = SM4.DecryptOFB(key, iv, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void CTRModeTest()
        {
            // SM4 CTR 模式测试
            var key = SM4.GenerateKey();
            var counter = SM4.GenerateCounter();
            var plaintext = "CTR mode test";

            var ciphertext = SM4.EncryptCTR(key, counter, plaintext);
            var decrypted = SM4.DecryptCTR(key, counter, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
