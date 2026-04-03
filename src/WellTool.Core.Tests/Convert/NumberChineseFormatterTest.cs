using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class NumberChineseFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        Assert.Equal("一百二十三", NumberChineseFormatter.Format(123));
        Assert.Equal("一千二百三十四", NumberChineseFormatter.Format(1234));
        Assert.Equal("一万二千三百四十五", NumberChineseFormatter.Format(12345));
    }

    [Fact]
    public void FormatNegativeTest()
    {
        Assert.Equal("负一百", NumberChineseFormatter.Format(-100));
    }

    [Fact]
    public void FormatZeroTest()
    {
        Assert.Equal("零", NumberChineseFormatter.Format(0));
    }

    [Fact]
    public void FormatDecimalTest()
    {
        Assert.Contains("元", NumberChineseFormatter.FormatDecimal(123.45m));
    }

    [Fact]
    public void FormatUpperTest()
    {
        Assert.Equal("壹", NumberChineseFormatter.FormatUpper(1));
        Assert.Equal("贰", NumberChineseFormatter.FormatUpper(2));
        Assert.Equal("叁", NumberChineseFormatter.FormatUpper(3));
    }
}
