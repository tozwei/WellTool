using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class BetweenFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        var betweenMs = DateUtil.BetweenMs(
            DateUtil.Parse("2017-01-01 22:59:59"),
            DateUtil.Parse("2017-01-02 23:59:58"));

        var formatter = new BetweenFormatter(betweenMs, BetweenFormatter.Level.MILLISECOND, 1);
        Assert.Equal("1天", formatter.ToString());
    }

    [Fact]
    public void FormatBetweenTest()
    {
        var betweenMs = DateUtil.BetweenMs(
            DateUtil.Parse("2018-07-16 11:23:19"),
            DateUtil.Parse("2018-07-16 11:23:20"));

        var formatter = new BetweenFormatter(betweenMs, BetweenFormatter.Level.SECOND, 1);
        Assert.Equal("1秒", formatter.ToString());
    }

    [Fact]
    public void FormatBetweenTest2()
    {
        var betweenMs = DateUtil.BetweenMs(
            DateUtil.Parse("2018-07-16 12:25:23"),
            DateUtil.Parse("2018-07-16 11:23:20"));

        var formatter = new BetweenFormatter(betweenMs, BetweenFormatter.Level.SECOND, 5);
        Assert.Equal("1小时2分3秒", formatter.ToString());
    }
}
