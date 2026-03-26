using System;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 比较异常
    /// </summary>
    public class ComparatorException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComparatorException() : base() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public ComparatorException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public ComparatorException(string message, Exception innerException) : base(message, innerException) { }
    }
}