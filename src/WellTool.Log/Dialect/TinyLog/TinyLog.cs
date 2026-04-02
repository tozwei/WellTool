using System;
using System.Reflection;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.TinyLog
{
    /// <summary>
    /// TinyLog 1.x日志实现
    /// </summary>
    internal class TinyLog : AbstractLog
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

        public TinyLog(string name, object logger) : base(name)
        {
            _logger = logger;
            var loggerType = logger.GetType();
            _debugMethod = loggerType.GetMethod("debug", new[] { typeof(object) });
            _infoMethod = loggerType.GetMethod("info", new[] { typeof(object) });
            _warnMethod = loggerType.GetMethod("warn", new[] { typeof(object) });
            _errorMethod = loggerType.GetMethod("error", new[] { typeof(object) });
            _fatalMethod = loggerType.GetMethod("fatal", new[] { typeof(object) });
            _isDebugEnabledMethod = loggerType.GetProperty("isDebugEnabled").GetGetMethod();
            _isInfoEnabledMethod = loggerType.GetProperty("isInfoEnabled").GetGetMethod();
            _isWarnEnabledMethod = loggerType.GetProperty("isWarnEnabled").GetGetMethod();
            _isErrorEnabledMethod = loggerType.GetProperty("isErrorEnabled").GetGetMethod();
            _isFatalEnabledMethod = loggerType.GetProperty("isFatalEnabled").GetGetMethod();
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