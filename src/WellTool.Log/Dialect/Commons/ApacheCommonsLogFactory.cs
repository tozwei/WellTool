using System;

namespace WellTool.Log.Dialect.Commons
{
    /// <summary>
    /// Apache Commons Logging 日志工厂
    /// </summary>
    public class ApacheCommonsLogFactory : LogFactory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApacheCommonsLogFactory() : base("Apache Common Logging")
        {
            // 暂时注释掉，因为Apache Common Logging库不存在
            // CheckLogExist(typeof(Apache.Common.Logging.LogManager));
        }

        /// <summary>
        /// 创建日志对象
        /// </summary>
        /// <param name="name">日志名称</param>
        /// <returns>日志对象</returns>
        public override ILog CreateLog(string name)
        {
            return new ApacheCommonsLog(name);
        }

        /// <summary>
        /// 创建日志对象
        /// </summary>
        /// <param name="clazz">类</param>
        /// <returns>日志对象</returns>
        public override ILog CreateLog(Type clazz)
        {
            return new ApacheCommonsLog(clazz);
        }

        /// <summary>
        /// 检查日志实现是否存在
        /// </summary>
        /// <param name="logClassName">日志类名</param>
        protected override void CheckLogExist(Type logClassName)
        {
            base.CheckLogExist(logClassName);
            // Commons Logging在调用GetLogger时才检查是否有日志实现，在此提前检查
            GetLog(typeof(ApacheCommonsLogFactory));
        }
    }

    // /// <summary>
    // /// Apache Commons Logging for Log4j
    // /// </summary>
    // public class ApacheCommonsLog4JLog : Dialect.Log4Net.Log4NetLog
    // {
    //     /// <summary>
    //     /// 构造函数
    //     /// </summary>
    //     /// <param name="clazz">类</param>
    //     public ApacheCommonsLog4JLog(Type clazz) : base(clazz)
    //     {}

    //     /// <summary>
    //     /// 构造函数
    //     /// </summary>
    //     /// <param name="name">日志名称</param>
    //     public ApacheCommonsLog4JLog(string name) : base(name)
    //     {}
    // }
}