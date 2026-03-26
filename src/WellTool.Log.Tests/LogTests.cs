using WellTool.Log;

namespace WellTool.Log.Tests;

public class LogTests
{
    [Fact]
    public void TestLogFactory()
    {
        // Test that LogFactory can be accessed
        var log = LogFactory.Get(typeof(LogTests));
        Assert.NotNull(log);
    }

    [Fact]
    public void TestStaticLog()
    {
        // Test that StaticLog can be accessed
        var log = StaticLog.Get(typeof(LogTests));
        Assert.NotNull(log);
    }

    [Fact]
    public void TestGlobalLogFactory()
    {
        // Test that GlobalLogFactory can be accessed
        var logFactory = GlobalLogFactory.Get();
        var log = logFactory.GetLog(typeof(LogTests));
        Assert.NotNull(log);
    }
}
