using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class UnicodeUtilLastTest
{
    [Fact]
    public void ToUnicodeTest()
    {
        var result = UnicodeUtil.ToUnicode("中", true);
        Assert.NotNull(result);
    }

    [Fact]
    public void ToStringTest()
    {
        var result = UnicodeUtil.ToString("\\u4e2d\\u6587");
        Assert.Equal("中文", result);
    }
}
