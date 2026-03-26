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

namespace WellTool.Crypto.Digest
{
    /// <summary>
    /// 通用消息摘要实现
    /// </summary>
    public class GeneralDigester : Digester
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">摘要算法</param>
        public GeneralDigester(DigestAlgorithm algorithm) : base(algorithm) { }

        /// <summary>
        /// 计算消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要</returns>
        public override byte[] Digest(byte[] data)
        {
            using (var hashAlgorithm = GetHashAlgorithm())
            {
                return hashAlgorithm.ComputeHash(data);
            }
        }

        /// <summary>
        /// 计算消息摘要
        /// </summary>
        /// <param name="stream">待计算的流</param>
        /// <returns>消息摘要</returns>
        public override byte[] Digest(Stream stream)
        {
            using (var hashAlgorithm = GetHashAlgorithm())
            {
                return hashAlgorithm.ComputeHash(stream);
            }
        }
    }
}
