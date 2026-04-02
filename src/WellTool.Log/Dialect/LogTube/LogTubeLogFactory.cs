using System;
using System.Reflection;

namespace WellTool.Log.Dialect.LogTube
{
    /// <summary>
    /// LogTube日志工厂
    /// </summary>
    public class LogTubeLogFactory : LogFactory
    {
        private static readonly Type _logTubeType;
        private static readonly MethodInfo _getLoggerMethod;

        static LogTubeLogFactory()
        {
            try
            {
                _logTubeType = Type.GetType("log.tube.LogTube, logtube");
                if (_logTubeType != null)
                {
                    _getLoggerMethod = _logTubeType.GetMethod("getLogger", new[] { typeof(string) });
                }
            }
            catch
            {
                // 忽略异常，说明LogTube未安装
            }
        }

        public LogTubeLogFactory() : base("LogTube")
        {
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        public static bool IsAvailable => _logTubeType != null && _getLoggerMethod != null;

        public override ILog CreateLog(string name)
        {
            if (_logTubeType == null || _getLoggerMethod == null)
            {
                return null;
            }

            try
            {
                var logger = _getLoggerMethod.Invoke(null, new[] { name });
                return new LogTubeLog(name, logger);
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