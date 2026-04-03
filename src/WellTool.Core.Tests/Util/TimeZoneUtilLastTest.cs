using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class TimeZoneUtilLastTest
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
}
