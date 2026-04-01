using System.Collections.Generic;

namespace WellTool.Extra.Validation
{
    /// <summary>
    /// bean 校验结果
    /// </summary>
    public class BeanValidationResult
    {
        /// <summary>
        /// 校验是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public List<ErrorMessage> ErrorMessages { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="success">是否验证成功</param>
        public BeanValidationResult(bool success)
        {
            Success = success;
            ErrorMessages = new List<ErrorMessage>();
        }

        /// <summary>
        /// 增加错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>this</returns>
        public BeanValidationResult AddErrorMessage(ErrorMessage errorMessage)
        {
            ErrorMessages.Add(errorMessage);
            return this;
        }

        /// <summary>
        /// 错误消息，包括字段名（字段路径）、消息内容和字段值
        /// </summary>
        public class ErrorMessage
        {
            /// <summary>
            /// 属性字段名称
            /// </summary>
            public string PropertyName { get; set; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// 错误值
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// 重写ToString方法
            /// </summary>
            /// <returns>字符串表示</returns>
            public override string ToString()
            {
                return string.Format("ErrorMessage{{propertyName='{0}', message='{1}', value={2}}}", PropertyName, Message, Value);
            }
        }
    }
}