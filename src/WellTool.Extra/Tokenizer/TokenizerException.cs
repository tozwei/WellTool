using System;

namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 分词异常
    /// </summary>
    public class TokenizerException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public TokenizerException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public TokenizerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public TokenizerException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public TokenizerException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="format">消息格式</param>
        /// <param name="args">消息参数</param>
        public TokenizerException(Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
        {
        }
    }
}