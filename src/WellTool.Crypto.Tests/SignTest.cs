// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

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
    /// SignUtil签名工具测试
    /// </summary>
    public class SignTest
    {
        [Fact]
        public void CreateSignTest()
        {
            // 测试Sign静态Create方法
            var sign = Sign.Create(AsymmetricAlgorithm.RSA);
            Assert.NotNull(sign);
        }

        [Fact]
        public void SignAndVerifyTest()
        {
            // 测试签名和验签
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair(2048);
            var sign = new Sign(SignAlgorithm.RSA_SHA256, privateKey, publicKey);
            
            byte[] data = System.Text.Encoding.UTF8.GetBytes("test data");
            byte[] signature = sign.Sign(data);
            Assert.NotNull(signature);
            
            bool isValid = sign.Verify(data, signature);
            Assert.True(isValid);
        }
    }
}