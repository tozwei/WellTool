using System;
using System.Reflection;

namespace WellTool.Log.Dialect.Log4Net2
{
    /// <summary>
    /// Log4Net2日志工厂
    /// </summary>
    internal class Log4Net2LogFactory : LogFactory
    {
        private static readonly Type _logManagerType;
        private static readonly MethodInfo _getLoggerMethod;

        static Log4Net2LogFactory()
        {
            try
            {
                _logManagerType = Type.GetType("log4net.LogManager, log4net");
                if (_logManagerType != null)
                {
                    _getLoggerMethod = _logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
                }
            }
            catch
            {
                // 忽略异常，说明Log4Net2未安装
            }
        }

        public Log4Net2LogFactory() : base("Log4Net2")
        {
        }

        public override ILog CreateLog(string name)
        {
            if (_logManagerType == null || _getLoggerMethod == null)
            {
                return null;
            }

            try
            {
                var logger = _getLoggerMethod.Invoke(null, new[] { name });
                return new Log4Net2Log(name, logger);
            }
            catch
            {
                return null;
            }
        }

        public override ILog CreateLog(Type type)
        {
            return CreateLog(type.FullName);
        }
    }
}