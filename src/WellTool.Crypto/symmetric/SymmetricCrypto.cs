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
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// 对称加密
    /// </summary>
    public abstract class SymmetricCrypto
    {
        /// <summary>
        /// 加密算法
        /// </summary>
        protected readonly SymmetricAlgorithm Algorithm;

        /// <summary>
        /// 密钥
        /// </summary>
        protected readonly byte[] Key;

        /// <summary>
        /// 初始化向量
        /// </summary>
        protected readonly byte[]? IV;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">加密算法</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        protected SymmetricCrypto(SymmetricAlgorithm algorithm, byte[] key, byte[]? iv = null)
        {
            Algorithm = algorithm;
            Key = key ?? throw new CryptoException("Key cannot be null");
            IV = iv;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public abstract byte[] Encrypt(byte[] data);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据（Base64编码）</returns>
        public string Encrypt(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] encrypted = Encrypt(bytes);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <returns>解密后的数据</returns>
        public abstract byte[] Decrypt(byte[] data);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据（Base64编码）</param>
        /// <returns>解密后的数据</returns>
        public string Decrypt(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            byte[] decrypted = Decrypt(bytes);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// 获取对称加密算法实例
        /// </summary>
        /// <param name="algorithm">加密算法</param>
        /// <returns>对称加密算法实例</returns>
        protected System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm()
        {
            switch (Algorithm)
            {
                case SymmetricAlgorithm.AES:
                    return new System.Security.Cryptography.AesManaged();
                case SymmetricAlgorithm.DES:
                    return new System.Security.Cryptography.DESCryptoServiceProvider();
                case SymmetricAlgorithm.DESede:
                    return new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                default:
                    throw new CryptoException("Unsupported symmetric algorithm: {0}", Algorithm);
            }
        }
    }
}
