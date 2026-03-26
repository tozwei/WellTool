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

namespace WellTool.Crypto.Asymmetric
{
    /// <summary>
    /// 非对称加密算法
    /// </summary>
    public enum AsymmetricAlgorithm
    {
        /// <summary>
        /// RSA算法
        /// </summary>
        RSA,

        /// <summary>
        /// DSA算法
        /// </summary>
        DSA,

        /// <summary>
        /// ECDSA算法
        /// </summary>
        ECDSA,

        /// <summary>
        /// SM2算法
        /// </summary>
        SM2
    }
}
