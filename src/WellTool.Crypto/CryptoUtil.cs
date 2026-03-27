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
using WellTool.Crypto.Asymmetric;
using WellTool.Crypto.Digest;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public static class CryptoUtil
    {
        // 对称加密相关方法

        /// <summary>
        /// 创建AES加密实例
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>AES加密实例</returns>
        public static AES CreateAES(byte[] key, byte[]? iv = null)
        {
            return new AES(key, iv);
        }

        /// <summary>
        /// 创建DES加密实例
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>DES加密实例</returns>
        public static DES CreateDES(byte[] key, byte[]? iv = null)
        {
            return new DES(key, iv);
        }

        /// <summary>
        /// 创建3DES加密实例
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>3DES加密实例</returns>
        public static DESede CreateDESede(byte[] key, byte[]? iv = null)
        {
            return new DESede(key, iv);
        }

        // 非对称加密相关方法

        /// <summary>
        /// 创建RSA加密实例
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>RSA加密实例</returns>
        public static Asymmetric.RSA CreateRSA(byte[] publicKey, byte[]? privateKey = null)
        {
            return new Asymmetric.RSA(publicKey, privateKey);
        }

        // 消息摘要相关方法

        /// <summary>
        /// 创建消息摘要实例
        /// </summary>
        /// <param name="algorithm">摘要算法</param>
        /// <returns>消息摘要实例</returns>
        public static Digester CreateDigester(DigestAlgorithm algorithm)
        {
            return new GeneralDigester(algorithm);
        }

        /// <summary>
        /// 计算MD5消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string MD5(string data)
        {
            return Digest.MD5.DigestHex(data);
        }

        /// <summary>
        /// 计算SHA1消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string SHA1(string data)
        {
            return CreateDigester(DigestAlgorithm.SHA1).DigestHex(data);
        }

        /// <summary>
        /// 计算SHA256消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string SHA256(string data)
        {
            return CreateDigester(DigestAlgorithm.SHA256).DigestHex(data);
        }

        /// <summary>
        /// 计算SHA384消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string SHA384(string data)
        {
            return CreateDigester(DigestAlgorithm.SHA384).DigestHex(data);
        }

        /// <summary>
        /// 计算SHA512消息摘要
        /// </summary>
        /// <param name="data">待计算的数据</param>
        /// <returns>消息摘要（十六进制字符串）</returns>
        public static string SHA512(string data)
        {
            return CreateDigester(DigestAlgorithm.SHA512).DigestHex(data);
        }

        // 数字签名相关方法

        /// <summary>
        /// 创建数字签名实例
        /// </summary>
        /// <param name="algorithm">签名算法</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>数字签名实例</returns>
        public static Sign CreateSign(SignAlgorithm algorithm, byte[] privateKey, byte[] publicKey)
        {
            return new Sign(algorithm, privateKey, publicKey);
        }

        // 密钥生成相关方法

        /// <summary>
        /// 生成对称加密密钥
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>密钥</returns>
        public static byte[] GenerateSymmetricKey(SymmetricAlgorithm algorithm)
        {
            int keySize = algorithm switch
            {
                SymmetricAlgorithm.AES => 256,
                SymmetricAlgorithm.DES => 64,
                SymmetricAlgorithm.DESede => 192,
                _ => throw new CryptoException("Unsupported symmetric algorithm: {0}", algorithm)
            };
            return KeyUtil.GenerateSymmetricKey(keySize);
        }

        /// <summary>
        /// 生成对称加密初始化向量
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>初始化向量</returns>
        public static byte[] GenerateIV(SymmetricAlgorithm algorithm)
        {
            int blockSize = algorithm switch
            {
                SymmetricAlgorithm.AES => 128,
                SymmetricAlgorithm.DES => 64,
                SymmetricAlgorithm.DESede => 64,
                _ => throw new CryptoException("Unsupported symmetric algorithm: {0}", algorithm)
            };
            return KeyUtil.GenerateIV(blockSize);
        }

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (byte[] publicKey, byte[] privateKey) GenerateRsaKeyPair(int keySize = 2048)
        {
            return KeyUtil.GenerateRsaKeyPair(keySize);
        }
    }
}
