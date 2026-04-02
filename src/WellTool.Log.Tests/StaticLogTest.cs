using WellTool.Log;
using WellTool.Log.Dialect.Console;

namespace WellTool.Log.Tests;

/// <summary>
/// 静态日志测试
/// </summary>
public class StaticLogTests
{
    [Fact]
    public void StaticLogTest()
    {
        LogFactory.SetCurrentLogFactory(new ConsoleLogFactory());
        StaticLog.Debug("This is static {0} log", "debug");
        StaticLog.Info("This is static {0} log", "info");
    }

    [Fact]
    public void StaticLogWithColorTest()
    {
        LogFactory.SetCurrentLogFactory(typeof(ConsoleColorLogFactory));
        StaticLog.Debug("This is static {0} log", "debug");
        StaticLog.Info("This is static {0} log", "info");
        StaticLog.Error("This is static {0} log", "error");
        StaticLog.Warn("This is static {0} log", "warn");
        StaticLog.Trace("This is static {0} log", "trace");
    }

    [Fact]
    public void StaticLogWithExceptionTest()
    {
        LogFactory.SetCurrentLogFactory(new ConsoleLogFactory());
        var exception = new Exception("Test exception");
        StaticLog.Error(exception, "Error with exception: {0}", "test");
    }
}
