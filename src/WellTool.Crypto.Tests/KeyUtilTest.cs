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
    /// KeyUtil密钥工具类测试
    /// </summary>
    public class KeyUtilTest
    {
        [Fact]
        public void GenerateKeyTest()
        {
            // 测试生成随机密钥
            byte[] key = KeyUtil.GenerateKey(128);
            Assert.NotNull(key);
            Assert.Equal(16, key.Length);
        }

        [Fact]
        public void GenerateSymmetricKeyTest()
        {
            // 测试生成对称密钥
            byte[] aesKey = KeyUtil.GenerateSymmetricKey(256);
            Assert.NotNull(aesKey);
            Assert.Equal(32, aesKey.Length);
        }

        [Fact]
        public void GenerateIVTest()
        {
            // 测试生成初始化向量
            byte[] iv = KeyUtil.GenerateIV(128);
            Assert.NotNull(iv);
            Assert.Equal(16, iv.Length);
        }

        [Fact]
        public void GenerateRsaKeyPairTest()
        {
            // 测试生成RSA密钥对
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair(2048);
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.True(publicKey.Length > 0);
            Assert.True(privateKey.Length > 0);
        }

        [Fact]
        public void GenerateAesKeyTest()
        {
            // 测试生成AES密钥
            byte[] key = KeyUtil.GenerateAesKey(256);
            Assert.NotNull(key);
            Assert.Equal(32, key.Length);
        }

        [Fact]
        public void GenerateDesKeyTest()
        {
            // 测试生成DES密钥
            byte[] key = KeyUtil.GenerateDesKey();
            Assert.NotNull(key);
            Assert.Equal(8, key.Length);
        }
    }
}
