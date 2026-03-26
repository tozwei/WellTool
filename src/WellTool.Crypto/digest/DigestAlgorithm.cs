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

namespace WellTool.Crypto.Digest
{
    /// <summary>
    /// 消息摘要算法
    /// </summary>
    public enum DigestAlgorithm
    {
        /// <summary>
        /// MD5算法
        /// </summary>
        MD5,

        /// <summary>
        /// SHA1算法
        /// </summary>
        SHA1,

        /// <summary>
        /// SHA256算法
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA384算法
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA512算法
        /// </summary>
        SHA512,

        /// <summary>
        /// SM3算法
        /// </summary>
        SM3
    }
}
