using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Log4Net2
{
    /// <summary>
    /// Log4Net2日志实现
    /// </summary>
    internal class Log4Net2Log : AbstractLog
    {
        private readonly object _logger;
        private readonly MethodInfo _debugMethod;
        private readonly MethodInfo _infoMethod;
        private readonly MethodInfo _warnMethod;
        private readonly MethodInfo _errorMethod;
        private readonly MethodInfo _fatalMethod;
        private readonly MethodInfo _isDebugEnabledMethod;
        private readonly MethodInfo _isInfoEnabledMethod;
        private readonly MethodInfo _isWarnEnabledMethod;
        private readonly MethodInfo _isErrorEnabledMethod;
        private readonly MethodInfo _isFatalEnabledMethod;

        public Log4Net2Log(string name, object logger) : base(name)
        {
            _logger = logger;
            var loggerType = logger.GetType();
            _debugMethod = loggerType.GetMethod("Debug", new[] { typeof(object) });
            _infoMethod = loggerType.GetMethod("Info", new[] { typeof(object) });
            _warnMethod = loggerType.GetMethod("Warn", new[] { typeof(object) });
            _errorMethod = loggerType.GetMethod("Error", new[] { typeof(object) });
            _fatalMethod = loggerType.GetMethod("Fatal", new[] { typeof(object) });
            _isDebugEnabledMethod = loggerType.GetProperty("IsDebugEnabled").GetGetMethod();
            _isInfoEnabledMethod = loggerType.GetProperty("IsInfoEnabled").GetGetMethod();
            _isWarnEnabledMethod = loggerType.GetProperty("IsWarnEnabled").GetGetMethod();
            _isErrorEnabledMethod = loggerType.GetProperty("IsErrorEnabled").GetGetMethod();
            _isFatalEnabledMethod = loggerType.GetProperty("IsFatalEnabled").GetGetMethod();
        }

        public override bool IsEnabled(WellTool.Log.Level.Level level)
        {
            switch (level)
            {
                case WellTool.Log.Level.Level.Debug:
                    return (bool)_isDebugEnabledMethod.Invoke(_logger, null);
                case WellTool.Log.Level.Level.Info:
                    return (bool)_isInfoEnabledMethod.Invoke(_logger, null);
                case WellTool.Log.Level.Level.Warn:
                    return (bool)_isWarnEnabledMethod.Invoke(_logger, null);
                case WellTool.Log.Level.Level.Error:
                    return (bool)_isErrorEnabledMethod.Invoke(_logger, null);
                case WellTool.Log.Level.Level.Fatal:
                    return (bool)_isFatalEnabledMethod.Invoke(_logger, null);
                default:
                    return false;
            }
        }

        public override void Log(WellTool.Log.Level.Level level, Exception t, string format, params object[] arguments)
        {
            switch (level)
            {
                case WellTool.Log.Level.Level.Debug:
                    Debug(t, format, arguments);
                    break;
                case WellTool.Log.Level.Level.Info:
                    Info(t, format, arguments);
                    break;
                case WellTool.Log.Level.Level.Warn:
                    Warn(t, format, arguments);
                    break;
                case WellTool.Log.Level.Level.Error:
                    Error(t, format, arguments);
                    break;
                case WellTool.Log.Level.Level.Fatal:
                    Fatal(t, format, arguments);
                    break;
            }
        }

        public override void Log(string fqcn, WellTool.Log.Level.Level level, Exception t, string format, params object[] arguments)
        {
            Log(level, t, format, arguments);
        }

        public override void Trace(Exception t, string format, params object[] arguments)
        {
            Debug(t, format, arguments);
        }

        public override void Debug(Exception t, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            _debugMethod?.Invoke(_logger, new[] { message });
        }

        public override void Info(Exception t, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            _infoMethod?.Invoke(_logger, new[] { message });
        }

        public override void Warn(Exception t, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            _warnMethod?.Invoke(_logger, new[] { message });
        }

        public override void Error(Exception t, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            _errorMethod?.Invoke(_logger, new[] { message });
        }

        public override void Fatal(Exception t, string format, params object[] arguments)
        {
            var message = string.Format(format, arguments);
            _fatalMethod?.Invoke(_logger, new[] { message });
        }
    }
}