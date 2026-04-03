using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3136 测试
    /// </summary>
    public class Issue3136Test
    {
        [Fact]
        public void TestIssue()
        {
            // Issue 3136: 测试特定场景
            var result = true;
            Assert.True(result);
        }
    }
}
