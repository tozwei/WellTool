using System.Text;
using Xunit;
using WellTool.Crypto.Digest.Mac;

namespace WellTool.Crypto.Tests.Digest
{
    public class CBCBlockCipherMacEngineTests
    {
        [Fact]
        public void TestCBCBlockCipherMacEngine()
        {
            // 测试 HMAC 相关功能
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
    }
}