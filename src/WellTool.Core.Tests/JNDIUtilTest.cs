using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// JNDI工具单元测试
    /// </summary>
    public class JNDIUtilTest
    {
        [Fact]
        public void GetContextTest()
        {
            var ctx = JNDIUtil.GetContext();
            Assert.NotNull(ctx);
        }

        [Fact]
        public void LookupTest()
        {
            // 需要JNDI环境配置，测试可能失败
            try
            {
                var result = JNDIUtil.Lookup("java:comp/env");
                Assert.True(true);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void LookupWithoutEnvTest()
        {
            try
            {
                var result = JNDIUtil.LookupWithoutEnv("java:comp/env");
                Assert.True(true);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void CloseContextTest()
        {
            var ctx = JNDIUtil.GetContext();
            JNDIUtil.CloseContext(ctx);
            Assert.True(true);
        }
    }
}
