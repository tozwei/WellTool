using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class IdUtilTest
{
    [Fact]
    public void SimpleUUIDTest()
    {
        var uuid = IdUtil.SimpleUUID();
        Assert.NotNull(uuid);
        Assert.Equal(32, uuid.Length);
    }

    [Fact]
    public void UUIDTest()
    {
        var uuid = IdUtil.UUID();
        Assert.NotNull(uuid);
        Assert.Equal(36, uuid.Length);
    }

    [Fact]
    public void NewObjectIdTest()
    {
        var objectId = IdUtil.NewObjectId();
        Assert.NotNull(objectId);
        Assert.Equal(24, objectId.Length);
    }

    [Fact]
    public void SnowflakeTest()
    {
        var id1 = IdUtil.SnowflakeId();
        var id2 = IdUtil.SnowflakeId();
        Assert.True(id2 > id1);
    }

    [Fact]
    public void TimeIdTest()
    {
        var timeId = IdUtil.TimeId();
        Assert.NotNull(timeId);
    }

    [Fact]
    public void FastUUIDTest()
    {
        var uuid = IdUtil.FastUUID();
        Assert.NotNull(uuid);
        Assert.Equal(32, uuid.Length);
    }

    [Fact]
    public void RandomIdTest()
    {
        var randomId = IdUtil.RandomId();
        Assert.NotNull(randomId);
        Assert.Equal(16, randomId.Length);

        var customRandomId = IdUtil.RandomId(32);
        Assert.Equal(32, customRandomId.Length);
    }
}
