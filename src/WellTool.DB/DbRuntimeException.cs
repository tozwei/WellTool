using System;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库运行时异常
    /// </summary>
    public class DbRuntimeException : DbException
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DbRuntimeException() : base()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public DbRuntimeException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public DbRuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
