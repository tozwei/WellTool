using System;
using System.Text;
using Xunit;
using WellTool.Crypto.Digest.Mac;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// MAC测试类
    /// </summary>
    public class MacTests
    {
        [Fact]
        public void HmacMd5Test()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC-MD5!");
            var mac = Mac.HmacMd5(key, data);
            Assert.NotNull(mac);
            Assert.Equal(16, mac.Length); // HMAC-MD5 长度为 16 字节
        }

        [Fact]
        public void HmacSha1Test()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC-SHA1!");
            var mac = Mac.HmacSha1(key, data);
            Assert.NotNull(mac);
            Assert.Equal(20, mac.Length); // HMAC-SHA1 长度为 20 字节
        }

        [Fact]
        public void HmacSha256Test()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC-SHA256!");
            var mac = Mac.HmacSha256(key, data);
            Assert.NotNull(mac);
            Assert.Equal(32, mac.Length); // HMAC-SHA256 长度为 32 字节
        }

        [Fact]
        public void HmacSha384Test()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC-SHA384!");
            var mac = Mac.HmacSha384(key, data);
            Assert.NotNull(mac);
            Assert.Equal(48, mac.Length); // HMAC-SHA384 长度为 48 字节
        }

        [Fact]
        public void HmacSha512Test()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC-SHA512!");
            var mac = Mac.HmacSha512(key, data);
            Assert.NotNull(mac);
            Assert.Equal(64, mac.Length); // HMAC-SHA512 长度为 64 字节
        }
    }
}