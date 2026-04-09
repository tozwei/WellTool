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
using WellTool.Crypto.Digest;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// CryptoUtil测试
    /// </summary>
    public class CryptoUtilTest
    {
        [Fact]
        public void CreateAESTest()
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var iv = Encoding.UTF8.GetBytes("1234567890123456");
            var aes = CryptoUtil.CreateAES(key, iv);
            Assert.NotNull(aes);
        }

        [Fact]
        public void CreateDESTest()
        {
            var key = Encoding.UTF8.GetBytes("12345678");
            var iv = Encoding.UTF8.GetBytes("12345678");
            var des = CryptoUtil.CreateDES(key, iv);
            Assert.NotNull(des);
        }

        [Fact]
        public void CreateDESedeTest()
        {
            var key = Encoding.UTF8.GetBytes("123456789012345678901234");
            var iv = Encoding.UTF8.GetBytes("12345678");
            var desede = CryptoUtil.CreateDESede(key, iv);
            Assert.NotNull(desede);
        }

        [Fact]
        public void CreateRSATest()
        {
            var (publicKey, privateKey) = CryptoUtil.GenerateRsaKeyPair();
            var rsa = CryptoUtil.CreateRSA(publicKey, privateKey);
            Assert.NotNull(rsa);
        }

        [Fact]
        public void CreateDigesterTest()
        {
            var digester = CryptoUtil.CreateDigester(DigestAlgorithm.SHA256);
            Assert.NotNull(digester);
        }

        [Fact]
        public void MD5Test()
        {
            var data = "Hello, World!";
            var md5 = CryptoUtil.MD5(data);
            Assert.NotNull(md5);
            Assert.NotEmpty(md5);
        }

        [Fact]
        public void SHA1Test()
        {
            var data = "Hello, World!";
            var sha1 = CryptoUtil.SHA1(data);
            Assert.NotNull(sha1);
            Assert.NotEmpty(sha1);
        }

        [Fact]
        public void SHA256Test()
        {
            var data = "Hello, World!";
            var sha256 = CryptoUtil.SHA256(data);
            Assert.NotNull(sha256);
            Assert.NotEmpty(sha256);
        }

        [Fact]
        public void SHA384Test()
        {
            var data = "Hello, World!";
            var sha384 = CryptoUtil.SHA384(data);
            Assert.NotNull(sha384);
            Assert.NotEmpty(sha384);
        }

        [Fact]
        public void SHA512Test()
        {
            var data = "Hello, World!";
            var sha512 = CryptoUtil.SHA512(data);
            Assert.NotNull(sha512);
            Assert.NotEmpty(sha512);
        }

        [Fact]
        public void GenerateSymmetricKeyTest()
        {
            var key = CryptoUtil.GenerateSymmetricKey(SymmetricAlgorithmType.AES);
            Assert.NotNull(key);
            Assert.Equal(32, key.Length); // 256 bits = 32 bytes
        }

        [Fact]
        public void GenerateIVTest()
        {
            var iv = CryptoUtil.GenerateIV(SymmetricAlgorithmType.AES);
            Assert.NotNull(iv);
            Assert.Equal(16, iv.Length); // 128 bits = 16 bytes
        }

        [Fact]
        public void GenerateRsaKeyPairTest()
        {
            var (publicKey, privateKey) = CryptoUtil.GenerateRsaKeyPair();
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
            Assert.NotEmpty(publicKey);
            Assert.NotEmpty(privateKey);
        }
    }
}
