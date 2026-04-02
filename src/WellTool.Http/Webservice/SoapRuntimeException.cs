using System;

namespace WellTool.Http.Webservice
{
    /// <summary>
    /// SOAP运行时异常
    /// </summary>
    public class SoapRuntimeException : Exception
    {
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
    }
}