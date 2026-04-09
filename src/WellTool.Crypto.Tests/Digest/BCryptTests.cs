using System.Text;
using Xunit;
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests.Digest
{
    /// <summary>
    /// BCrypt 密码哈希测试
    /// </summary>
    public class BCryptTests
    {
        [Fact]
        public void HashTest()
        {
            var password = "password123";
            var hash = BCryptUtil.Hash(password);
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
        }

        [Fact]
        public void VerifyTest()
        {
            var password = "password123";
            var hash = BCryptUtil.Hash(password);
            var result = BCryptUtil.Verify(password, hash);
            Assert.True(result);
        }

        [Fact]
        public void VerifyWithByteArrayTest()
        {
            var password = "password123";
            var hash = BCryptUtil.Hash(password);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var result = BCryptUtil.Verify(passwordBytes, hash);
            Assert.True(result);
        }

        [Fact]
        public void VerifyInvalidPasswordTest()
        {
            var password = "password123";
            var hash = BCryptUtil.Hash(password);
            var result = BCryptUtil.Verify("invalid", hash);
            Assert.False(result);
        }
    }
}
