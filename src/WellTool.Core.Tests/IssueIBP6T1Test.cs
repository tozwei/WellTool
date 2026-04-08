using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #IBP6T1 测试
    /// </summary>
    public class IssueIBP6T1Test
    {
        [Fact]
        public void IsValidCard10Test()
        {
            Assert.Equal("true", IdcardUtil.IsValidCard10("1608214(1)")[2]);
            Assert.Equal("true", IdcardUtil.IsValidCard10("1608214（1）")[2]);
        }
    }
}
