using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests.Asymmetric
{
    public class RSATests
    {
        [Fact]
        public void GenerateKeyPairTest()
        {
            // 测试生成 RSA 密钥对
            var (publicKey, privateKey) = RSA.GenerateKeyPair();
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }

        [Fact]
        public void EncryptDecryptTest()
        {
            // 测试 RSA 加密和解密
            var (publicKey, privateKey) = RSA.GenerateKeyPair();
            var rsa = new RSA(publicKey, privateKey);

            var plaintext = "Hello, RSA!";
            var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext));
            var decrypted = rsa.Decrypt(encrypted);

            Assert.NotNull(encrypted);
            Assert.NotNull(decrypted);
            Assert.Equal(plaintext, Encoding.UTF8.GetString(decrypted));
        }
    }
}