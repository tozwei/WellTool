using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class PageUtilLastTest
{
    [Fact]
    public void TotalPageTest()
    {
        Assert.Equal(10, PageUtil.TotalPage(100, 10));
        Assert.Equal(11, PageUtil.TotalPage(101, 10));
    }

    [Fact]
    public void TransToStartEndTest()
    {
        var (start, end) = PageUtil.TransToStartEnd(1, 10);
        Assert.Equal(0, start);
        Assert.Equal(10, end);
    }

    [Fact]
    public void RainbowTest()
    {
        var rainbow = PageUtil.Rainbow(1, 10, 5);
        Assert.Equal(5, rainbow.Length);
    }
}
