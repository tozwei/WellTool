using System.Text;
using Xunit;
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// Argon2 密码哈希测试 - 2015 年密码哈希竞赛获胜者
    /// </summary>
    public class Argon2Tests
    {
        [Fact]
        public void HashTest()
        {
            var password = Encoding.UTF8.GetBytes("password123");
            var salt = Encoding.UTF8.GetBytes("salt");
            var hash = Argon2.Hash(password, salt);
            Assert.NotNull(hash);
            Assert.Equal(32, hash.Length); // 默认哈希长度为 32 字节
        }

        [Fact]
        public void HashHexTest()
        {
            var password = "password123";
            var salt = "salt";
            var hashHex = Argon2.HashHex(password, salt);
            Assert.NotNull(hashHex);
            Assert.NotEmpty(hashHex);
            Assert.Equal(64, hashHex.Length); // 32 字节哈希长度为 64 个十六进制字符
        }

        [Fact]
        public void VerifyTest()
        {
            var password = "password123";
            var salt = "salt";
            var hashHex = Argon2.HashHex(password, salt);
            var result = Argon2.Verify(password, hashHex, salt);
            Assert.True(result);
        }

        [Fact]
        public void VerifyWithByteArrayTest()
        {
            var password = Encoding.UTF8.GetBytes("password123");
            var salt = Encoding.UTF8.GetBytes("salt");
            var hash = Argon2.Hash(password, salt);
            var result = Argon2.Verify(password, hash, salt);
            Assert.True(result);
        }

        [Fact]
        public void VerifyInvalidPasswordTest()
        {
            var password = "password123";
            var salt = "salt";
            var hashHex = Argon2.HashHex(password, salt);
            var result = Argon2.Verify("invalid", hashHex, salt);
            Assert.False(result);
        }
    }
}
