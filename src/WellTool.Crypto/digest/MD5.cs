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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    /// <summary>
    /// MD5消息摘要
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5()
        {
        }

        /// <summary>
        /// 计算MD5消息摘要（实例方法）
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要</returns>
        public byte[] Digest(byte[] data)
        {
            using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(data);
            }
        }

        /// <summary>
        /// 计算MD5消息摘要（实例方法）
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public string DigestHex(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] digest = Digest(bytes);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算MD5消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要</returns>
        public static byte[] Digest(byte[] data)
        {
            using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(data);
            }
        }

        /// <summary>
        /// 计算MD5消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string DigestHex(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] digest = Digest(bytes);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算MD5消息摘要
        /// </summary>
        /// <param name="stream">待计算的流</param>
        /// <returns>消息摘要</returns>
        public static byte[] Digest(Stream stream)
        {
            using (var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(stream);
            }
        }
    }
}
