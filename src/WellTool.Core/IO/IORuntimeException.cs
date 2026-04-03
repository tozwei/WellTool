using System;
using System.IO;

namespace WellTool.Core.IO
{
    /// <summary>
    /// IO运行时异常
    /// </summary>
    public class IORuntimeException : System.Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IORuntimeException() : base() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public IORuntimeException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public IORuntimeException(string message, System.Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public IORuntimeException(IOException innerException) : base(innerException.Message, innerException) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public IORuntimeException(System.Exception innerException) : base(innerException.Message, innerException) { }
    }
}