using WellTool.Log;
using WellTool.Log.Dialect.LogTube;

namespace WellTool.Log.Tests;

/// <summary>
/// LogTube 日志测试
/// </summary>
public class LogTubeTest
{
    [Fact]
    public void LogTubeLogTest()
    {
        if (!LogTubeLogFactory.IsAvailable)
        {
            // LogTube 不可用，跳过测试
            return;
        }

        var factory = new LogTubeLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();
        
        log.Debug("LogTube debug test.");
        log.Info("LogTube info test.");
    }

    [Fact]
    public void LogTubeLogByNameTest()
    {
        if (!LogTubeLogFactory.IsAvailable)
        {
            // LogTube 不可用，跳过测试
            return;
        }

        var factory = new LogTubeLogFactory();
        var log = factory.GetLog("LogTubeTest");
        
        Assert.NotNull(log);
        log.Info("LogTube log by name test.");
    }

    [Fact]
    public void LogTubeLogByTypeTest()
    {
        if (!LogTubeLogFactory.IsAvailable)
        {
            // LogTube 不可用，跳过测试
            return;
        }

        var factory = new LogTubeLogFactory();
        var log = factory.GetLog(typeof(LogTubeTest));
        
        Assert.NotNull(log);
        log.Info("LogTube log by type test.");
    }
}
