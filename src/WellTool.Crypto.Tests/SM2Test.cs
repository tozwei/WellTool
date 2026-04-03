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
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// SM2加密测试
    /// </summary>
    public class SM2Test
    {
        [Fact]
        public void CreateSM2Test()
        {
            // 测试SM2实例创建
            var sm2 = SmUtil.CreateSM2();
            Assert.NotNull(sm2);
        }

        [Fact]
        public void CreateSM2WithKeysTest()
        {
            // 测试使用指定密钥创建SM2
            byte[] privateKey = new byte[32];
            byte[] publicKey = new byte[64];
            RandomNumberGenerator.Fill(privateKey);
            RandomNumberGenerator.Fill(publicKey);
            
            var sm2 = SmUtil.CreateSM2(privateKey, publicKey);
            Assert.NotNull(sm2);
        }
    }
}
