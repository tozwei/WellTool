using System;

namespace WellTool.Extra.Qrcode
{
    /// <summary>
    /// 二维码异常
    /// </summary>
    public class QrCodeException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public QrCodeException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public QrCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public QrCodeException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public QrCodeException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public QrCodeException(Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
        {
        }
    }
}