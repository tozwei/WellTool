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
        // 测试数据
        private const string TestData = "Hello, World!";
        private readonly byte[] _key = Encoding.UTF8.GetBytes("secret_key");

        /// <summary>
        /// 测试 HMAC-MD5
        /// </summary>
        [Fact]
        public void TestHmacMd5()
        {
            var data = Encoding.UTF8.GetBytes(TestData);
            var result = Mac.HmacMd5(_key, data);
            Assert.NotNull(result);
            Assert.Equal(16, result.Length);
        }

        /// <summary>
        /// 测试 HMAC-SHA1
        /// </summary>
        [Fact]
        public void TestHmacSha1()
        {
            var data = Encoding.UTF8.GetBytes(TestData);
            var result = Mac.HmacSha1(_key, data);
            Assert.NotNull(result);
            Assert.Equal(20, result.Length);
        }

        /// <summary>
        /// 测试 HMAC-SHA256
        /// </summary>
        [Fact]
        public void TestHmacSha256()
        {
            var data = Encoding.UTF8.GetBytes(TestData);
            var result = Mac.HmacSha256(_key, data);
            Assert.NotNull(result);
            Assert.Equal(32, result.Length);
        }

        /// <summary>
        /// 测试 HMAC-SHA384
        /// </summary>
        [Fact]
        public void TestHmacSha384()
        {
            var data = Encoding.UTF8.GetBytes(TestData);
            var result = Mac.HmacSha384(_key, data);
            Assert.NotNull(result);
            Assert.Equal(48, result.Length);
        }

        /// <summary>
        /// 测试 HMAC-SHA512
        /// </summary>
        [Fact]
        public void TestHmacSha512()
        {
            var data = Encoding.UTF8.GetBytes(TestData);
            var result = Mac.HmacSha512(_key, data);
            Assert.NotNull(result);
            Assert.Equal(64, result.Length);
        }
    }
}