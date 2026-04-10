using System;

namespace WellTool.Jwt.Exceptions
{
    /// <summary>
    /// JWT异常
    /// </summary>
    public class JWTException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JWTException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        public JWTException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public JWTException(string message, Exception innerException) : base(message, innerException) { }
    }
}
