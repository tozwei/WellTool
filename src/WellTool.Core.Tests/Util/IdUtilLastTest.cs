using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class IdUtilLastTest
{
    [Fact]
    public void FastSimpleUUIDTest()
    {
        var uuid = IdUtil.FastSimpleUUID();
        Assert.NotNull(uuid);
        Assert.Equal(32, uuid.Length);
    }

    [Fact]
    public void RandomUUIDTest()
    {
        var uuid = IdUtil.RandomUUID();
        Assert.NotNull(uuid);
    }

    [Fact]
    public void ObjectIdTest()
    {
        var objectId = ObjectId.Get();
        Assert.NotNull(objectId);
        Assert.Equal(24, objectId.Length);
    }

    [Fact]
    public void SnowflakeTest()
    {
        var id1 = IdUtil.GetSnowflakeNextId();
        var id2 = IdUtil.GetSnowflakeNextId();
        Assert.True(id2 > id1);
    }

    [Fact]
    public void NanoIdTest()
    {
        var nanoId = IdUtil.NanoId();
        Assert.NotNull(nanoId);
    }

    [Fact]
    public void FastUUIDTest()
    {
        var uuid = IdUtil.FastUUID();
        Assert.NotNull(uuid);
    }

    [Fact]
    public void CreateUUIDTest()
    {
        var uuid = IdUtil.CreateUUID();
        Assert.NotNull(uuid);
    }
}
