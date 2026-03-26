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
using WellTool.Crypto.Asymmetric;
using SymmetricAlgorithm = WellTool.Crypto.Symmetric.SymmetricAlgorithm;

namespace WellTool.Crypto
{
    /// <summary>
    /// 密钥工具类
    /// </summary>
    public static class KeyUtil
    {
        /// <summary>
        /// 生成对称加密密钥
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>密钥</returns>
        public static byte[] GenerateSymmetricKey(SymmetricAlgorithm algorithm)
        {
            using (var symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                symmetricAlgorithm.GenerateKey();
                return symmetricAlgorithm.Key;
            }
        }

        /// <summary>
        /// 生成对称加密初始化向量
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>初始化向量</returns>
        public static byte[] GenerateIV(SymmetricAlgorithm algorithm)
        {
            using (var symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                symmetricAlgorithm.GenerateIV();
                return symmetricAlgorithm.IV;
            }
        }

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (byte[] publicKey, byte[] privateKey) GenerateRsaKeyPair(int keySize = 2048)
        {
            return Asymmetric.RSA.GenerateKeyPair(keySize);
        }

        /// <summary>
        /// 从文件加载密钥
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>密钥</returns>
        public static byte[] LoadKeyFromFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 保存密钥到文件
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveKeyToFile(byte[] key, string filePath)
        {
            File.WriteAllBytes(filePath, key);
        }

        /// <summary>
        /// 获取对称加密算法实例
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>对称加密算法实例</returns>
        private static System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case SymmetricAlgorithm.AES:
                    return System.Security.Cryptography.Aes.Create();
                case SymmetricAlgorithm.DES:
                    return System.Security.Cryptography.DES.Create();
                case SymmetricAlgorithm.DESede:
                    return System.Security.Cryptography.TripleDES.Create();
                default:
                    throw new CryptoException("Unsupported symmetric algorithm: {0}", algorithm);
            }
        }
    }
}
