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

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// RSA加密测试类
    /// </summary>
    public class RSATest
    {
        /// <summary>
        /// 测试RSA密钥对生成
        /// </summary>
        [Fact]
        public void TestGenerateKeyPair()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 验证密钥对
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }

        /// <summary>
        /// 测试RSA加密和解密
        /// </summary>
        [Fact]
        public void TestRSAEncryptAndDecrypt()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建RSA加密实例
            var rsa = new Asymmetric.RSA(publicKey, privateKey);

            // 测试数据
            string testData = "Hello, RSA!";

            // 加密
            string encrypted = rsa.Encrypt(testData);
            // 解密
            string decrypted = rsa.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(testData, decrypted);
        }

        /// <summary>
        /// 测试RSA加密空数据
        /// </summary>
        [Fact]
        public void TestRSAWithEmptyData()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建RSA加密实例
            var rsa = new Asymmetric.RSA(publicKey, privateKey);

            // 测试空数据
            string testData = "";

            // 加密
            string encrypted = rsa.Encrypt(testData);
            // 解密
            string decrypted = rsa.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(testData, decrypted);
        }
    }
}