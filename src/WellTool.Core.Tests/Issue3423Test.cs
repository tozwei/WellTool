using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3423 测试
    /// </summary>
    public class Issue3423Test
    {
        [Fact]
        public void ToBigDecimalOfNaNTest()
        {
            Assert.Throws<ArgumentException>(() => NumberUtil.ToBigDecimal("NaN"));
        }
    }
}
