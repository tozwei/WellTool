using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// ChaCha20 流加密测试
    /// </summary>
    public class ChaCha20Tests
    {
        [Fact]
        public void EncryptAndDecryptTest()
        {
            // ChaCha20 加密解密
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var plaintext = "Hello ChaCha20!";

            var ciphertext = ChaCha20.Encrypt(key, nonce, plaintext);
            var decrypted = ChaCha20.Decrypt(key, nonce, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void KeySizeTest()
        {
            // ChaCha20 密钥大小测试
            var key = ChaCha20.GenerateKey();

            Assert.NotNull(key);
            Assert.Equal(32, key.Length); // ChaCha20 使用 256 位（32 字节）密钥
        }

        [Fact]
        public void NonceSizeTest()
        {
            // ChaCha20 随机数大小测试
            var nonce = ChaCha20.GenerateNonce();

            Assert.NotNull(nonce);
            Assert.Equal(12, nonce.Length); // ChaCha20 使用 96 位（12 字节）随机数
        }

        [Fact]
        public void EmptyDataTest()
        {
            // ChaCha20 空数据测试
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var emptyData = "";

            var ciphertext = ChaCha20.Encrypt(key, nonce, emptyData);
            var decrypted = ChaCha20.Decrypt(key, nonce, ciphertext);

            Assert.Equal(emptyData, decrypted);
        }

        [Fact]
        public void LargeDataTest()
        {
            // ChaCha20 大数据测试
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var largeData = new string('A', 10000);

            var ciphertext = ChaCha20.Encrypt(key, nonce, largeData);
            var decrypted = ChaCha20.Decrypt(key, nonce, ciphertext);

            Assert.Equal(largeData, decrypted);
        }

        [Fact]
        public void ChineseTextTest()
        {
            // ChaCha20 中文文本测试
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var chineseText = "你好，ChaCha20 流加密！";

            var ciphertext = ChaCha20.Encrypt(key, nonce, chineseText);
            var decrypted = ChaCha20.Decrypt(key, nonce, ciphertext);

            Assert.Equal(chineseText, decrypted);
        }

        [Fact]
        public void DifferentNonceProducesDifferentCiphertextTest()
        {
            // ChaCha20 不同随机数产生不同密文
            var key = ChaCha20.GenerateKey();
            var plaintext = "test data";

            var nonce1 = ChaCha20.GenerateNonce();
            var nonce2 = ChaCha20.GenerateNonce();

            var ciphertext1 = ChaCha20.Encrypt(key, nonce1, plaintext);
            var ciphertext2 = ChaCha20.Encrypt(key, nonce2, plaintext);

            Assert.NotEqual(ciphertext1, ciphertext2);
        }

        [Fact]
        public void WrongNonceDecryptionFailureTest()
        {
            // ChaCha20 错误随机数解密失败
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var wrongNonce = ChaCha20.GenerateNonce();
            var plaintext = "test data";

            var ciphertext = ChaCha20.Encrypt(key, nonce, plaintext);

            // 使用错误的随机数解密应该失败或产生错误结果
            var decrypted = ChaCha20.Decrypt(key, wrongNonce, ciphertext);

            Assert.NotEqual(plaintext, decrypted);
        }

        [Fact]
        public void StreamCipherPropertyTest()
        {
            // ChaCha20 流加密特性测试
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();

            // 流加密可以逐字节加密
            var byte1 = new byte[] { 0x41 }; // 'A'
            var byte2 = new byte[] { 0x42 }; // 'B'

            var cipher1 = ChaCha20.EncryptBytes(key, nonce, byte1);
            var cipher2 = ChaCha20.EncryptBytes(key, nonce, byte2);

            Assert.NotNull(cipher1);
            Assert.NotNull(cipher2);
            Assert.NotEqual(cipher1, cipher2);
        }

        [Fact]
        public void IETFModeTest()
        {
            // ChaCha20 IETF 模式测试
            var key = ChaCha20.GenerateKey();
            var nonce = ChaCha20.GenerateNonce();
            var plaintext = "IETF mode test";

            var ciphertext = ChaCha20.EncryptIETF(key, nonce, plaintext);
            var decrypted = ChaCha20.DecryptIETF(key, nonce, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }
    }
}
