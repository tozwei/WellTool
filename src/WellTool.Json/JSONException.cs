using System;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 异常
    /// </summary>
    public class JSONException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JSONException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public JSONException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public JSONException(string message, Exception innerException) : base(message, innerException) { }
    }
}