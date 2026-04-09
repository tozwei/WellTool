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
            var keyPair = rsa.GenerateKeyPair();

            // 序列化密钥对
            var publicKeyBytes = rsa.ExportPublicKey(keyPair.Public);
            var privateKeyBytes = rsa.ExportPrivateKey(keyPair.Private);

            Assert.NotNull(publicKeyBytes);
            Assert.NotEmpty(publicKeyBytes);
            Assert.NotNull(privateKeyBytes);
            Assert.NotEmpty(privateKeyBytes);

            // 反序列化密钥对
            var publicKey = rsa.ImportPublicKey(publicKeyBytes);
            var privateKey = rsa.ImportPrivateKey(privateKeyBytes);

            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
        }

        [Fact]
        public void TestRSAEncryptionWithSerializedKeys()
        {
            // 测试使用序列化后的密钥进行加密和解密
            var rsa = new RSA();
            var keyPair = rsa.GenerateKeyPair();

            // 序列化密钥对
            var publicKeyBytes = rsa.ExportPublicKey(keyPair.Public);
            var privateKeyBytes = rsa.ExportPrivateKey(keyPair.Private);

            // 反序列化密钥对
            var publicKey = rsa.ImportPublicKey(publicKeyBytes);
            var privateKey = rsa.ImportPrivateKey(privateKeyBytes);

            var plaintext = "Hello, RSA with serialized keys!";
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            var encrypted = rsa.Encrypt(plaintextBytes, publicKey);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            var decrypted = rsa.Decrypt(encrypted, privateKey);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);

            var decryptedText = Encoding.UTF8.GetString(decrypted);
            Assert.Equal(plaintext, decryptedText);
        }
    }
}

