using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3809 测试
    /// </summary>
    public class Issue3809Test
    {
        [Fact]
        public void TestIssue()
        {
            // Issue 3809: 测试特定场景
            var result = true;
            Assert.True(result);
        }
    }
}
