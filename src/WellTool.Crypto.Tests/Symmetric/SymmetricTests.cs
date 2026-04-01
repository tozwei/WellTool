using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// 对称加密通用测试
    /// </summary>
    public class SymmetricTests
    {
        [Fact]
        public void AESBasicTest()
        {
            // AES 基础加密测试
            var key = AES.GenerateKey();
            var plaintext = "Hello AES!";

            var ciphertext = AES.Encrypt(key, plaintext);
            var decrypted = AES.Decrypt(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void AESDifferentModesTest()
        {
            // AES 不同模式测试
            var key = AES.GenerateKey();
            var plaintext = "Different modes test";

            // ECB 模式
            var cipherECB = AES.EncryptECB(key, plaintext);
            var plainECB = AES.DecryptECB(key, cipherECB);
            Assert.Equal(plaintext, plainECB);

            // CBC 模式
            var iv = AES.GenerateIV();
            var cipherCBC = AES.EncryptCBC(key, iv, plaintext);
            var plainCBC = AES.DecryptCBC(key, iv, cipherCBC);
            Assert.Equal(plaintext, plainCBC);

            // CFB 模式
            var cipherCFB = AES.EncryptCFB(key, iv, plaintext);
            var plainCFB = AES.DecryptCFB(key, iv, cipherCFB);
            Assert.Equal(plaintext, plainCFB);
        }

        [Fact]
        public void AESKeySizesTest()
        {
            // AES 不同密钥长度测试
            var plaintext = "Test different key sizes";

            var key128 = AES.GenerateKey(128);
            var cipher128 = AES.Encrypt(key128, plaintext);
            Assert.Equal(plaintext, AES.Decrypt(key128, cipher128));

            var key192 = AES.GenerateKey(192);
            var cipher192 = AES.Encrypt(key192, plaintext);
            Assert.Equal(plaintext, AES.Decrypt(key192, cipher192));

            var key256 = AES.GenerateKey(256);
            var cipher256 = AES.Encrypt(key256, plaintext);
            Assert.Equal(plaintext, AES.Decrypt(key256, cipher256));
        }

        [Fact]
        public void PaddingTest()
        {
            // 填充测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();

            // 不同长度的数据（测试 PKCS7 填充）
            for (int i = 1; i <= 32; i++)
            {
                var plaintext = new string('A', i);
                var ciphertext = AES.EncryptCBC(key, iv, plaintext);
                var decrypted = AES.DecryptCBC(key, iv, ciphertext);

                Assert.Equal(plaintext, decrypted);
            }
        }

        [Fact]
        public void LargeDataEncryptionTest()
        {
            // 大数据加密测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();
            var largeData = new string('B', 100000);

            var ciphertext = AES.EncryptCBC(key, iv, largeData);
            var decrypted = AES.DecryptCBC(key, iv, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void BinaryDataTest()
        {
            // 二进制数据加密测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();

            var binaryData = new byte[] { 0x00, 0x01, 0xFF, 0xFE, 0x7F, 0x80 };

            var ciphertext = AES.EncryptBytes(key, iv, binaryData);
            var decrypted = AES.DecryptBytes(key, iv, ciphertext);

            Assert.Equal(binaryData, decrypted);
        }

        [Fact]
        public void NullAndEmptyTest()
        {
            // 空数据和 null 测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();

            // 空字符串
            var empty = "";
            var cipherEmpty = AES.EncryptCBC(key, iv, empty);
            var plainEmpty = AES.DecryptCBC(key, iv, cipherEmpty);
            Assert.Equal(empty, plainEmpty);
        }

        [Fact]
        public void KeyIVValidationTest()
        {
            // 密钥和 IV 验证
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();

            Assert.NotNull(key);
            Assert.NotNull(iv);

            // 密钥长度应该是 16、24 或 32 字节
            Assert.Contains(key.Length, new[] { 16, 24, 32 });

            // IV 长度应该是 16 字节
            Assert.Equal(16, iv.Length);
        }

        [Fact]
        public void DifferentPlaintextsTest()
        {
            // 不同明文测试
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();

            var testCases = new[]
            {
                "ASCII text",
                "中文文本",
                "Emoji 🔐🔑🔒",
                "Mixed: ASCII + 中文 + Emoji 🎉"
            };

            foreach (var plaintext in testCases)
            {
                var ciphertext = AES.EncryptCBC(key, iv, plaintext);
                var decrypted = AES.DecryptCBC(key, iv, ciphertext);
                Assert.Equal(plaintext, decrypted);
            }
        }

        [Fact]
        public void RepeatedEncryptionTest()
        {
            // 重复加密测试（使用相同的 IV）
            var key = AES.GenerateKey();
            var iv = AES.GenerateIV();
            var plaintext = "Repeated test";

            var cipher1 = AES.EncryptCBC(key, iv, plaintext);
            var cipher2 = AES.EncryptCBC(key, iv, plaintext);

            // 相同的密钥和 IV 应该产生相同的密文
            Assert.Equal(cipher1, cipher2);

            // 但都能正确解密
            Assert.Equal(plaintext, AES.DecryptCBC(key, iv, cipher1));
            Assert.Equal(plaintext, AES.DecryptCBC(key, iv, cipher2));
        }
    }
}
