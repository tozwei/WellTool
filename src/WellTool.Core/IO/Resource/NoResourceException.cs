// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace WellTool.Core.IO.Resource
{
    /// <summary>
    /// 资源文件或资源不存在异常
    /// </summary>
    public class NoResourceException : IORuntimeException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e">异常</param>
        public NoResourceException(System.Exception e) : base(e.Message, e)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        public NoResourceException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageTemplate">消息模板</param>
        /// <param name="args">参数</param>
        public NoResourceException(string messageTemplate, params object[] args) : base(string.Format(messageTemplate, args))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="throwable">异常</param>
        public NoResourceException(string message, System.Exception throwable) : base(message, throwable)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="throwable">异常</param>
        /// <param name="messageTemplate">消息模板</param>
        /// <param name="args">参数</param>
        public NoResourceException(System.Exception throwable, string messageTemplate, params object[] args) : base(string.Format(messageTemplate, args), throwable)
        {
        }

        /// <summary>
        /// 导致这个异常的异常是否是指定类型的异常
        /// </summary>
        /// <param name="clazz">异常类型</param>
        /// <returns>是否为指定类型异常</returns>
        public bool CauseInstanceOf(Type clazz)
        {
            var cause = InnerException;
            return cause != null && clazz.IsInstanceOfType(cause);
        }
    }
}