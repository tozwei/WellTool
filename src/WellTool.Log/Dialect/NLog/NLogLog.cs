using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.NLog;

/// <summary>
/// NLog日志实现
/// </summary>
public class NLogLog : AbstractLog
{
    private readonly object _logger;
    private readonly MethodInfo _traceMethod;
    private readonly MethodInfo _debugMethod;
    private readonly MethodInfo _infoMethod;
    private readonly MethodInfo _warnMethod;
    private readonly MethodInfo _errorMethod;
    private readonly MethodInfo _fatalMethod;
    private readonly PropertyInfo _isTraceEnabledProperty;
    private readonly PropertyInfo _isDebugEnabledProperty;
    private readonly PropertyInfo _isInfoEnabledProperty;
    private readonly PropertyInfo _isWarnEnabledProperty;
    private readonly PropertyInfo _isErrorEnabledProperty;
    private readonly PropertyInfo _isFatalEnabledProperty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public NLogLog(string name) : base(name)
    {
        // 使用反射加载NLog相关类型
        var nlogAssembly = Assembly.Load("NLog");
        var logManagerType = nlogAssembly.GetType("NLog.LogManager");
        var loggerType = nlogAssembly.GetType("NLog.Logger");

        // 获取Logger实例
        var getLoggerMethod = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
        _logger = getLoggerMethod.Invoke(null, new object[] { name });

        // 获取方法
        _traceMethod = _logger.GetType().GetMethod("Trace", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _infoMethod = _logger.GetType().GetMethod("Info", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _warnMethod = _logger.GetType().GetMethod("Warn", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(Exception), typeof(string), typeof(object[]) });

        // 获取属性
        _isTraceEnabledProperty = _logger.GetType().GetProperty("IsTraceEnabled");
        _isDebugEnabledProperty = _logger.GetType().GetProperty("IsDebugEnabled");
        _isInfoEnabledProperty = _logger.GetType().GetProperty("IsInfoEnabled");
        _isWarnEnabledProperty = _logger.GetType().GetProperty("IsWarnEnabled");
        _isErrorEnabledProperty = _logger.GetType().GetProperty("IsErrorEnabled");
        _isFatalEnabledProperty = _logger.GetType().GetProperty("IsFatalEnabled");
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type">日志类型</param>
    public NLogLog(Type type) : base(type)
    {
        // 使用反射加载NLog相关类型
        var nlogAssembly = Assembly.Load("NLog");
        var logManagerType = nlogAssembly.GetType("NLog.LogManager");
        var loggerType = nlogAssembly.GetType("NLog.Logger");

        // 获取Logger实例
        var getLoggerMethod = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
        _logger = getLoggerMethod.Invoke(null, new object[] { type.FullName });

        // 获取方法
        _traceMethod = _logger.GetType().GetMethod("Trace", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _infoMethod = _logger.GetType().GetMethod("Info", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _warnMethod = _logger.GetType().GetMethod("Warn", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(Exception), typeof(string), typeof(object[]) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(Exception), typeof(string), typeof(object[]) });

        // 获取属性
        _isTraceEnabledProperty = _logger.GetType().GetProperty("IsTraceEnabled");
        _isDebugEnabledProperty = _logger.GetType().GetProperty("IsDebugEnabled");
        _isInfoEnabledProperty = _logger.GetType().GetProperty("IsInfoEnabled");
        _isWarnEnabledProperty = _logger.GetType().GetProperty("IsWarnEnabled");
        _isErrorEnabledProperty = _logger.GetType().GetProperty("IsErrorEnabled");
        _isFatalEnabledProperty = _logger.GetType().GetProperty("IsFatalEnabled");
    }

    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public override bool IsEnabled(Level.Level level)
    {
        switch (level)
        {
            case Level.Level.Trace:
                return (bool)_isTraceEnabledProperty.GetValue(_logger);
            case Level.Level.Debug:
                return (bool)_isDebugEnabledProperty.GetValue(_logger);
            case Level.Level.Info:
                return (bool)_isInfoEnabledProperty.GetValue(_logger);
            case Level.Level.Warn:
                return (bool)_isWarnEnabledProperty.GetValue(_logger);
            case Level.Level.Error:
                return (bool)_isErrorEnabledProperty.GetValue(_logger);
            case Level.Level.Fatal:
                return (bool)_isFatalEnabledProperty.GetValue(_logger);
            default:
                return false;
        }
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
                _traceMethod?.Invoke(_logger, new object[] { t, format, arguments });
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
        // NLog不直接支持fqcn，使用普通Log方法
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
