using WellTool.Core.Convert;
using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateConvertTest
{
    [Fact]
    public void ToDateTest()
    {
        var a = "2017-05-06";
        var value = WellTool.Core.Convert.ConvertUtil.ToDateTime(a);
        Assert.NotNull(value);
    }

    [Fact]
    public void ToDateFromLongTest()
    {
        // ConvertUtil.ToDateTime 不直接支持 long 时间戳转换
        // 需要使用 DateTimeOffset.FromUnixTimeMilliseconds
        var timeLong = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timeLong);
        var value = dateTimeOffset.DateTime;
        Assert.NotNull(value);
    }

    [Fact]
    public void ToSqlDateTest()
    {
        var a = "2017-05-06";
        var value = WellTool.Core.Convert.ConvertUtil.ToDateTime(a);
        Assert.NotNull(value);
    }

    [Fact]
    public void ToLocalDateTimeTest()
    {
        var str = "2020-12-12 12:12:12";
        var value = WellTool.Core.Convert.ConvertUtil.ToDateTime(str);
        Assert.NotNull(value);
    }
}
