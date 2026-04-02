using System;

namespace WellTool.Log.Dialect.Jdk
{
    /// <summary>
    /// System.Diagnostics.TraceSource 日志工厂
    /// </summary>
    public class JdkLogFactory : LogFactory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JdkLogFactory() : base("JDK Logging")
        {
            // JDK日志在.NET中对应System.Diagnostics.TraceSource，不需要额外检查
        }

        /// <summary>
        /// 创建日志对象
        /// </summary>
        /// <param name="name">日志名称</param>
        /// <returns>日志对象</returns>
        public override ILog CreateLog(string name)
        {
            return new JdkLog(name);
        }

        /// <summary>
        /// 创建日志对象
        /// </summary>
        /// <param name="clazz">类</param>
        /// <returns>日志对象</returns>
        public override ILog CreateLog(Type clazz)
        {
            return new JdkLog(clazz);
        }
    }
}