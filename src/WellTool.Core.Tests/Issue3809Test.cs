using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3809 测试
    /// </summary>
    public class Issue3809Test
    {
        [Fact]
        public void RoundStrTest()
        {
            Assert.Equal("9999999999999999.99", NumberUtil.RoundStr("9999999999999999.99", 2));
            Assert.Equal("11111111111111119.00", NumberUtil.RoundStr("11111111111111119.00", 2));
            Assert.Equal("7999999999999999.99", NumberUtil.RoundStr("7999999999999999.99", 2));
            Assert.Equal("699999999991999.92", NumberUtil.RoundStr("699999999991999.92", 2));
            Assert.Equal("10.92", NumberUtil.RoundStr("10.92", 2));
            Assert.Equal("10.99", NumberUtil.RoundStr("10.99", 2));
        }
    }
}
