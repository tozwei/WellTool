using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// RC4 流加密测试
    /// </summary>
    public class RC4Tests
    {
        [Fact]
        public void EncryptAndDecryptTest()
        {
            // RC4 加密解密
            var key = RC4.GenerateKey(16);
            var plaintext = "Hello RC4!";

            var ciphertext = RC4.Encrypt(key, plaintext);
            var decrypted = RC4.Decrypt(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void KeySizeTest()
        {
            // RC4 密钥大小测试（40-2048 位）
            var key8 = RC4.GenerateKey(8);
            Assert.NotNull(key8);

            var key16 = RC4.GenerateKey(16);
            Assert.NotNull(key16);

            var key32 = RC4.GenerateKey(32);
            Assert.NotNull(key32);
        }

        [Fact]
        public void EmptyDataTest()
        {
            // RC4 空数据测试
            var key = RC4.GenerateKey(16);
            var emptyData = "";

            var ciphertext = RC4.Encrypt(key, emptyData);
            var decrypted = RC4.Decrypt(key, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void LargeDataTest()
        {
            // RC4 大数据测试
            var key = RC4.GenerateKey(16);
            var largeData = new string('A', 10000);

            var ciphertext = RC4.Encrypt(key, largeData);
            var decrypted = RC4.Decrypt(key, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void ChineseTextTest()
        {
            // RC4 中文文本测试
            var key = RC4.GenerateKey(16);
            var chineseText = "你好，RC4 流加密！";

            var ciphertext = RC4.Encrypt(key, chineseText);
            var decrypted = RC4.Decrypt(key, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void DifferentKeysTest()
        {
            // RC4 不同密钥产生不同密文
            var key1 = RC4.GenerateKey(16);
            var key2 = RC4.GenerateKey(16);
            var plaintext = "test data";

            var cipher1 = RC4.Encrypt(key1, plaintext);
            var cipher2 = RC4.Encrypt(key2, plaintext);

            Assert.NotEqual(cipher1, cipher2);
        }

        [Fact]
        public void StreamPropertyTest()
        {
            // RC4 流加密特性测试
            var key = RC4.GenerateKey(16);

            // 流加密可以逐字节处理
            var byte1 = new byte[] { 0x41 };
            var byte2 = new byte[] { 0x42 };

            var cipher1 = RC4.EncryptBytes(key, byte1);
            var cipher2 = RC4.EncryptBytes(key, byte2);

            Assert.NotNull(cipher1);
            Assert.NotNull(cipher2);
            Assert.NotEqual(cipher1, cipher2);
        }
    }
}
