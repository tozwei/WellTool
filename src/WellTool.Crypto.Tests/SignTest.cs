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
    /// 数字签名测试类
    /// </summary>
    public class SignTest
    {
        /// <summary>
        /// 测试数字签名和验证
        /// </summary>
        [Fact]
        public void TestSignAndVerify()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建数字签名实例
            var sign = new Sign(SignAlgorithm.RSA_SHA256, privateKey, publicKey);

            // 测试数据
            string testData = "Hello, Sign!";

            // 签名
            string signature = sign.SignData(testData);
            // 验证签名
            bool verified = sign.VerifyData(testData, signature);

            // 验证签名结果
            Assert.True(verified);
        }

        /// <summary>
        /// 测试数字签名验证失败的情况
        /// </summary>
        [Fact]
        public void TestSignVerifyFailed()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建数字签名实例
            var sign = new Sign(SignAlgorithm.RSA_SHA256, privateKey, publicKey);

            // 测试数据
            string testData = "Hello, Sign!";
            string modifiedData = "Hello, Modified Sign!";

            // 签名
            string signature = sign.SignData(testData);
            // 验证签名（使用修改后的数据）
            bool verified = sign.VerifyData(modifiedData, signature);

            // 验证签名结果
            Assert.False(verified);
        }

        /// <summary>
        /// 测试空数据的数字签名
        /// </summary>
        [Fact]
        public void TestSignWithEmptyData()
        {
            // 生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair();

            // 创建数字签名实例
            var sign = new Sign(SignAlgorithm.RSA_SHA256, privateKey, publicKey);

            // 测试空数据
            string testData = "";

            // 签名
            string signature = sign.SignData(testData);
            // 验证签名
            bool verified = sign.VerifyData(testData, signature);

            // 验证签名结果
            Assert.True(verified);
        }
    }
}