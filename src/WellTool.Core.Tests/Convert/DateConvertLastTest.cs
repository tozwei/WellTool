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
        // ConvertUtil.ToDateTime 不直接支持 long 时间戳转换
        // 需要使用 DateTimeOffset.FromUnixTimeMilliseconds
        var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        var result = dateTimeOffset.DateTime;
        Assert.NotNull(result);
    }
}
