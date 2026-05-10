using System;
using System.Diagnostics;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Jdk
{
    /// <summary>
    /// System.Diagnostics.TraceSource 日志实现
    /// </summary>
    public class JdkLog : AbstractLog
    {
        private readonly TraceSource _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">TraceSource对象</param>
        public JdkLog(TraceSource logger) : base(logger.Name)
        {
            _logger = logger;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clazz">类</param>
        public JdkLog(Type clazz) : base(clazz)
        {
            _logger = new TraceSource(clazz?.FullName ?? "null");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">日志名称</param>
        public JdkLog(string name) : base(name)
        {
            _logger = new TraceSource(name);
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
                    return _logger.Switch.ShouldTrace(TraceEventType.Verbose);
                case Level.Level.Debug:
                    return _logger.Switch.ShouldTrace(TraceEventType.Verbose);
                case Level.Level.Info:
                    return _logger.Switch.ShouldTrace(TraceEventType.Information);
                case Level.Level.Warn:
                    return _logger.Switch.ShouldTrace(TraceEventType.Warning);
                case Level.Level.Error:
                    return _logger.Switch.ShouldTrace(TraceEventType.Error);
                case Level.Level.Fatal:
                    return _logger.Switch.ShouldTrace(TraceEventType.Critical);
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
            TraceEventType eventType;
            switch (level)
            {
                case Level.Level.Trace:
                    eventType = TraceEventType.Verbose;
                    break;
                case Level.Level.Debug:
                    eventType = TraceEventType.Verbose;
                    break;
                case Level.Level.Info:
                    eventType = TraceEventType.Information;
                    break;
                case Level.Level.Warn:
                    eventType = TraceEventType.Warning;
                    break;
                case Level.Level.Error:
                    eventType = TraceEventType.Error;
                    break;
                case Level.Level.Fatal:
                    eventType = TraceEventType.Critical;
                    break;
                default:
                    throw new Exception($"Can not identify level: {level}");
            }
            LogIfEnabled(eventType, t, format, arguments);
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
            LogIfEnabled(TraceEventType.Verbose, t, format, arguments);
        }

        /// <summary>
        /// 打印Debug级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Debug(Exception t, string format, params object[] arguments)
        {
            LogIfEnabled(TraceEventType.Verbose, t, format, arguments);
        }

        /// <summary>
        /// 打印Info级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Info(Exception t, string format, params object[] arguments)
        {
            LogIfEnabled(TraceEventType.Information, t, format, arguments);
        }

        /// <summary>
        /// 打印Warn级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Warn(Exception t, string format, params object[] arguments)
        {
            LogIfEnabled(TraceEventType.Warning, t, format, arguments);
        }

        /// <summary>
        /// 打印Error级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Error(Exception t, string format, params object[] arguments)
        {
            LogIfEnabled(TraceEventType.Error, t, format, arguments);
        }

        /// <summary>
        /// 打印Fatal级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Fatal(Exception t, string format, params object[] arguments)
        {
            LogIfEnabled(TraceEventType.Critical, t, format, arguments);
        }

        /// <summary>
        /// 打印对应等级的日志
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="throwable">异常对象</param>
        /// <param name="format">消息模板</param>
        /// <param name="arguments">参数</param>
        private void LogIfEnabled(TraceEventType eventType, Exception throwable, string format, object[] arguments)
        {
            if (_logger.Switch.ShouldTrace(eventType))
            {
                string message = string.Format(format, arguments);
                if (throwable != null)
                {
                    message += Environment.NewLine + throwable.ToString();
                }
                _logger.TraceEvent(eventType, 0, message);
            }
        }
    }
}