using WellTool.Log;

namespace WellTool.Log.Tests;

public class LogTests
{
    [Fact]
    public void TestLogFactory()
    {
        // Test that LogFactory can be accessed
        var log = LogFactory.GetLog(typeof(LogTests));
        Assert.NotNull(log);
    }

    [Fact]
    public void TestStaticLog()
    {
        // Test that StaticLog can be accessed
        Assert.NotNull(StaticLog.Log);
    }

    [Fact]
    public void TestGlobalLogFactory()
    {
        // Test that GlobalLogFactory can be accessed
        Assert.NotNull(GlobalLogFactory.GetLog(typeof(LogTests)));
    }
}
