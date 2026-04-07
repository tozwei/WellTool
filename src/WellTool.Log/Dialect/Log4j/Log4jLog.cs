using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Log4j;

/// <summary>
/// Apache Log4j 1.x 日志实现
/// </summary>
public class Log4jLog : AbstractLog
{
    private readonly object _logger;
    private readonly MethodInfo _getLoggerMethod;
    private readonly MethodInfo _isEnabledForMethod;
    private readonly MethodInfo _logMethod;
    private readonly Type _levelType;
    private readonly object _warnLevel;
    private readonly object _errorLevel;
    private readonly object _traceLevel;
    private readonly object _debugLevel;
    private readonly object _infoLevel;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public Log4jLog(string name) : base(name)
    {
        try
        {
            // 使用反射加载Log4j相关类型
            var log4jAssembly = Assembly.Load("log4j");
            var loggerType = log4jAssembly.GetType("org.apache.log4j.Logger");
            _levelType = log4jAssembly.GetType("org.apache.log4j.Level");

            // 获取方法和字段
            _getLoggerMethod = loggerType.GetMethod("getLogger", new[] { typeof(string) });
            _isEnabledForMethod = loggerType.GetMethod("isEnabledFor", new[] { _levelType });
            _logMethod = loggerType.GetMethod("log", new[] { typeof(string), _levelType, typeof(object), typeof(Exception) });

            // 获取级别字段
            _traceLevel = _levelType.GetField("TRACE").GetValue(null);
            _debugLevel = _levelType.GetField("DEBUG").GetValue(null);
            _infoLevel = _levelType.GetField("INFO").GetValue(null);
            _warnLevel = _levelType.GetField("WARN").GetValue(null);
            _errorLevel = _levelType.GetField("ERROR").GetValue(null);

            // 创建Logger实例
            _logger = _getLoggerMethod.Invoke(null, new object[] { name });
        }
        catch (Exception ex)
        {
            throw new Exception("Log4j not found, please add log4j dependency", ex);
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type">日志类型</param>
    public Log4jLog(Type type) : base(type)
    {
        try
        {
            // 使用反射加载Log4j相关类型
            var log4jAssembly = Assembly.Load("log4j");
            var loggerType = log4jAssembly.GetType("org.apache.log4j.Logger");
            _levelType = log4jAssembly.GetType("org.apache.log4j.Level");

            // 获取方法和字段
            _getLoggerMethod = loggerType.GetMethod("getLogger", new[] { typeof(Type) });
            _isEnabledForMethod = loggerType.GetMethod("isEnabledFor", new[] { _levelType });
            _logMethod = loggerType.GetMethod("log", new[] { typeof(string), _levelType, typeof(object), typeof(Exception) });

            // 获取级别字段
            _traceLevel = _levelType.GetField("TRACE").GetValue(null);
            _debugLevel = _levelType.GetField("DEBUG").GetValue(null);
            _infoLevel = _levelType.GetField("INFO").GetValue(null);
            _warnLevel = _levelType.GetField("WARN").GetValue(null);
            _errorLevel = _levelType.GetField("ERROR").GetValue(null);

            // 创建Logger实例
            _logger = _getLoggerMethod.Invoke(null, new object[] { type });
        }
        catch (Exception ex)
        {
            throw new Exception("Log4j not found, please add log4j dependency", ex);
        }
    }

    /// <summary>
    /// 获取日志名称
    /// </summary>
    /// <returns>日志名称</returns>
    public override string GetName()
    {
        var getNameMethod = _logger.GetType().GetMethod("getName");
        return getNameMethod.Invoke(_logger, null) as string;
    }

    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public override bool IsEnabled(Level.Level level)
    {
        object log4jLevel;
        switch (level)
        {
            case Level.Level.Trace:
                log4jLevel = _traceLevel;
                break;
            case Level.Level.Debug:
                log4jLevel = _debugLevel;
                break;
            case Level.Level.Info:
                log4jLevel = _infoLevel;
                break;
            case Level.Level.Warn:
                log4jLevel = _warnLevel;
                break;
            case Level.Level.Error:
            case Level.Level.Fatal:
                log4jLevel = _errorLevel;
                break;
            default:
                throw new Exception($"Can not identify level: {level}");
        }

        return (bool)_isEnabledForMethod.Invoke(_logger, new[] { log4jLevel });
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
        Log(null, level, t, format, arguments);
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
        object log4jLevel;
        switch (level)
        {
            case Level.Level.Trace:
                log4jLevel = _traceLevel;
                break;
            case Level.Level.Debug:
                log4jLevel = _debugLevel;
                break;
            case Level.Level.Info:
                log4jLevel = _infoLevel;
                break;
            case Level.Level.Warn:
                log4jLevel = _warnLevel;
                break;
            case Level.Level.Error:
            case Level.Level.Fatal:
                log4jLevel = _errorLevel;
                break;
            default:
                throw new Exception($"Can not identify level: {level}");
        }

        var isEnabled = (bool)_isEnabledForMethod.Invoke(_logger, new[] { log4jLevel });
        if (isEnabled)
        {
            var message = string.Format(format, arguments);
            _logMethod.Invoke(_logger, new object[] { fqcn, log4jLevel, message, t });
        }
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