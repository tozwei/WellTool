using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class NumberChineseFormatterTest2
{
    [Fact]
    public void FormatTest()
    {
        Assert.Equal("零", NumberChineseFormatter.Format(0));
        Assert.Equal("一", NumberChineseFormatter.Format(1));
        Assert.Equal("十", NumberChineseFormatter.Format(10));
        Assert.Equal("一百", NumberChineseFormatter.Format(100));
    }

    [Fact]
    public void FormatUpperTest()
    {
        Assert.Equal("零", NumberChineseFormatter.Format(0, true));
        Assert.Equal("壹", NumberChineseFormatter.Format(1, true));
        Assert.Equal("拾", NumberChineseFormatter.Format(10, true));
    }

    [Fact]
    public void FormatDoubleTest()
    {
        var result = NumberChineseFormatter.Format(123.45, true, true);
        Assert.NotNull(result);
    }

    [Fact]
    public void FormatNegativeTest()
    {
        Assert.Contains("负", NumberChineseFormatter.Format(-1));
    }
}

