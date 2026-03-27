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
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Asymmetric;
using WellTool.Crypto.Digest;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// 加密测试类
    /// </summary>
    public class CryptoTest
    {
        // 测试数据
        private const string TestData = "Hello, World!";

        // 对称加密测试

        /// <summary>
        /// 测试AES加密
        /// </summary>
        [Fact]
        public void TestAES()
        {
            // 生成密钥和初始化向量
            byte[] key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            byte[] iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.AES);

            // 创建AES加密实例
            var aes = new AES(key, iv);

            // 加密
            string encrypted = aes.Encrypt(TestData);
            // 解密
            string decrypted = aes.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(TestData, decrypted);
        }

        /// <summary>
        /// 测试DES加密
        /// </summary>
        [Fact]
        public void TestDES()
        {
            // 生成密钥和初始化向量
            byte[] key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.DES);
            byte[] iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.DES);

            // 创建DES加密实例
            var des = new DES(key, iv);

            // 加密
            string encrypted = des.Encrypt(TestData);
            // 解密
            string decrypted = des.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(TestData, decrypted);
        }

        /// <summary>
        /// 测试3DES加密
        /// </summary>
        [Fact]
        public void TestDESede()
        {
            // 生成密钥和初始化向量
            byte[] key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.DESede);
            byte[] iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.DESede);

            // 创建3DES加密实例
            var desEde = new DESede(key, iv);

            // 加密
            string encrypted = desEde.Encrypt(TestData);
            // 解密
            string decrypted = desEde.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(TestData, decrypted);
        }

        // 非对称加密测试

        /// <summary>
        /// 测试RSA加密
        /// </summary>
        [Fact]
        public void TestRSA()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建RSA加密实例
            var rsa = new Asymmetric.RSA(publicKey, privateKey);

            // 加密
            string encrypted = rsa.Encrypt(TestData);
            // 解密
            string decrypted = rsa.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(TestData, decrypted);
        }

        // 消息摘要测试

        /// <summary>
        /// 测试MD5消息摘要
        /// </summary>
        [Fact]
        public void TestMD5()
        {
            // 计算MD5消息摘要
            string md5 = CryptoUtil.MD5(TestData);
            // 验证消息摘要长度
            Assert.Equal(32, md5.Length);
        }

        /// <summary>
        /// 测试SHA1消息摘要
        /// </summary>
        [Fact]
        public void TestSHA1()
        {
            // 计算SHA1消息摘要
            string sha1 = CryptoUtil.SHA1(TestData);
            // 验证消息摘要长度
            Assert.Equal(40, sha1.Length);
        }

        /// <summary>
        /// 测试SHA256消息摘要
        /// </summary>
        [Fact]
        public void TestSHA256()
        {
            // 计算SHA256消息摘要
            string sha256 = CryptoUtil.SHA256(TestData);
            // 验证消息摘要长度
            Assert.Equal(64, sha256.Length);
        }

        // 数字签名测试

        /// <summary>
        /// 测试数字签名
        /// </summary>
        [Fact]
        public void TestSign()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建数字签名实例
            var sign = new Sign(SignAlgorithm.RSA_SHA256, privateKey, publicKey);

            // 签名
            string signature = sign.SignData(TestData);
            // 验证签名
            bool verified = sign.VerifyData(TestData, signature);

            // 验证签名结果
            Assert.True(verified);
        }

        // CryptoUtil工具类测试

        /// <summary>
        /// 测试CryptoUtil工具类
        /// </summary>
        [Fact]
        public void TestCryptoUtil()
        {
            // 测试消息摘要
            string md5 = CryptoUtil.MD5(TestData);
            string sha1 = CryptoUtil.SHA1(TestData);
            string sha256 = CryptoUtil.SHA256(TestData);

            // 验证消息摘要长度
            Assert.Equal(32, md5.Length);
            Assert.Equal(40, sha1.Length);
            Assert.Equal(64, sha256.Length);

            // 测试密钥生成
            byte[] aesKey = CryptoUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            byte[] desKey = CryptoUtil.GenerateSymmetricKey(SymmetricAlgorithmType.DES);
            var (rsaPublicKey, rsaPrivateKey) = CryptoUtil.GenerateRsaKeyPair();

            // 验证密钥长度
            Assert.NotNull(aesKey);
            Assert.NotNull(desKey);
            Assert.NotNull(rsaPublicKey);
            Assert.NotNull(rsaPrivateKey);
        }
    }
}
