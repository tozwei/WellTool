using WellTool.Core.Id;
using Xunit;

namespace WellTool.Core.Tests;

public class SnowflakeTest
{
    [Fact]
    public void NextIdTest()
    {
        var snowflake = new Snowflake();
        var id1 = snowflake.NextId();
        var id2 = snowflake.NextId();
        Assert.True(id2 > id1);
    }

    [Fact]
    public void NextIdStringTest()
    {
        var snowflake = new Snowflake();
        var id = snowflake.NextIdStr();
        Assert.NotNull(id);
        Assert.NotEmpty(id);
    }

    [Fact]
    public void GetWorkerIdTest()
    {
        var snowflake = new Snowflake(1, 1);
        Assert.Equal(1, snowflake.WorkerId);
    }

    [Fact]
    public void GetDataCenterIdTest()
    {
        var snowflake = new Snowflake(1, 2);
        Assert.Equal(2, snowflake.DataCenterId);
    }

    [Fact]
    public void MultiThreadTest()
    {
        var snowflake = new Snowflake(1, 1);
        var ids = new System.Collections.Concurrent.ConcurrentBag<long>();
        Parallel.For(0, 1000, _ =>
        {
            ids.Add(snowflake.NextId());
        });
        Assert.Equal(1000, ids.Count);
        Assert.Equal(1000, ids.Distinct().Count());
    }
}
