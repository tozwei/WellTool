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

using System.Text;
using WellTool.Core.Codec;

namespace WellTool.Core.Net
{
    /// <summary>
    /// RFC 3986 规范的 URL 编码实现
    /// </summary>
    public static class RFC3986
    {
        /// <summary>
        /// 未保留字符集
        /// </summary>
        public static readonly PercentCodec Unreserved = PercentCodec.Of("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._~");

        /// <summary>
        /// 路径片段编码
        /// </summary>
        public static class Fragment
        {
            /// <summary>
            /// 编码片段
            /// </summary>
            /// <param name="fragment">片段</param>
            /// <param name="encoding">编码</param>
            /// <param name="safeChars">安全字符</param>
            /// <returns>编码后的片段</returns>
            public static string Encode(string fragment, Encoding encoding, params char[] safeChars)
            {
                return Unreserved.Encode(fragment, encoding, safeChars);
            }
        }

        /// <summary>
        /// 路径段编码
        /// </summary>
        public static class Segment
        {
            /// <summary>
            /// 编码路径段
            /// </summary>
            /// <param name="segment">路径段</param>
            /// <param name="encoding">编码</param>
            /// <param name="safeChars">安全字符</param>
            /// <returns>编码后的路径段</returns>
            public static string Encode(string segment, Encoding encoding, params char[] safeChars)
            {
                return Unreserved.Encode(segment, encoding, safeChars);
            }
        }

        /// <summary>
        /// 查询参数名称编码（严格模式）
        /// </summary>
        public static readonly PercentCodec QUERY_PARAM_NAME_STRICT = Unreserved;

        /// <summary>
        /// 查询参数值编码（严格模式）
        /// </summary>
        public static readonly PercentCodec QUERY_PARAM_VALUE_STRICT = Unreserved;

        /// <summary>
        /// 查询参数名称编码
        /// </summary>
        public static readonly PercentCodec QUERY_PARAM_NAME = Unreserved;

        /// <summary>
        /// 查询参数值编码
        /// </summary>
        public static readonly PercentCodec QUERY_PARAM_VALUE = Unreserved;
    }
}