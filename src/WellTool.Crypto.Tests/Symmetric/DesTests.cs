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
        public void DesEncryptAndDecryptTest()
        {
            // DES 加密解密
            var key = DES.GenerateKey();
            var plaintext = "Hello DES!";

            var ciphertext = DES.Encrypt(key, plaintext);
            var decrypted = DES.Decrypt(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void TripleDesEncryptAndDecryptTest()
        {
            // 3DES 加密解密
            var key = DES.GenerateKey(168); // 3DES 使用 168 位密钥
            var plaintext = "Hello 3DES!";

            var ciphertext = DES.Encrypt3DES(key, plaintext);
            var decrypted = DES.Decrypt3DES(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void DesKeySizeTest()
        {
            // DES 密钥大小测试
            var key56 = DES.GenerateKey(56);
            Assert.NotNull(key56);

            var key64 = DES.GenerateKey(64);
            Assert.NotNull(key64);
        }

        [Fact]
        public void TripleDesKeySizeTest()
        {
            // 3DES 密钥大小测试
            var key168 = DES.GenerateKey(168);
            Assert.NotNull(key168);

            var key192 = DES.GenerateKey(192);
            Assert.NotNull(key192);
        }

        [Fact]
        public void DesEmptyDataTest()
        {
            // DES 空数据测试
            var key = DES.GenerateKey();
            var emptyData = "";

            var ciphertext = DES.Encrypt(key, emptyData);
            var decrypted = DES.Decrypt(key, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void DesLargeDataTest()
        {
            // DES 大数据测试
            var key = DES.GenerateKey();
            var largeData = new string('A', 1000);

            var ciphertext = DES.Encrypt(key, largeData);
            var decrypted = DES.Decrypt(key, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void DesChineseTextTest()
        {
            // DES 中文文本测试
            var key = DES.GenerateKey();
            var chineseText = "你好，DES 加密！";

            var ciphertext = DES.Encrypt(key, chineseText);
            var decrypted = DES.Decrypt(key, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void DesDeterministicTest()
        {
            // DES 确定性测试 - 相同输入产生相同输出
            var key = DES.GenerateKey();
            var plaintext = "test input";

            var ciphertext1 = DES.Encrypt(key, plaintext);
            var ciphertext2 = DES.Encrypt(key, plaintext);

            Assert.Equal(ciphertext1, ciphertext2);
        }

        [Fact]
        public void DesDifferentKeysTest()
        {
            // DES 不同密钥加密不同
            var key1 = DES.GenerateKey();
            var key2 = DES.GenerateKey();
            var plaintext = "test data";

            var ciphertext1 = DES.Encrypt(key1, plaintext);
            var ciphertext2 = DES.Encrypt(key2, plaintext);

            Assert.NotEqual(ciphertext1, ciphertext2);
        }

        [Fact]
        public void DesCBCTest()
        {
            // DES CBC 模式测试
            var key = DES.GenerateKey();
            var iv = DES.GenerateIV();
            var plaintext = "CBC mode test";

            var ciphertext = DES.EncryptCBC(key, iv, plaintext);
            var decrypted = DES.DecryptCBC(key, iv, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void DesECBTest()
        {
            // DES ECB 模式测试
            var key = DES.GenerateKey();
            var plaintext = "ECB mode test";

            var ciphertext = DES.EncryptECB(key, plaintext);
            var decrypted = DES.DecryptECB(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
