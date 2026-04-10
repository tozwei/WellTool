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
        public RSA() : base(AsymmetricAlgorithm.RSA, GenerateKeyPair().publicKey, GenerateKeyPair().privateKey) { }

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
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>加密后的数据</returns>
        public byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
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
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>解密后的数据</returns>
        public byte[] Decrypt(byte[] data, byte[] privateKey)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(privateKey, out _);
                return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <returns>签名后的数据</returns>
        public byte[] Sign(byte[] data)
        {
            if (PrivateKey == null)
            {
                throw new CryptoException("Private key is required for signing");
            }

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(PrivateKey, out _);
                return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名后的数据</returns>
        public byte[] Sign(byte[] data, byte[] privateKey)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(privateKey, out _);
                return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="data">待验签数据</param>
        /// <param name="signature">签名数据</param>
        /// <returns>验签结果</returns>
        public bool Verify(byte[] data, byte[] signature)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(PublicKey, out _);
                return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="data">待验签数据</param>
        /// <param name="signature">签名数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>验签结果</returns>
        public bool Verify(byte[] data, byte[] signature, byte[] publicKey)
        {
            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
                return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        /// <summary>
        /// 导出公钥
        /// </summary>
        /// <returns>公钥</returns>
        public byte[] ExportPublicKey()
        {
            return PublicKey;
        }

        /// <summary>
        /// 导出私钥
        /// </summary>
        /// <returns>私钥</returns>
        public byte[] ExportPrivateKey()
        {
            if (PrivateKey == null)
            {
                throw new CryptoException("Private key is not available");
            }
            return PrivateKey;
        }

        /// <summary>
        /// 导入公钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        public void ImportPublicKey(byte[] publicKey)
        {
            PublicKey = publicKey;
        }

        /// <summary>
        /// 导入私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        public void ImportPrivateKey(byte[] privateKey)
        {
            PrivateKey = privateKey;
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

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <returns>包含公钥和私钥的元组</returns>
        public (byte[] publicKey, byte[] privateKey) GenerateKeyPair()
        {
            return GenerateKeyPair(2048);
        }
    }
}
