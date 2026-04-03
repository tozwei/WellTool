using System;
using System.Runtime.Serialization;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件异常
    /// </summary>
    [Serializable]
    public class MailException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        public MailException()
        {
        }

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
        /// <param name="info">序列化信息</param>
        /// <param name="context">流上下文</param>
        protected MailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
