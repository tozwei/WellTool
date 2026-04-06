using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class ObjectIdTest
{
    [Fact]
    public void NewObjectIdTest()
    {
        var objectId = ObjectId.Get();
        Assert.NotNull(objectId);
        Assert.Equal(24, objectId.Length);
    }

    [Fact]
    public void ParseTest()
    {
        var objectId = ObjectId.Get();
        var parsed = ObjectId.Parse(objectId);
        Assert.Equal(objectId, parsed.ToString());
    }

    [Fact]
    public void GetTimestampTest()
    {
        var objectId = ObjectId.Get();
        var timestamp = objectId.GetTimestamp();
        Assert.True(timestamp > 0);
    }

    [Fact]
    public void GetMachineIdTest()
    {
        var objectId = ObjectId.Get();
        var machineId = objectId.GetMachineId();
        Assert.True(machineId >= 0);
    }

    [Fact]
    public void CompareToTest()
    {
        var objectId1 = ObjectId.Get();
        System.Threading.Thread.Sleep(1);
        var objectId2 = ObjectId.Get();
        Assert.True(objectId1.CompareTo(objectId2) < 0);
    }

    [Fact]
    public void EqualsTest()
    {
        var objectId1 = ObjectId.Get();
        var objectId2 = ObjectId.Parse(objectId1.ToString());
        Assert.Equal(objectId1, objectId2);
    }
}
