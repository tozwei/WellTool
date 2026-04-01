using WellTool.Crypto.Symmetric;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// TEA 加密算法测试
    /// </summary>
    public class TEATests
    {
        [Fact]
        public void EncryptAndDecryptTest()
        {
            // TEA 加密解密
            var key = new byte[16]; // TEA 使用 128 位密钥
            for (int i = 0; i < 16; i++) key[i] = (byte)i;

            var plaintext = "Hello TEA!";
            var ciphertext = TEA.Encrypt(key, plaintext);
            var decrypted = TEA.Decrypt(key, ciphertext);

            Assert.Equal(plaintext, decrypted);
        }

        [Fact]
        public void KeySizeTest()
        {
            // TEA 密钥大小测试（固定 128 位）
            var key = new byte[16];

            Assert.Throws<System.ArgumentException>(() =>
            {
                var shortKey = new byte[8];
                TEA.Encrypt(shortKey, "test");
            });
        }

        [Fact]
        public void BlockCipherTest()
        {
            // TEA 分组密码测试
            var key = new byte[16];
            for (int i = 0; i < 16; i++) key[i] = (byte)i;

            // TEA 处理 64 位数据块
            var data = new byte[8];
            for (int i = 0; i < 8; i++) data[i] = (byte)i;

            var encrypted = TEA.EncryptBytes(key, data);
            var decrypted = TEA.DecryptBytes(key, encrypted);

            Assert.Equal(data, decrypted);
        }
    }
}
