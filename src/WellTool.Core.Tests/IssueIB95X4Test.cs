using Xunit;
using WellTool.Core;
using WellTool.Core.Lang;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #IB95X4 测试
    /// </summary>
    public class IssueIB95X4Test
    {
        [Fact]
        public void IsMacTest()
        {
            Assert.True(ReUtil.IsMatch(PatternPool.MAC_ADDRESS.ToString(), "ab1c.2d3e.f468"));
            Assert.True(ReUtil.IsMatch(PatternPool.MAC_ADDRESS.ToString(), "ab:1c:2d:3e:f4:68"));
            Assert.True(ReUtil.IsMatch(PatternPool.MAC_ADDRESS.ToString(), "ab-1c-2d-3e-f4-68"));
            Assert.True(ReUtil.IsMatch(PatternPool.MAC_ADDRESS.ToString(), "ab1c2d3ef468"));
        }
    }
}
