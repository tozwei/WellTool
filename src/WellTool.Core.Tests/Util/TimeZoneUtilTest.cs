using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class TimeZoneUtilTest
{
    [Fact]
    public void GetDefaultTest()
    {
        var tz = TimeZoneUtil.GetDefault();
        Assert.NotNull(tz);
    }

    [Fact]
    public void GetAvailableIDsTest()
    {
        var ids = TimeZoneUtil.GetAvailableIDs();
        Assert.NotNull(ids);
        Assert.NotEmpty(ids);
    }

    [Fact]
    public void GetTest()
    {
        var tz = TimeZoneUtil.Get("UTC");
        Assert.NotNull(tz);
    }

    [Fact]
    public void GetOffsetTest()
    {
        var offset = TimeZoneUtil.GetOffset("UTC");
        Assert.Equal(0, offset);
    }

    [Fact]
    public void GetDisplayNameTest()
    {
        var name = TimeZoneUtil.GetDisplayName("UTC");
        Assert.NotNull(name);
    }

    [Fact]
    public void InDaylightTimeTest()
    {
        var tz = TimeZoneUtil.GetDefault();
        var now = DateTime.Now;
        var result = TimeZoneUtil.InDaylightTime(now);
        Assert.False(result);
    }
}
