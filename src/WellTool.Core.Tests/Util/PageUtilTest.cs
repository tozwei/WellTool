using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class PageUtilTest
{
    [Fact]
    public void TotalPageTest()
    {
        Assert.Equal(10, PageUtil.TotalPage(100, 10));
        Assert.Equal(11, PageUtil.TotalPage(101, 10));
        Assert.Equal(1, PageUtil.TotalPage(0, 10));
    }

    [Fact]
    public void TransToStartEndTest()
    {
        var (start, end) = PageUtil.TransToStartEnd(1, 10);
        Assert.Equal(0, start);
        Assert.Equal(10, end);

        var (start2, end2) = PageUtil.TransToStartEnd(2, 10);
        Assert.Equal(10, start2);
        Assert.Equal(20, end2);
    }

    [Fact]
    public void GetFirstPageTest()
    {
        Assert.Equal(0, PageUtil.GetFirstPage());
    }

    [Fact]
    public void RainbowTest()
    {
        var rainbow = PageUtil.Rainbow(100, 10, 5);
        Assert.True(rainbow.Length > 0);
        Assert.Contains(1, rainbow);
    }

    [Fact]
    public void ToHtmlSelectTest()
    {
        var html = PageUtil.ToHtmlSelect(100);
        Assert.NotNull(html);
        Assert.Contains("100", html);
    }
}
