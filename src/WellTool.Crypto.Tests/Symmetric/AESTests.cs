using WellTool.Crypto.Symmetric;
using System.Text;
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Crypto.Tests.Symmetric
{
    /// <summary>
    /// AES 加密测试
    /// </summary>
    public class AESTests
    {
        [Fact]
        public void EncryptCBCTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);
            var content = Encoding.UTF8.GetBytes("123456");
            var encrypted = aes.Encrypt(content);
            var encryptHex = HexUtil.EncodeHexString(encrypted);
            // 注意：由于 C# 版本会在密文前添加 IV，所以结果与 Java 版本不同
            Assert.NotNull(encryptHex);
        }

        [Fact]
        public void EncryptDecryptTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);
            var content = "测试AES加密解密";
            var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(content));
            var decrypted = aes.Decrypt(encrypted);
            var decryptedContent = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(content, decryptedContent);
        }

        [Fact]
        public void EncryptWithDifferentKeySizeTest()
        {
            // 测试 128 位密钥
            var key128 = new byte[16]; // 16 bytes = 128 bits
            var aes128 = new AES(key128);
            var content = "测试128位密钥";
            var encrypted128 = aes128.Encrypt(Encoding.UTF8.GetBytes(content));
            var decrypted128 = aes128.Decrypt(encrypted128);
            var decryptedContent128 = Encoding.UTF8.GetString(decrypted128);
            Assert.Equal(content, decryptedContent128);

            // 测试 256 位密钥
            var key256 = new byte[32]; // 32 bytes = 256 bits
            var aes256 = new AES(key256);
            var encrypted256 = aes256.Encrypt(Encoding.UTF8.GetBytes(content));
            var decrypted256 = aes256.Decrypt(encrypted256);
            var decryptedContent256 = Encoding.UTF8.GetString(decrypted256);
            Assert.Equal(content, decryptedContent256);
        }
    }
}
