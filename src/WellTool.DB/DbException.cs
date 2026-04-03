using System;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库异常类
    /// </summary>
    public class DbException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DbException() : base()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public DbException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public DbException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
