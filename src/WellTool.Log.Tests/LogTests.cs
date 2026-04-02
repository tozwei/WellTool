using WellTool.Log;
using LevelEnum = WellTool.Log.Level.Level;

namespace WellTool.Log.Tests;

/// <summary>
/// 日志门面单元测试
/// </summary>
public class LogTests
{
    [Fact]
    public void LogTest()
    {
        var log = LogFactory.Get();

        // 自动选择日志实现
        log.Debug("This is {0} log", LevelEnum.Debug);
        log.Info("This is {0} log", LevelEnum.Info);
        log.Warn("This is {0} log", LevelEnum.Warn);
    }

    [Fact]
    public void LogWithExceptionTest()
    {
        var log = LogFactory.Get();
        var exception = new Exception("test Exception");
        log.Error(exception, "This is {0} log", LevelEnum.Error);
    }

    [Fact]
    public void LogNullTest()
    {
        var log = LogFactory.Get();
        log.Debug(null as string);
        log.Info(null as string);
        log.Warn(null as string);
    }

    [Fact]
    public void ParameterizedMessageEdgeCasesTest()
    {
        var log = LogFactory.Get();

        // 测试不同数量的参数
        log.Info("No parameters");
        log.Info("One: {0}", "param1");
        log.Info("Two: {0} and {1}", "param1", "param2");
        log.Info("Three: {0}, {1}, {2}", "param1", "param2", "param3");
        log.Info("Four: {0}, {1}, {2}, {3}", "param1", "param2", "param3", "param4");

        // 测试参数不足的情况
        log.Info("Missing param: {0} and {1}", "only_one");

        // 测试参数过多的情况
        log.Info("Extra param: {0}", "param1", "extra_param");
    }

    [Fact]
    public void ComplexObjectTest()
    {
        var log = LogFactory.Get();
        
        // 复杂对象参数测试
        var list = new[] { "item1", "item2" };
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "key", "value" }
        };

        log.Info("Array: {0}", string.Join(", ", list));
        log.Info("Map: {0}", map);
        log.Info("Null object: {0}", (object)null);
    }

    [Fact]
    public void TestLogFactory()
    {
        // Test that LogFactory can be accessed
        var log = LogFactory.Get(typeof(LogTests));
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

    [Fact]
    public void TestLevelValues()
    {
        Assert.True(LevelEnum.Trace < LevelEnum.Debug);
        Assert.True(LevelEnum.Debug < LevelEnum.Info);
        Assert.True(LevelEnum.Info < LevelEnum.Warn);
        Assert.True(LevelEnum.Warn < LevelEnum.Error);
        Assert.True(LevelEnum.Error < LevelEnum.Fatal);
    }

    [Fact]
    public void TestLogByName()
    {
        var log = LogFactory.Get("CustomLogName");
        Assert.NotNull(log);
        Assert.Equal("CustomLogName", log.GetName());
    }

    [Fact]
    public void TestLogByType()
    {
        var log = LogFactory.Get(typeof(LogTests));
        Assert.NotNull(log);
        Assert.Contains("LogTests", log.GetName());
    }

    [Fact]
    public void TestLogCaching()
    {
        var log1 = LogFactory.Get(typeof(LogTests));
        var log2 = LogFactory.Get(typeof(LogTests));
        Assert.Same(log1, log2);
    }

    [Fact]
    public void TestGetCurrentLogFactory()
    {
        var factory = LogFactory.GetCurrentLogFactory();
        Assert.NotNull(factory);
    }

    [Fact]
    public void TestSetCurrentLogFactory()
    {
        var originalFactory = LogFactory.GetCurrentLogFactory();
        try
        {
            var newFactory = new WellTool.Log.Dialect.Console.ConsoleLogFactory();
            LogFactory.SetCurrentLogFactory(newFactory);
            Assert.Same(newFactory, LogFactory.GetCurrentLogFactory());
        }
        finally
        {
            LogFactory.SetCurrentLogFactory(originalFactory);
        }
    }
}
