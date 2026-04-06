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

namespace WellTool.Crypto.Asymmetric
{
    /// <summary>
    /// 数字签名算法
    /// </summary>
    public enum SignAlgorithm
    {
        /// <summary>
        /// RSA-SHA1算法
        /// </summary>
        RSA_SHA1,

        /// <summary>
        /// RSA-SHA256算法
        /// </summary>
        RSA_SHA256,

        /// <summary>
        /// RSA-SHA384算法
        /// </summary>
        RSA_SHA384,

        /// <summary>
        /// RSA-SHA512算法
        /// </summary>
        RSA_SHA512,

        /// <summary>
        /// ECDSA-SHA1算法
        /// </summary>
        ECDSA_SHA1,

        /// <summary>
        /// ECDSA-SHA256算法
        /// </summary>
        ECDSA_SHA256,

        /// <summary>
        /// ECDSA-SHA384算法
        /// </summary>
        ECDSA_SHA384,

        /// <summary>
        /// ECDSA-SHA512算法
        /// </summary>
        ECDSA_SHA512
    }

    /// <summary>
    /// 数字签名
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// 签名算法
        /// </summary>
        private readonly SignAlgorithm algorithm;

        /// <summary>
        /// 私钥
        /// </summary>
        private readonly byte[] privateKey;

        /// <summary>
        /// 公钥
        /// </summary>
        private readonly byte[] publicKey;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">签名算法</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public Sign(SignAlgorithm algorithm, byte[] privateKey, byte[] publicKey)
        {
            this.algorithm = algorithm;
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        /// <summary>
        /// 根据 AsymmetricAlgorithm 创建 Sign 对象
        /// </summary>
        /// <param name="algorithm">非对称算法</param>
        /// <returns>Sign 对象</returns>
        public static Sign Create(AsymmetricAlgorithm algorithm)
        {
            return new Sign(SignAlgorithm.RSA_SHA256, Array.Empty<byte>(), Array.Empty<byte>());
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="signature">签名</param>
        /// <returns>是否验证通过</returns>
        public bool Verify(byte[] data, byte[] signature)
        {
            return VerifyData(data, signature);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <returns>签名</returns>
        public byte[] SignData(byte[] data)
        {
            if (privateKey == null)
            {
                throw new CryptoException("Private key is required for signing");
            }

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(privateKey, out _);
                return rsa.SignData(data, GetHashAlgorithmName(), GetRSASignaturePadding());
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <returns>签名（Base64编码）</returns>
        public string SignData(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] signature = SignData(bytes);
            return Convert.ToBase64String(signature);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="signature">签名</param>
        /// <returns>是否验证通过</returns>
        public bool VerifyData(byte[] data, byte[] signature)
        {
            if (publicKey == null)
            {
                throw new CryptoException("Public key is required for verification");
            }

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
                return rsa.VerifyData(data, signature, GetHashAlgorithmName(), GetRSASignaturePadding());
            }
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="signature">签名（Base64编码）</param>
        /// <returns>是否验证通过</returns>
        public bool VerifyData(string data, string signature)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signatureBytes = Convert.FromBase64String(signature);
            return VerifyData(dataBytes, signatureBytes);
        }

        /// <summary>
        /// 获取哈希算法名称
        /// </summary>
        /// <returns>哈希算法名称</returns>
        private HashAlgorithmName GetHashAlgorithmName()
        {
            switch (algorithm)
            {
                case SignAlgorithm.RSA_SHA1:
                case SignAlgorithm.ECDSA_SHA1:
                    return HashAlgorithmName.SHA1;
                case SignAlgorithm.RSA_SHA256:
                case SignAlgorithm.ECDSA_SHA256:
                    return HashAlgorithmName.SHA256;
                case SignAlgorithm.RSA_SHA384:
                case SignAlgorithm.ECDSA_SHA384:
                    return HashAlgorithmName.SHA384;
                case SignAlgorithm.RSA_SHA512:
                case SignAlgorithm.ECDSA_SHA512:
                    return HashAlgorithmName.SHA512;
                default:
                    throw new CryptoException("Unsupported sign algorithm: {0}", algorithm);
            }
        }

        /// <summary>
        /// 获取RSA签名填充模式
        /// </summary>
        /// <returns>RSA签名填充模式</returns>
        private RSASignaturePadding GetRSASignaturePadding()
        {
            return RSASignaturePadding.Pkcs1;
        }
    }
}
