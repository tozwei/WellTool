using WellTool.Core.Convert;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class NumberWordFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        Assert.Equal("一百二十三", NumberWordFormatter.Format(123));
        Assert.Equal("一千二百三十四", NumberWordFormatter.Format(1234));
    }

    [Fact]
    public void FormatUpperTest()
    {
        Assert.Equal("壹", NumberWordFormatter.FormatUpper(1));
        Assert.Equal("贰", NumberWordFormatter.FormatUpper(2));
    }

    [Fact]
    public void FormatDecimalTest()
    {
        var result = NumberWordFormatter.FormatDecimal(123.45);
        Assert.Contains("一百二十三", result);
        Assert.Contains("四五", result);
    }

    [Fact]
    public void FormatNegativeTest()
    {
        Assert.Contains("负", NumberWordFormatter.Format(-100));
    }

    [Fact]
    public void FormatZeroTest()
    {
        Assert.Equal("零", NumberWordFormatter.Format(0));
    }
}
