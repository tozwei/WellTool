using System.Text;
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
        public void EncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes key
            var nonce = Encoding.UTF8.GetBytes("123456789012"); // 12 bytes nonce
            var plaintext = Encoding.UTF8.GetBytes("Hello, ChaCha20!");

            var encrypted = ChaCha20.Encrypt(plaintext, key, nonce);
            var decrypted = ChaCha20.Decrypt(encrypted, key, nonce);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(plaintext, decrypted);
        }
    }
}
