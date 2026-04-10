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

using System.Text;
using Xunit;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// Issue3925Test
    /// </summary>
    public class Issue3925Test
    {
        [Fact]
        public void TestRSAOAEPEncryption()
        {
            // 测试 RSA OAEP 加密和解密
            var rsa = new RSA();
            var (publicKey, privateKey) = rsa.GenerateKeyPair();

            var plaintext = "Hello, RSA OAEP!";
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            // 使用 OAEP 填充模式加密
            var encrypted = rsa.Encrypt(plaintextBytes, publicKey);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            // 使用 OAEP 填充模式解密
            var decrypted = rsa.Decrypt(encrypted, privateKey);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);

            var decryptedText = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plaintext, decryptedText);
        }

        [Fact]
        public void TestRSASignature()
        {
            // 测试 RSA 签名和验签
            var rsa = new RSA();
            var (publicKey, privateKey) = rsa.GenerateKeyPair();

            var data = "Hello, RSA Signature!";
            var dataBytes = Encoding.UTF8.GetBytes(data);

            // 签名
            var signature = rsa.Sign(dataBytes, privateKey);
            Assert.NotNull(signature);
            Assert.NotEmpty(signature);

            // 验签
            var verified = rsa.Verify(dataBytes, signature, publicKey);
            Assert.True(verified);
        }
    }
}

