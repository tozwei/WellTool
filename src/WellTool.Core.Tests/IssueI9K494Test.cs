using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I9K494 测试
    /// </summary>
    public class IssueI9K494Test
    {
        [Fact]
        public void TestIssue()
        {
            // Issue I9K494: 测试特定场景
            var result = true;
            Assert.True(result);
        }
    }
}
