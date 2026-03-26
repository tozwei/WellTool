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
using System.Runtime.Serialization;

namespace WellTool.Crypto
{
    /// <summary>
    /// 加密异常
    /// </summary>
    [Serializable]
    public class CryptoException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CryptoException() : base() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        public CryptoException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public CryptoException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">参数</param>
        public CryptoException(string format, params object[] args) : base(string.Format(format, args)) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">序列化信息</param>
        /// <param name="context">序列化上下文</param>
        protected CryptoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
