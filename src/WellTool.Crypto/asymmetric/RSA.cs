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

namespace WellTool.Crypto.Asymmetric
{
    /// <summary>
    /// RSA加密
    /// </summary>
    public class RSA : AsymmetricCrypto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        public RSA(byte[] publicKey, byte[]? privateKey = null) : base(AsymmetricAlgorithm.RSA, publicKey, privateKey) { }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public override byte[] Encrypt(byte[] data)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(PublicKey, out _);
                return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <returns>解密后的数据</returns>
        public override byte[] Decrypt(byte[] data)
        {
            if (PrivateKey == null)
            {
                throw new CryptoException("Private key is required for decryption");
            }

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(PrivateKey, out _);
                return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (byte[] publicKey, byte[] privateKey) GenerateKeyPair(int keySize = 2048)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create(keySize))
            {
                byte[] publicKey = rsa.ExportSubjectPublicKeyInfo();
                byte[] privateKey = rsa.ExportPkcs8PrivateKey();
                return (publicKey, privateKey);
            }
        }
    }
}
