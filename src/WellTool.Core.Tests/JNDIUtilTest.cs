using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// JNDI工具单元测试
    /// </summary>
    public class JNDIUtilTest
    {
        [Fact]
        public void GetEnvironmentTest()
        {
            var env = JNDIUtil.GetEnvironment();
            Assert.NotNull(env);
        }

        [Fact]
        public void GetEnvironmentValueTest()
        {
            var javaFactory = JNDIUtil.GetEnvironment("java.naming.factory.initial");
            // 没有JNDI环境时可能返回null
            Assert.Null(javaFactory);
        }

        [Fact]
        public void LookupInvalidTest()
        {
            // 不存在的JNDI资源应返回null或抛异常
            var result = JNDIUtil.Lookup("invalid/Resource");
            Assert.Null(result);
        }
    }
}
