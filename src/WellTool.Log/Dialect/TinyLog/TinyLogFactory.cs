using System;
using System.Reflection;

namespace WellTool.Log.Dialect.TinyLog
{
    /// <summary>
    /// TinyLog 1.x日志工厂
    /// </summary>
    internal class TinyLogFactory : LogFactory
    {
        private static readonly Type _loggerType;
        private static readonly MethodInfo _getLoggerMethod;

        static TinyLogFactory()
        {
            try
            {
                _loggerType = Type.GetType("tinylog.Logger, tinylog");
                if (_loggerType != null)
                {
                    _getLoggerMethod = _loggerType.GetMethod("getLogger", new[] { typeof(string) });
                }
            }
            catch
            {
                // 忽略异常，说明TinyLog 1.x未安装
            }
        }

        public TinyLogFactory() : base("TinyLog")
        {
        }

        public override ILog CreateLog(string name)
        {
            if (_loggerType == null || _getLoggerMethod == null)
            {
                return null;
            }

            try
            {
                var logger = _getLoggerMethod.Invoke(null, new[] { name });
                return new TinyLog(name, logger);
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