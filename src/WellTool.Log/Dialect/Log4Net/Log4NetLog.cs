using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Log4Net;

/// <summary>
/// Log4Net日志实现
/// </summary>
public class Log4NetLog : AbstractLog
{
    private readonly object _logger;
    private readonly MethodInfo _debugMethod;
    private readonly MethodInfo _infoMethod;
    private readonly MethodInfo _warnMethod;
    private readonly MethodInfo _errorMethod;
    private readonly MethodInfo _fatalMethod;
    private readonly PropertyInfo _isDebugEnabledProperty;
    private readonly PropertyInfo _isInfoEnabledProperty;
    private readonly PropertyInfo _isWarnEnabledProperty;
    private readonly PropertyInfo _isErrorEnabledProperty;
    private readonly PropertyInfo _isFatalEnabledProperty;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public Log4NetLog(string name) : base(name)
    {
        // 使用反射加载Log4Net相关类型
        var log4netAssembly = Assembly.Load("log4net");
        var logManagerType = log4netAssembly.GetType("log4net.LogManager");
        var loggerType = log4netAssembly.GetType("log4net.ILog");

        // 获取Logger实例
        var getLoggerMethod = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
        _logger = getLoggerMethod.Invoke(null, new object[] { name });

        // 获取方法
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(object), typeof(Exception) });
        _infoMethod = _logger.GetType().GetMethod("Info", new[] { typeof(object), typeof(Exception) });
        _warnMethod = _logger.GetType().GetMethod("Warn", new[] { typeof(object), typeof(Exception) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(object), typeof(Exception) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(object), typeof(Exception) });

        // 获取属性
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
    public Log4NetLog(Type type) : base(type)
    {
        // 使用反射加载Log4Net相关类型
        var log4netAssembly = Assembly.Load("log4net");
        var logManagerType = log4netAssembly.GetType("log4net.LogManager");
        var loggerType = log4netAssembly.GetType("log4net.ILog");

        // 获取Logger实例
        var getLoggerMethod = logManagerType.GetMethod("GetLogger", new[] { typeof(Type) });
        _logger = getLoggerMethod.Invoke(null, new object[] { type });

        // 获取方法
        _debugMethod = _logger.GetType().GetMethod("Debug", new[] { typeof(object), typeof(Exception) });
        _infoMethod = _logger.GetType().GetMethod("Info", new[] { typeof(object), typeof(Exception) });
        _warnMethod = _logger.GetType().GetMethod("Warn", new[] { typeof(object), typeof(Exception) });
        _errorMethod = _logger.GetType().GetMethod("Error", new[] { typeof(object), typeof(Exception) });
        _fatalMethod = _logger.GetType().GetMethod("Fatal", new[] { typeof(object), typeof(Exception) });

        // 获取属性
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
        var message = string.Format(format, arguments);
        switch (level)
        {
            case Level.Level.Trace:
            case Level.Level.Debug:
                _debugMethod?.Invoke(_logger, new object[] { message, t });
                break;
            case Level.Level.Info:
                _infoMethod?.Invoke(_logger, new object[] { message, t });
                break;
            case Level.Level.Warn:
                _warnMethod?.Invoke(_logger, new object[] { message, t });
                break;
            case Level.Level.Error:
                _errorMethod?.Invoke(_logger, new object[] { message, t });
                break;
            case Level.Level.Fatal:
                _fatalMethod?.Invoke(_logger, new object[] { message, t });
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
        // Log4Net不直接支持fqcn，使用普通Log方法
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
