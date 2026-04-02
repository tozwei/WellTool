using System;
using System.Runtime.Serialization;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// SOAP运行时异常
    /// </summary>
    [Serializable]
    public class SoapRuntimeException : Exception
    {
        private static readonly long SerialVersionUID = 8247610319171014183L;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">错误消息</param>
        public SoapRuntimeException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public SoapRuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <param name="args">格式化参数</param>
        public SoapRuntimeException(string format, params object[] args) 
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="format">格式字符串</param>
        /// <param name="args">格式化参数</param>
        public SoapRuntimeException(Exception innerException, string format, params object[] args) 
            : base(string.Format(format, args), innerException)
        {
        }

        /// <summary>
        /// 反序列化构造
        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">上下文</param>
        protected SoapRuntimeException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
