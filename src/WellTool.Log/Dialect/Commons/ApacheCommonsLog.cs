using System;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Commons
{
    /// <summary>
    /// Apache Commons Logging
    /// </summary>
    public class ApacheCommonsLog : AbstractLog
    {
        private readonly object _logger;
        private readonly string _name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志对象</param>
        /// <param name="name">日志名称</param>
        public ApacheCommonsLog(object logger, string name) : base(name)
        {
            _logger = logger;
            _name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clazz">类</param>
        public ApacheCommonsLog(Type clazz) : base(clazz)
        {
            _name = clazz?.FullName ?? "null";
            // 暂时使用null，实际使用时需要根据Apache Commons Logging库实现
            _logger = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">日志名称</param>
        public ApacheCommonsLog(string name) : base(name)
        {
            _name = name;
            // 暂时使用null，实际使用时需要根据Apache Commons Logging库实现
            _logger = null;
        }

        /// <summary>
        /// 是否启用指定级别的日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns>是否启用</returns>
        public override bool IsEnabled(Level.Level level)
        {
            return true; // 暂时返回true，实际使用时需要根据Apache Commons Logging库实现
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
                    Trace(t, format, arguments);
                    break;
                case Level.Level.Debug:
                    Debug(t, format, arguments);
                    break;
                case Level.Level.Info:
                    Info(t, format, arguments);
                    break;
                case Level.Level.Warn:
                    Warn(t, format, arguments);
                    break;
                case Level.Level.Error:
                    Error(t, format, arguments);
                    break;
                case Level.Level.Fatal:
                    Error(t, format, arguments); // Fatal级别使用Error级别
                    break;
                default:
                    throw new Exception($"Can not identify level: {level}");
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
            if (IsTraceEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[TRACE] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }

        /// <summary>
        /// 打印Debug级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Debug(Exception t, string format, params object[] arguments)
        {
            if (IsDebugEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[DEBUG] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }

        /// <summary>
        /// 打印Info级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Info(Exception t, string format, params object[] arguments)
        {
            if (IsInfoEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[INFO] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }

        /// <summary>
        /// 打印Warn级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Warn(Exception t, string format, params object[] arguments)
        {
            if (IsWarnEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[WARN] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }

        /// <summary>
        /// 打印Error级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Error(Exception t, string format, params object[] arguments)
        {
            if (IsErrorEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[ERROR] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }

        /// <summary>
        /// 打印Fatal级别日志
        /// </summary>
        /// <param name="t">异常信息</param>
        /// <param name="format">消息格式</param>
        /// <param name="arguments">消息参数</param>
        public override void Fatal(Exception t, string format, params object[] arguments)
        {
            if (IsErrorEnabled())
            {
                // 暂时使用Console输出，实际使用时需要根据Apache Commons Logging库实现
                System.Console.WriteLine($"[FATAL] {string.Format(format, arguments)}");
                if (t != null)
                {
                    System.Console.WriteLine(t);
                }
            }
        }
    }
}