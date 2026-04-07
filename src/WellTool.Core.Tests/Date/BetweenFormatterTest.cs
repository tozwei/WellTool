using WellTool.Core.Date;
using Xunit;
using DateUnit = WellTool.Core.Date.DateUnit;

namespace WellTool.Core.Tests;

public class BetweenFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        var begin = WellTool.Core.Date.DateUtil.Parse("2017-01-01 22:59:59");
        var end = WellTool.Core.Date.DateUtil.Parse("2017-01-02 23:59:58");
        var betweenMs = WellTool.Core.Date.DateUtil.Between(begin, end, DateUnit.Millisecond);

        var result = BetweenFormatter.Format(betweenMs, DateUnit.Millisecond, BetweenFormatter.Level.DAY);
        Assert.NotNull(result);
    }

    [Fact]
    public void FormatBetweenTest()
    {
        var begin = WellTool.Core.Date.DateUtil.Parse("2018-07-16 11:23:19");
        var end = WellTool.Core.Date.DateUtil.Parse("2018-07-16 11:23:20");
        var betweenMs = WellTool.Core.Date.DateUtil.Between(begin, end, DateUnit.Millisecond);

        var result = BetweenFormatter.Format(betweenMs, DateUnit.Second, BetweenFormatter.Level.SECOND);
        Assert.NotNull(result);
    }

    [Fact]
    public void FormatBetweenTest2()
    {
        var begin = WellTool.Core.Date.DateUtil.Parse("2018-07-16 12:25:23");
        var end = WellTool.Core.Date.DateUtil.Parse("2018-07-16 11:23:20");
        var betweenMs = WellTool.Core.Date.DateUtil.Between(begin, end, DateUnit.Millisecond);

        var result = BetweenFormatter.Format(betweenMs, DateUnit.Second, BetweenFormatter.Level.SECOND);
        Assert.NotNull(result);
    }
}
