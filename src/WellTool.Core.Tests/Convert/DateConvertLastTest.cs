using WellTool.Core.Convert;
using Xunit;
using System;

namespace WellTool.Core.Tests;

public class DateConvertLastTest
{
    [Fact]
    public void ToDateTest()
    {
        var result = Convert.ToDate("2024-01-01");
        Assert.NotNull(result);
    }

    [Fact]
    public void ToDateFromLongTest()
    {
        var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var result = Convert.ToDate(timestamp);
        Assert.NotNull(result);
    }
}
