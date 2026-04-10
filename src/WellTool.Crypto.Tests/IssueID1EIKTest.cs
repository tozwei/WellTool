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
    /// IssueID1EIKTest
    /// </summary>
    public class IssueID1EIKTest
    {
        [Fact]
        public void TestRSAKeyPairSerialization()
        {
            // 测试 RSA 密钥对序列化和反序列化
            var rsa = new RSA();
            var (publicKey, privateKey) = rsa.GenerateKeyPair();

            // 验证密钥对
            Assert.NotNull(publicKey);
            Assert.NotEmpty(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(privateKey);

            // 测试导入密钥
            var rsa2 = new RSA(publicKey, privateKey);
            var exportedPublicKey = rsa2.ExportPublicKey();
            var exportedPrivateKey = rsa2.ExportPrivateKey();

            Assert.NotNull(exportedPublicKey);
            Assert.NotEmpty(exportedPublicKey);
            Assert.NotNull(exportedPrivateKey);
            Assert.NotEmpty(exportedPrivateKey);
        }

        [Fact]
        public void TestRSAEncryptionWithSerializedKeys()
        {
            // 测试使用序列化后的密钥进行加密和解密
            var rsa = new RSA();
            var (publicKey, privateKey) = rsa.GenerateKeyPair();

            var plaintext = "Hello, RSA with serialized keys!";
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            // 使用公钥加密
            var encrypted = rsa.Encrypt(plaintextBytes, publicKey);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            // 使用私钥解密
            var decrypted = rsa.Decrypt(encrypted, privateKey);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);

            var decryptedText = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plaintext, decryptedText);
        }
    }
}

