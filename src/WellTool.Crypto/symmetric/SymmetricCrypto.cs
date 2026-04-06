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
        /// 字节数组转十六进制字符串
        /// </summary>
        protected static string ToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        protected static byte[] FromHexString(string hex)
        {
            if (hex == null) throw new ArgumentNullException(nameof(hex));
            if (hex.Length % 2 != 0) throw new ArgumentException("Hex string must have even length");

            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return result;
        }
        /// <summary>
        /// 加密算法
        /// </summary>
        protected readonly SymmetricAlgorithmType Algorithm;

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
        protected SymmetricCrypto(SymmetricAlgorithmType algorithm, byte[] key, byte[]? iv = null)
        {
            Algorithm = algorithm;
            Key = key ?? throw new CryptoException("Key cannot be null");
            IV = iv;
        }

        /// <summary>
        /// 加密并返回十六进制字符串
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>加密后的十六进制字符串</returns>
        public virtual string EncryptHex(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] encrypted = Encrypt(bytes);
            return ToHexString(encrypted);
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="encryptedHex">加密的十六进制字符串</param>
        /// <returns>解密后的字符串</returns>
        public virtual string DecryptStr(string encryptedHex)
        {
            byte[] encrypted = FromHexString(encryptedHex);
            byte[] decrypted = Decrypt(encrypted);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="encryptedHex">加密的十六进制字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public virtual string DecryptStr(string encryptedHex, Encoding encoding)
        {
            byte[] encrypted = FromHexString(encryptedHex);
            byte[] decrypted = Decrypt(encrypted);
            return encoding.GetString(decrypted);
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
        /// <returns>对称加密算法实例</returns>
        protected System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm()
        {
            switch (Algorithm)
            {
                case SymmetricAlgorithmType.AES:
                    return new System.Security.Cryptography.AesManaged();
                case SymmetricAlgorithmType.DES:
                    return new System.Security.Cryptography.DESCryptoServiceProvider();
                case SymmetricAlgorithmType.DESede:
                    return new System.Security.Cryptography.TripleDESCryptoServiceProvider();
                default:
                    throw new CryptoException("Unsupported symmetric algorithm: {0}", Algorithm);
            }
        }
    }
}
