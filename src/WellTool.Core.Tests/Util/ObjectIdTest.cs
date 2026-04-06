using Xunit;

namespace WellTool.Core.Tests;

public class ObjectIdTest
{
    [Fact]
    public void NewObjectIdTest()
    {
        var objectId = WellTool.Core.Lang.ObjectId.Get();
        Assert.NotNull(objectId);
        Assert.Equal(24, objectId.ToString().Length);
    }

    [Fact]
    public void ParseTest()
    {
        var objectId = WellTool.Core.Lang.ObjectId.Get();
        var parsed = WellTool.Core.Lang.ObjectId.Parse(objectId.ToString());
        Assert.Equal(objectId.ToString(), parsed.ToString());
    }

    [Fact]
    public void GetTimestampTest()
    {
        var objectId = WellTool.Core.Lang.ObjectId.Get();
        var timestamp = objectId.GetTimestamp();
        Assert.True(timestamp > 0);
    }

    [Fact]
    public void GetMachineIdTest()
    {
        var objectId = WellTool.Core.Lang.ObjectId.Get();
        var machineId = objectId.GetMachineId();
        Assert.True(machineId >= 0);
    }

    [Fact]
    public void CompareToTest()
    {
        var objectId1 = WellTool.Core.Lang.ObjectId.Get();
        System.Threading.Thread.Sleep(1);
        var objectId2 = WellTool.Core.Lang.ObjectId.Get();
        Assert.True(objectId1.CompareTo(objectId2) < 0);
    }

    [Fact]
    public void EqualsTest()
    {
        var objectId1 = WellTool.Core.Lang.ObjectId.Get();
        var objectId2 = WellTool.Core.Lang.ObjectId.Parse(objectId1.ToString());
        Assert.Equal(objectId1, objectId2);
    }
}

