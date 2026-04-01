using System;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件异常
    /// </summary>
    public class MailException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public MailException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public MailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public MailException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public MailException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public MailException(Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
        {
        }
    }
}