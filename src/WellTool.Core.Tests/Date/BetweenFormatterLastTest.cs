using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class BetweenFormatterLastTest
{
    [Fact]
    public void FormatTest()
    {
        var formatter = new BetweenFormatter(1000, BetweenFormatter.Level.SECOND, 1);
        Assert.NotNull(formatter);
    }

    [Fact]
    public void ToStringTest()
    {
        var formatter = new BetweenFormatter(1000, BetweenFormatter.Level.SECOND, 1);
        var result = formatter.ToString();
        Assert.NotNull(result);
    }
}
