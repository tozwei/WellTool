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
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// AES加密测试类
    /// </summary>
    public class AESTest
    {
        /// <summary>
        /// 测试AES加密和解密
        /// </summary>
        [Fact]
        public void TestAESEncryptAndDecrypt()
        {
            // 生成密钥和初始化向量
            byte[] key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            byte[] iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.AES);

            // 创建AES加密实例
            var aes = new AES(key, iv);

            // 测试数据
            string testData = "Hello, AES!";

            // 加密
            string encrypted = aes.Encrypt(testData);
            // 解密
            string decrypted = aes.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(testData, decrypted);
        }

        /// <summary>
        /// 测试AES加密使用固定密钥
        /// </summary>
        [Fact]
        public void TestAESWithFixedKey()
        {
            // 使用固定密钥和IV
            byte[] key = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = System.Text.Encoding.UTF8.GetBytes("1234567890123456");

            // 创建AES加密实例
            var aes = new AES(key, iv);

            // 测试数据
            string testData = "123456";

            // 加密
            string encrypted = aes.Encrypt(testData);
            // 解密
            string decrypted = aes.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(testData, decrypted);
        }

        /// <summary>
        /// 测试AES加密空数据
        /// </summary>
        [Fact]
        public void TestAESWithEmptyData()
        {
            // 生成密钥和初始化向量
            byte[] key = KeyUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            byte[] iv = KeyUtil.GenerateIV(SymmetricAlgorithmType.AES);

            // 创建AES加密实例
            var aes = new AES(key, iv);

            // 测试空数据
            string testData = "";

            // 加密
            string encrypted = aes.Encrypt(testData);
            // 解密
            string decrypted = aes.Decrypt(encrypted);

            // 验证解密结果
            Assert.Equal(testData, decrypted);
        }
    }
}