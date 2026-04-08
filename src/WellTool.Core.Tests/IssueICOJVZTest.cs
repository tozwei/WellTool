using Xunit;
using WellTool.Core;
using WellTool.Core.Text;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #ICOJVZ 测试
    /// </summary>
    public class IssueICOJVZTest
    {
        [Fact]
        public void ToUnderlineTest()
        {
            string field = "PAGE_NAME";
            field = NamingCase.ToUnderlineCase(field).ToUpperInvariant();
            Assert.Equal("PAGE_NAME", field);
        }
    }
}
