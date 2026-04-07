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

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// 3DES加密
    /// </summary>
    public class DESede : SymmetricCrypto
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DESede() : base(SymmetricAlgorithmType.DESede, new byte[24]) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        public DESede(byte[] key, byte[]? iv = null) : base(SymmetricAlgorithmType.DESede, key, iv) { }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public override byte[] Encrypt(byte[] data)
        {
            using (var tripleDes = (TripleDES)GetSymmetricAlgorithm())
            {
                tripleDes.Key = Key;
                if (IV != null)
                {
                    tripleDes.IV = IV;
                }
                else
                {
                    tripleDes.GenerateIV();
                }
                tripleDes.Mode = System.Security.Cryptography.CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;

                using (var encryptor = tripleDes.CreateEncryptor())
                {
                    byte[] encrypted = Encrypt(data, encryptor);
                    byte[] result = new byte[tripleDes.IV.Length + encrypted.Length];
                    Array.Copy(tripleDes.IV, 0, result, 0, tripleDes.IV.Length);
                    Array.Copy(encrypted, 0, result, tripleDes.IV.Length, encrypted.Length);
                    return result;
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <returns>解密后的数据</returns>
        public override byte[] Decrypt(byte[] data)
        {
            using (var tripleDes = (TripleDES)GetSymmetricAlgorithm())
            {
                tripleDes.Key = Key;
                tripleDes.Mode = System.Security.Cryptography.CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;

                int blockSize = tripleDes.BlockSize / 8;
                if (IV != null)
                {
                    tripleDes.IV = IV;
                }
                else if (data.Length > blockSize)
                {
                    byte[] iv = new byte[blockSize];
                    Array.Copy(data, 0, iv, 0, blockSize);
                    tripleDes.IV = iv;
                }

                byte[] cipherText = new byte[data.Length - blockSize];
                Array.Copy(data, blockSize, cipherText, 0, cipherText.Length);

                using (var decryptor = tripleDes.CreateDecryptor())
                {
                    return Decrypt(cipherText, decryptor);
                }
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="encryptor">加密器</param>
        /// <returns>加密后的数据</returns>
        private byte[] Encrypt(byte[] data, ICryptoTransform encryptor)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="decryptor">解密器</param>
        /// <returns>解密后的数据</returns>
        private byte[] Decrypt(byte[] data, ICryptoTransform decryptor)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var msDecrypt = new MemoryStream())
                    {
                        cs.CopyTo(msDecrypt);
                        return msDecrypt.ToArray();
                    }
                }
            }
        }
    }
}
