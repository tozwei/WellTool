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

using System;
using System.Globalization;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 线程工具异常
    /// </summary>
    public class ThreadException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        public ThreadException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public ThreadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerException">内部异常</param>
        public ThreadException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageTemplate">消息模板</param>
        /// <param name="args">参数</param>
        public ThreadException(string messageTemplate, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, messageTemplate, args))
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <param name="messageTemplate">消息模板</param>
        /// <param name="args">参数</param>
        public ThreadException(Exception innerException, string messageTemplate, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, messageTemplate, args), innerException)
        {
        }
    }
}