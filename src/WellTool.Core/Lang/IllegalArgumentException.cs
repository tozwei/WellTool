using System;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 非法参数异常
    /// </summary>
    public class IllegalArgumentException : ArgumentException
    {
        /// <summary>
        /// 创建异常实例
        /// </summary>
        public IllegalArgumentException() : base()
        {
        }

        /// <summary>
        /// 创建异常实例
        /// </summary>
        public IllegalArgumentException(string message) : base(message)
        {
        }

        /// <summary>
        /// 创建异常实例
        /// </summary>
        public IllegalArgumentException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 创建异常实例
        /// </summary>
        public IllegalArgumentException(string message, string paramName) : base(message, paramName)
        {
        }

        /// <summary>
        /// 工厂方法：创建新实例
        /// </summary>
        public static IllegalArgumentException New(string? message = null)
        {
            return new IllegalArgumentException(message ?? "Illegal argument");
        }
    }
}
