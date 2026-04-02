using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Serilog;

/// <summary>
/// Serilog日志实现
/// </summary>
public class SerilogLog : AbstractLog
{
    private readonly object _logger;
    private readonly MethodInfo _verboseMethod;
    private readonly MethodInfo _debugMethod;
    private readonly MethodInfo _infoMethod;
    private readonly MethodInfo _warnMethod;
    private readonly MethodInfo _errorMethod;
    private readonly MethodInfo _fatalMethod;
    private readonly MethodInfo _isEnabledMethod;
    private readonly object _verboseLevel;
    private readonly object _debugLevel;
    private readonly object _infoLevel;
    private readonly object _warnLevel;
    private readonly object _errorLevel;
    private readonly object _fatalLevel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public SerilogLog(string name) : base(name)
    {
        // 使用反射加载Serilog相关类型
        var serilogAssembly = Assembly.Load("Serilog");
        var loggerType = serilogAssembly.GetType("Serilog.ILogger");
        var logManagerType = serilogAssembly.GetType("Serilog.Log");
        var logEventLevelType = serilogAssembly.GetType("Serilog.Events.LogEventLevel");

        // 获取Logger实例
        var loggerProperty = logManagerType.GetProperty("Logger");
        var logger = loggerProperty.GetValue(null);
        var forContextMethod = logger.GetType().GetMethod("ForContext", new[] { typeof(string), typeof(object) });
        _logger = forContextMethod.Invoke(logger, new object[] { "SourceContext", name });

        // 获取日志级别
        _verboseLevel = Enum.Parse(logEventLevelType, "Verbose");
        _debugLevel = Enum.Parse(logEventLevelType, "Debug");
        _infoLevel = Enum.Parse(logEventLevelType, "Information");
        _warnLevel = Enum.Parse(logEventLevelType, "Warning");
        _errorLevel = Enum.Parse(logEventLevelType, "Error");
        _fatalLevel = Enum.Parse(logEventLevelType, "Fatal");

        // 获取方法
        _verboseMethod = _logger.GetType().GetMethod("Verbose", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _infoMethod = _logger.GetType().GetMethod("Information", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _warnMethod = _logger.GetType().GetMethod("Warning", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _isEnabledMethod = _logger.GetType().GetMethod("IsEnabled", new[] { logEventLevelType });
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type">日志类型</param>
    public SerilogLog(Type type) : base(type)
    {
        // 使用反射加载Serilog相关类型
        var serilogAssembly = Assembly.Load("Serilog");
        var loggerType = serilogAssembly.GetType("Serilog.ILogger");
        var logManagerType = serilogAssembly.GetType("Serilog.Log");
        var logEventLevelType = serilogAssembly.GetType("Serilog.Events.LogEventLevel");

        // 获取Logger实例
        var loggerProperty = logManagerType.GetProperty("Logger");
        var logger = loggerProperty.GetValue(null);
        var forContextMethod = logger.GetType().GetMethod("ForContext", new[] { typeof(Type) });
        _logger = forContextMethod.Invoke(logger, new object[] { type });

        // 获取日志级别
        _verboseLevel = Enum.Parse(logEventLevelType, "Verbose");
        _debugLevel = Enum.Parse(logEventLevelType, "Debug");
        _infoLevel = Enum.Parse(logEventLevelType, "Information");
        _warnLevel = Enum.Parse(logEventLevelType, "Warning");
        _errorLevel = Enum.Parse(logEventLevelType, "Error");
        _fatalLevel = Enum.Parse(logEventLevelType, "Fatal");

        // 获取方法
        _verboseMethod = _logger.GetType().GetMethod("Verbose", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _infoMethod = _logger.GetType().GetMethod("Information", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _warnMethod = _logger.GetType().GetMethod("Warning", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _isEnabledMethod = _logger.GetType().GetMethod("IsEnabled", new[] { logEventLevelType });
    }

    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public override bool IsEnabled(Level.Level level)
    {
        object serilogLevel;
        switch (level)
        {
            case Level.Level.Trace:
                serilogLevel = _verboseLevel;
                break;
            case Level.Level.Debug:
                serilogLevel = _debugLevel;
                break;
            case Level.Level.Info:
                serilogLevel = _infoLevel;
                break;
            case Level.Level.Warn:
                serilogLevel = _warnLevel;
                break;
            case Level.Level.Error:
                serilogLevel = _errorLevel;
                break;
            case Level.Level.Fatal:
                serilogLevel = _fatalLevel;
                break;
            default:
                return false;
        }

        return (bool)_isEnabledMethod.Invoke(_logger, new[] { serilogLevel });
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(Level.Level level, Exception t, string format, params object[] arguments)
    {
        switch (level)
        {
            case Level.Level.Trace:
                _verboseMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
            case Level.Level.Debug:
                _debugMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
            case Level.Level.Info:
                _infoMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
            case Level.Level.Warn:
                _warnMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
            case Level.Level.Error:
                _errorMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
            case Level.Level.Fatal:
                _fatalMethod?.Invoke(_logger, new object[] { t, format, arguments });
                break;
        }
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(string fqcn, Level.Level level, Exception t, string format, params object[] arguments)
    {
        // Serilog不直接支持fqcn，使用普通Log方法
        Log(level, t, format, arguments);
    }

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Trace(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Trace, t, format, arguments);
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Debug(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Debug, t, format, arguments);
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Info(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Info, t, format, arguments);
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Warn(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Warn, t, format, arguments);
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Error(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Error, t, format, arguments);
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Fatal(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Fatal, t, format, arguments);
    }
}
