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

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// 对称加密算法
    /// </summary>
    public enum SymmetricAlgorithm
    {
        /// <summary>
        /// AES算法
        /// </summary>
        AES,

        /// <summary>
        /// DES算法
        /// </summary>
        DES,

        /// <summary>
        /// 3DES算法
        /// </summary>
        DESede,

        /// <summary>
        /// RC4算法
        /// </summary>
        RC4,

        /// <summary>
        /// ChaCha20算法
        /// </summary>
        ChaCha20,

        /// <summary>
        /// SM4算法
        /// </summary>
        SM4,

        /// <summary>
        /// XXTEA算法
        /// </summary>
        XXTEA,

        /// <summary>
        /// ZUC算法
        /// </summary>
        ZUC,

        /// <summary>
        /// Vigenere算法
        /// </summary>
        Vigenere
    }
}
