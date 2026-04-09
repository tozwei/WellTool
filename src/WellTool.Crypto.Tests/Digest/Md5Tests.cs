using System.IO;
using System.Text;
using Xunit;
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// MD5 哈希算法测试
    /// </summary>
    public class Md5Tests
    {
        [Fact]
        public void DigestTest()
        {
            var data = Encoding.UTF8.GetBytes("Hello, MD5!");
            var digest = MD5.Digest(data);
            Assert.NotNull(digest);
            Assert.Equal(16, digest.Length); // MD5 摘要长度为 16 字节
        }

        [Fact]
        public void DigestHexTest()
        {
            var data = "Hello, MD5!";
            var digestHex = MD5.DigestHex(data);
            Assert.NotNull(digestHex);
            Assert.NotEmpty(digestHex);
            Assert.Equal(32, digestHex.Length); // MD5 摘要长度为 32 个十六进制字符
        }

        [Fact]
        public void DigestStreamTest()
        {
            var data = "Hello, MD5!";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var digest = MD5.Digest(stream);
            Assert.NotNull(digest);
            Assert.Equal(16, digest.Length); // MD5 摘要长度为 16 字节
        }
    }
}
