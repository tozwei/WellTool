using System.Text;
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    public class SecureUtilTests
    {
        [Fact]
        public void Md5Test()
        {
            var data = Encoding.UTF8.GetBytes("Hello, MD5!");
            var md5 = SecureUtil.Md5(data);
            Assert.NotNull(md5);
            Assert.Equal(16, md5.Length); // MD5 摘要长度为 16 字节
        }

        [Fact]
        public void Md5StringTest()
        {
            var str = "Hello, MD5!";
            var md5 = SecureUtil.Md5(str);
            Assert.NotNull(md5);
            Assert.Equal(16, md5.Length); // MD5 摘要长度为 16 字节
        }

        [Fact]
        public void Sha1Test()
        {
            var data = Encoding.UTF8.GetBytes("Hello, SHA1!");
            var sha1 = SecureUtil.Sha1(data);
            Assert.NotNull(sha1);
            Assert.Equal(20, sha1.Length); // SHA1 摘要长度为 20 字节
        }

        [Fact]
        public void Sha256Test()
        {
            var data = Encoding.UTF8.GetBytes("Hello, SHA256!");
            var sha256 = SecureUtil.Sha256(data);
            Assert.NotNull(sha256);
            Assert.Equal(32, sha256.Length); // SHA256 摘要长度为 32 字节
        }

        [Fact]
        public void AesTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = SecureUtil.Aes(key);
            Assert.NotNull(aes);
        }

        [Fact]
        public void RsaTest()
        {
            var (publicKey, privateKey) = CryptoUtil.GenerateRsaKeyPair();
            var rsa = SecureUtil.Rsa(privateKey);
            Assert.NotNull(rsa);
        }
    }
}