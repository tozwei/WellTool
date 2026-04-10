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
    /// Issue3512Test
    /// </summary>
    public class Issue3512Test
    {
        [Fact]
        public void TestRSAKeyGeneration()
        {
            // 测试 RSA 密钥生成
            var rsa = new RSA();
            var (publicKey, privateKey) = rsa.GenerateKeyPair();

            Assert.NotNull(publicKey);
            Assert.NotEmpty(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(privateKey);
        }

        [Fact]
        public void TestRSAEncryptionWithDifferentKeySizes()
        {
            // 测试不同密钥大小的 RSA 加密
            var keySizes = new int[] { 1024, 2048, 4096 };

            foreach (var keySize in keySizes)
            {
                var (publicKey, privateKey) = RSA.GenerateKeyPair(keySize);
                var rsa = new RSA(publicKey, privateKey);

                var plaintext = "Hello, RSA with key size " + keySize + "!";
                var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

                var encrypted = rsa.Encrypt(plaintextBytes);
                Assert.NotNull(encrypted);
                Assert.NotEmpty(encrypted);

                var decrypted = rsa.Decrypt(encrypted);
                Assert.NotNull(decrypted);
                Assert.NotEmpty(decrypted);

                var decryptedText = Encoding.UTF8.GetString(decrypted);
                Assert.Equal(plaintext, decryptedText);
            }
        }
    }
}

