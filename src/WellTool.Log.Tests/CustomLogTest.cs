using Xunit;
using WellTool.Log;
using WellTool.Log.Dialect.Commons;
using WellTool.Log.Dialect.Console;
using WellTool.Log.Dialect.Jdk;
using WellTool.Log.Dialect.Log4Net;
using WellTool.Log.Dialect.Log4j;
using WellTool.Log.Dialect.Log4j2;
using WellTool.Log.Dialect.NLog;
using WellTool.Log.Dialect.Serilog;

namespace WellTool.Log.Tests;

/// <summary>
/// 自定义日志门面测试
/// </summary>
public class CustomLogTest
{
    private const string LINE = "----------------------------------------------------------------------";

    [Fact]
    public void ConsoleLogTest()
    {
        var factory = new ConsoleLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void ConsoleColorLogTest()
    {
        var factory = new ConsoleColorLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void JdkLogTest()
    {
        var factory = new JdkLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void Log4jLogTest()
    {
        var factory = new Log4jLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void Log4j2LogTest()
    {
        var factory = new Log4j2LogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Debug("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void Log4NetLogTest()
    {
        var factory = new Log4NetLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void NLogTest()
    {
        var factory = new NLogLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void SerilogTest()
    {
        var factory = new SerilogLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void CommonsLogTest()
    {
        var factory = new ApacheCommonsLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        var log = LogFactory.Get();

        log.Info("This is custom '{0}' log\n{1}", factory.GetName(), LINE);
    }

    [Fact]
    public void SetCurrentLogFactoryByTypeTest()
    {
        LogFactory.SetCurrentLogFactory(typeof(ConsoleLogFactory));
        var log = LogFactory.Get();
        Assert.NotNull(log);
    }

    [Fact]
    public void SetCurrentLogFactoryByInstanceTest()
    {
        var factory = new ConsoleLogFactory();
        LogFactory.SetCurrentLogFactory(factory);
        Assert.Same(factory, LogFactory.GetCurrentLogFactory());
    }
}
