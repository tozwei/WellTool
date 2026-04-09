using Xunit;
using WellTool.Crypto;
using System;

namespace WellTool.Crypto.Tests
{
    public class BCUtilTests
    {
        [Fact]
        public void CreateSecureRandomTest()
        {
            // 测试创建安全随机数生成器
            var random = BCUtil.CreateSecureRandom();
            Assert.NotNull(random);
            
            // 测试生成随机数
            var randomBytes = new byte[16];
            random.NextBytes(randomBytes);
            Assert.NotEmpty(randomBytes);
        }
    }
}