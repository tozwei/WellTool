using System;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto
{
    /// <summary>
    /// 密钥工具类
    /// </summary>
    public static class KeyUtil
    {
        /// <summary>
        /// 生成随机密钥
        /// </summary>
        /// <param name="keySize">密钥大小（位）</param>
        /// <returns>随机密钥</returns>
        public static byte[] GenerateKey(int keySize)
        {
            var key = new byte[keySize / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(key);
            return key;
        }

        /// <summary>
        /// 生成对称密钥
        /// </summary>
        /// <param name="keySize">密钥大小（位）</param>
        /// <returns>对称密钥</returns>
        public static byte[] GenerateSymmetricKey(int keySize)
        {
            return GenerateKey(keySize);
        }

        /// <summary>
        /// 生成对称密钥
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>对称密钥</returns>
        public static byte[] GenerateSymmetricKey(WellTool.Crypto.Symmetric.SymmetricAlgorithm algorithm)
        {
            int keySize = algorithm switch
            {
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.AES => 256,
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.DES => 64,
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.DESede => 192,
                _ => throw new CryptoException("Unsupported symmetric algorithm: {0}", algorithm)
            };
            return GenerateSymmetricKey(keySize);
        }

        /// <summary>
        /// 生成初始化向量
        /// </summary>
        /// <param name="blockSize">块大小（位）</param>
        /// <returns>初始化向量</returns>
        public static byte[] GenerateIV(int blockSize)
        {
            var iv = new byte[blockSize / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(iv);
            return iv;
        }

        /// <summary>
        /// 生成初始化向量
        /// </summary>
        /// <param name="algorithm">对称加密算法</param>
        /// <returns>初始化向量</returns>
        public static byte[] GenerateIV(WellTool.Crypto.Symmetric.SymmetricAlgorithm algorithm)
        {
            int blockSize = algorithm switch
            {
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.AES => 128,
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.DES => 64,
                WellTool.Crypto.Symmetric.SymmetricAlgorithm.DESede => 64,
                _ => throw new CryptoException("Unsupported symmetric algorithm: {0}", algorithm)
            };
            return GenerateIV(blockSize);
        }

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小（位）</param>
        /// <returns>RSA密钥对</returns>
        public static (byte[] publicKey, byte[] privateKey) GenerateRsaKeyPair(int keySize = 2048)
        {
            using var rsa = RSA.Create(keySize);
            var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
            var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
            return (publicKeyBytes, privateKeyBytes);
        }

        /// <summary>
        /// 生成AES密钥
        /// </summary>
        /// <param name="keySize">密钥大小（位）</param>
        /// <returns>AES密钥</returns>
        public static byte[] GenerateAesKey(int keySize = 256)
        {
            return GenerateKey(keySize);
        }

        /// <summary>
        /// 生成DES密钥
        /// </summary>
        /// <returns>DES密钥</returns>
        public static byte[] GenerateDesKey()
        {
            return GenerateKey(64);
        }
    }
}