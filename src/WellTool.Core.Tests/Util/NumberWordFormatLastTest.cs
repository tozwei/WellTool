using WellTool.Core.Convert;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class NumberWordFormatLastTest
{
    [Fact]
    public void FormatTest()
    {
        Assert.Equal("一百二十三", NumberWordFormatter.Format(123));
    }

    [Fact]
    public void FormatUpperTest()
    {
        Assert.Equal("壹", NumberWordFormatter.FormatUpper(1));
    }

    [Fact]
    public void FormatDecimalTest()
    {
        var result = NumberWordFormatter.FormatDecimal(123.45);
        Assert.NotNull(result);
    }
}
