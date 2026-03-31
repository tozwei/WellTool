using System;

namespace WellTool.JWT.Exceptions
{
    /// <summary>
    /// 验证异常
    /// </summary>
    public class ValidateException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ValidateException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public ValidateException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public ValidateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
