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
using System.Text;
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
        [Fact]
        public void GenerateRsaKeyPairTest()
        {
            // 测试RSA密钥对生成
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair(2048);
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.True(publicKey.Length > 0);
            Assert.True(privateKey.Length > 0);
        }

        [Fact]
        public void RsaEncryptDecryptTest()
        {
            // 测试RSA加密解密
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair(2048);
            var rsa = new RSA(publicKey, privateKey);
            
            string plainText = "Hello, RSA encryption!";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            
            // 加密
            byte[] encrypted = rsa.Encrypt(plainBytes);
            Assert.NotNull(encrypted);
            Assert.True(encrypted.Length > 0);
            
            // 解密
            byte[] decrypted = rsa.Decrypt(encrypted);
            string result = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plainText, result);
        }

        [Fact]
        public void SmallKeySizeTest()
        {
            // 测试使用较小的密钥大小
            var (publicKey, privateKey) = KeyUtil.GenerateRsaKeyPair(512);
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
        }
    }
}