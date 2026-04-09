using System.Text;
using Xunit;
using WellTool.Crypto.Digest.Mac;

namespace WellTool.Crypto.Tests.Digest
{
    public class HmacTests
    {
        [Fact]
        public void HMacEngineTest()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC!");

            // 测试 HMAC-MD5
            var hmacMd5Engine = new HMacEngine(MacAlgorithm.HmacMD5);
            hmacMd5Engine.Init(key);
            hmacMd5Engine.Update(data);
            var hmacMd5 = hmacMd5Engine.DoFinal();
            Assert.NotNull(hmacMd5);
            Assert.Equal(16, hmacMd5.Length); // HMAC-MD5 长度为 16 字节

            // 测试 HMAC-SHA1
            var hmacSha1Engine = new HMacEngine(MacAlgorithm.HmacSHA1);
            hmacSha1Engine.Init(key);
            hmacSha1Engine.Update(data);
            var hmacSha1 = hmacSha1Engine.DoFinal();
            Assert.NotNull(hmacSha1);
            Assert.Equal(20, hmacSha1.Length); // HMAC-SHA1 长度为 20 字节

            // 测试 HMAC-SHA256
            var hmacSha256Engine = new HMacEngine(MacAlgorithm.HmacSHA256);
            hmacSha256Engine.Init(key);
            hmacSha256Engine.Update(data);
            var hmacSha256 = hmacSha256Engine.DoFinal();
            Assert.NotNull(hmacSha256);
            Assert.Equal(32, hmacSha256.Length); // HMAC-SHA256 长度为 32 字节
        }

        [Fact]
        public void MacTest()
        {
            var key = Encoding.UTF8.GetBytes("key");
            var data = Encoding.UTF8.GetBytes("Hello, HMAC!");

            // 测试 HMAC-MD5
            var hmacMd5 = Mac.HmacMd5(key, data);
            Assert.NotNull(hmacMd5);
            Assert.Equal(16, hmacMd5.Length); // HMAC-MD5 长度为 16 字节

            // 测试 HMAC-SHA1
            var hmacSha1 = Mac.HmacSha1(key, data);
            Assert.NotNull(hmacSha1);
            Assert.Equal(20, hmacSha1.Length); // HMAC-SHA1 长度为 20 字节

            // 测试 HMAC-SHA256
            var hmacSha256 = Mac.HmacSha256(key, data);
            Assert.NotNull(hmacSha256);
            Assert.Equal(32, hmacSha256.Length); // HMAC-SHA256 长度为 32 字节

            // 测试 HMAC-SHA384
            var hmacSha384 = Mac.HmacSha384(key, data);
            Assert.NotNull(hmacSha384);
            Assert.Equal(48, hmacSha384.Length); // HMAC-SHA384 长度为 48 字节

            // 测试 HMAC-SHA512
            var hmacSha512 = Mac.HmacSha512(key, data);
            Assert.NotNull(hmacSha512);
            Assert.Equal(64, hmacSha512.Length); // HMAC-SHA512 长度为 64 字节
        }
    }
}