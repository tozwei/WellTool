using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I9UK5V 测试
    /// </summary>
    public class IssueI9UK5VTest
    {
        [Fact]
        public void SplitTest()
        {
            string str = "";
            var split = StrUtil.Split(str, ',');
            Assert.Equal(1, split.Length);

            split = StrUtil.SplitTrim(str, ',');
            Assert.Equal(0, split.Length);
        }
    }
}
