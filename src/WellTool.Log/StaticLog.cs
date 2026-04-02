using System;
using System.Diagnostics;
using LevelEnum = WellTool.Log.Level.Level;

namespace WellTool.Log
{
    /// <summary>
    /// 静态日志类，用于在不引入日志对象的情况下打印日志
    /// </summary>
    public static class StaticLog
    {
        private static readonly string FQCN = typeof(StaticLog).FullName;

        // ----------------------------------------------------------- Log method start
        // ------------------------ Trace
        /// <summary>
        /// Trace等级日志，小于debug
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Trace(string format, params object[] arguments)
        {
            Trace(LogFactory.Get(GetCallerCaller()), format, arguments);
        }

        /// <summary>
        /// Trace等级日志，小于Debug
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Trace(ILog log, string format, params object[] arguments)
        {
            log.Trace(FQCN, null, format, arguments);
        }

        // ------------------------ debug
        /// <summary>
        /// Debug等级日志，小于Info
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Debug(string format, params object[] arguments)
        {
            Debug(LogFactory.Get(GetCallerCaller()), format, arguments);
        }

        /// <summary>
        /// Debug等级日志，小于Info
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Debug(ILog log, string format, params object[] arguments)
        {
            log.Debug(FQCN, null, format, arguments);
        }

        // ------------------------ info
        /// <summary>
        /// Info等级日志，小于Warn
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Info(string format, params object[] arguments)
        {
            Info(LogFactory.Get(GetCallerCaller()), format, arguments);
        }

        /// <summary>
        /// Info等级日志，小于Warn
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Info(ILog log, string format, params object[] arguments)
        {
            log.Info(FQCN, null, format, arguments);
        }

        // ------------------------ warn
        /// <summary>
        /// Warn等级日志，小于Error
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Warn(string format, params object[] arguments)
        {
            Warn(LogFactory.Get(GetCallerCaller()), format, arguments);
        }

        /// <summary>
        /// Warn等级日志，小于Error
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Warn(Exception e, string format, params object[] arguments)
        {
            Warn(LogFactory.Get(GetCallerCaller()), e, string.Format(format, arguments));
        }

        /// <summary>
        /// Warn等级日志，小于Error
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Warn(ILog log, string format, params object[] arguments)
        {
            Warn(log, null, format, arguments);
        }

        /// <summary>
        /// Warn等级日志，小于Error
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Warn(ILog log, Exception e, string format, params object[] arguments)
        {
            log.Warn(FQCN, e, format, arguments);
        }

        // ------------------------ error
        /// <summary>
        /// Error等级日志
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        public static void Error(Exception e)
        {
            Error(LogFactory.Get(GetCallerCaller()), e);
        }

        /// <summary>
        /// Error等级日志
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Error(string format, params object[] arguments)
        {
            Error(LogFactory.Get(GetCallerCaller()), format, arguments);
        }

        /// <summary>
        /// Error等级日志
        /// 由于动态获取Log，效率较低，建议在非频繁调用的情况下使用！！
        /// </summary>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Error(Exception e, string format, params object[] arguments)
        {
            Error(LogFactory.Get(GetCallerCaller()), e, format, arguments);
        }

        /// <summary>
        /// Error等级日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        public static void Error(ILog log, Exception e)
        {
            Error(log, e, e.Message);
        }

        /// <summary>
        /// Error等级日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Error(ILog log, string format, params object[] arguments)
        {
            Error(log, null, format, arguments);
        }

        /// <summary>
        /// Error等级日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <param name="e">需在日志中堆栈打印的异常</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Error(ILog log, Exception e, string format, params object[] arguments)
        {
            log.Error(FQCN, e, format, arguments);
        }

        // ------------------------ Log
        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="t">需在日志中堆栈打印的异常</param>
        /// <param name="format">格式文本，{} 代表变量</param>
        /// <param name="arguments">变量对应的参数</param>
        public static void Log(LevelEnum level, Exception t, string format, params object[] arguments)
        {
            LogFactory.Get(GetCallerCaller()).Log(FQCN, level, t, format, arguments);
        }

        // ----------------------------------------------------------- Log method end

        /// <summary>
        /// 获得Log
        /// </summary>
        /// <param name="clazz">日志发出的类</param>
        /// <returns>Log</returns>
        [Obsolete("请使用 Log.Get(Type)")]
        public static ILog Get(Type clazz)
        {
            return LogFactory.Get(clazz);
        }

        /// <summary>
        /// 获得Log
        /// </summary>
        /// <param name="name">自定义的日志发出者名称</param>
        /// <returns>Log</returns>
        [Obsolete("请使用 Log.Get(string)")]
        public static ILog Get(string name)
        {
            return LogFactory.Get(name);
        }

        /// <summary>
        /// 获得日志，自动判定日志发出者
        /// </summary>
        /// <returns>Log</returns>
        [Obsolete("请使用 Log.Get()")]
        public static ILog Get()
        {
            return LogFactory.Get(GetCallerCaller());
        }

        /// <summary>
        /// 获取调用者的调用者类型
        /// </summary>
        /// <returns>调用者的调用者类型</returns>
        private static Type GetCallerCaller()
        {
            var stackTrace = new StackTrace();
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var method = frame.GetMethod();
                var type = method.DeclaringType;
                if (type != typeof(StaticLog))
                {
                    return type;
                }
            }
            return typeof(StaticLog);
        }
    }
}