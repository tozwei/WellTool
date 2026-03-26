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
    /// 消息摘要
    /// </summary>
    public abstract class Digester
    {
        /// <summary>
        /// 摘要算法
        /// </summary>
        protected readonly DigestAlgorithm Algorithm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">摘要算法</param>
        protected Digester(DigestAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        /// 计算消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要</returns>
        public abstract byte[] Digest(byte[] data);

        /// <summary>
        /// 计算消息摘要
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
        /// 计算消息摘要
        /// </summary>
        /// <param name="stream">待计算的流</param>
        /// <returns>消息摘要</returns>
        public abstract byte[] Digest(Stream stream);

        /// <summary>
        /// 获取哈希算法实例
        /// </summary>
        /// <param name="algorithm">摘要算法</param>
        /// <returns>哈希算法实例</returns>
        protected HashAlgorithm GetHashAlgorithm()
        {
            switch (Algorithm)
            {
                case DigestAlgorithm.MD5:
                    return new System.Security.Cryptography.MD5CryptoServiceProvider();
                case DigestAlgorithm.SHA1:
                    return new System.Security.Cryptography.SHA1Managed();
                case DigestAlgorithm.SHA256:
                    return new System.Security.Cryptography.SHA256Managed();
                case DigestAlgorithm.SHA384:
                    return new System.Security.Cryptography.SHA384Managed();
                case DigestAlgorithm.SHA512:
                    return new System.Security.Cryptography.SHA512Managed();
                default:
                    throw new CryptoException("Unsupported digest algorithm: {0}", Algorithm);
            }
        }
    }
}
