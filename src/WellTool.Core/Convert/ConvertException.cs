using System;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 转换异常
    /// </summary>
    public class ConvertException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConvertException() : base() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public ConvertException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public ConvertException(string message, Exception innerException) : base(message, innerException) { }
    }
}