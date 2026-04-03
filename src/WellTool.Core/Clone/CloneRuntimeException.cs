using System;

namespace WellTool.Core.Clone
{
    /// <summary>
    /// 克隆运行时异常
    /// </summary>
    public class CloneRuntimeException : System.Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CloneRuntimeException() : base() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public CloneRuntimeException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public CloneRuntimeException(string message, System.Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public CloneRuntimeException(System.Exception innerException) : base(innerException.Message, innerException) { }
    }
}