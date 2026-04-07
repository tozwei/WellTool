using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests.Date;

public class BetweenFormatterLastTest
{
    [Fact]
    public void FormatTest()
    {
        var result = BetweenFormatter.Format(1000, DateUnit.Second, BetweenFormatter.Level.SECOND);
        Assert.NotNull(result);
    }
}