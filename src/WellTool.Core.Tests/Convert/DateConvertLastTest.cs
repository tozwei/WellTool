using WellTool.Core.Convert;
using Xunit;
using System;

namespace WellTool.Core.Tests;

public class DateConvertLastTest
{
    [Fact]
    public void ToDateTimeTest()
    {
        var result = WellTool.Core.Convert.ConvertUtil.ToDateTime("2024-01-01");
        Assert.NotNull(result);
    }

    [Fact]
    public void ToDateTimeFromLongTest()
    {
        var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var result = WellTool.Core.Convert.ConvertUtil.ToDateTime(timestamp);
        Assert.NotNull(result);
    }
}
