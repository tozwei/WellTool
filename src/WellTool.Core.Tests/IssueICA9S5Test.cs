using Xunit;
using WellTool.Core;
using WellTool.Core.Util;
using WellTool.Core.Text;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #ICA9S5 测试
    /// </summary>
    public class IssueICA9S5Test
    {
        [Fact]
        public void Test()
        {
            string a = "ENUM{\ndisable ~ 0\nenable ~ 1\n}";
            var split = StrSplitter.SplitByString(a, "\n");
            Assert.Equal(4, split.Length);
        }
    }
}
