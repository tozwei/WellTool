using System;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库运行时异常
    /// </summary>
    public class DbRuntimeException : Exception
    {
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

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public DbRuntimeException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public DbRuntimeException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public DbRuntimeException(Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
        {
        }
    }
}