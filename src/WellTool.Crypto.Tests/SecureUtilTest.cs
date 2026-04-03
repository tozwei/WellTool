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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// SecureUtil工具类测试
    /// </summary>
    public class SecureUtilTest
    {
        private const string TestContent = "test中文";
        private const string TestData = "test data";

        [Fact]
        public void AesTest()
        {
            // 测试AES加密解密
            var aes = new Crypto.Symmetric.AES();
            Assert.NotNull(aes);

            // 使用密钥创建AES
            byte[] key = new byte[32];
            RandomNumberGenerator.Fill(key);
            var aesWithKey = new Crypto.Symmetric.AES(key);
            Assert.NotNull(aesWithKey);

            // 测试加密解密
            string encrypted = aesWithKey.EncryptHex(TestContent);
            string decrypted = aesWithKey.DecryptStr(encrypted, Encoding.UTF8);
            Assert.Equal(TestContent, decrypted);
        }

        [Fact]
        public void DesTest()
        {
            // 测试DES加密解密
            var des = new Crypto.Symmetric.DES();
            Assert.NotNull(des);

            byte[] key = new byte[8];
            RandomNumberGenerator.Fill(key);
            var desWithKey = new Crypto.Symmetric.DES(key);
            Assert.NotNull(desWithKey);

            // 测试加密解密
            string encrypted = desWithKey.EncryptHex(TestContent);
            string decrypted = desWithKey.DecryptStr(encrypted, Encoding.UTF8);
            Assert.Equal(TestContent, decrypted);
        }

        [Fact]
        public void Md5Test()
        {
            // 测试MD5对象
            var md5 = new Crypto.Digest.MD5();
            Assert.NotNull(md5);

            // 测试字符串MD5
            string md5Str = md5.DigestHex(TestData);
            Assert.NotNull(md5Str);
            Assert.Equal(32, md5Str.Length);

            // 测试字节数组MD5
            string md5Bytes = md5.DigestHex(Encoding.UTF8.GetBytes(TestData));
            Assert.Equal(md5Str, md5Bytes);
        }

        [Fact]
        public void Sha1Test()
        {
            // 测试SHA1对象
            var sha1 = new Crypto.Digest.SHA1();
            Assert.NotNull(sha1);

            // 测试字符串SHA1
            string sha1Str = sha1.DigestHex(TestData);
            Assert.NotNull(sha1Str);
            Assert.Equal(40, sha1Str.Length);
        }

        [Fact]
        public void Sha256Test()
        {
            // 测试SHA256对象
            var sha256 = new Crypto.Digest.SHA256();
            Assert.NotNull(sha256);

            // 测试字符串SHA256
            string sha256Str = sha256.DigestHex(TestData);
            Assert.NotNull(sha256Str);
            Assert.Equal(64, sha256Str.Length);
        }

        [Fact]
        public void HmacTest()
        {
            // 测试HMac对象生成
            byte[] key = new byte[16];
            RandomNumberGenerator.Fill(key);
            
            var hmac = new Crypto.Digest.HMac("HMACSHA256", key);
            Assert.NotNull(hmac);

            // 测试字符串密钥
            var hmac2 = new Crypto.Digest.HMac("HMACMD5", Encoding.UTF8.GetBytes("testkey"));
            Assert.NotNull(hmac2);

            // 验证加密结果
            string result = hmac2.DigestHex(TestData);
            Assert.NotNull(result);
            Assert.Equal(32, result.Length);
        }

        [Fact]
        public void GenerateKeyPairTest()
        {
            // 测试RSA密钥对生成
            var (publicKey, privateKey) = Crypto.KeyUtil.GenerateRsaKeyPair(2048);
            Assert.NotNull(publicKey);
            Assert.NotNull(privateKey);
        }

        [Fact]
        public void GenerateSymmetricKeyTest()
        {
            // 测试对称密钥生成
            byte[] aesKey = Crypto.KeyUtil.GenerateAesKey(256);
            Assert.NotNull(aesKey);
            Assert.Equal(32, aesKey.Length);

            byte[] desKey = Crypto.KeyUtil.GenerateDesKey();
            Assert.NotNull(desKey);
            Assert.Equal(8, desKey.Length);
        }

        [Fact]
        public void DigestTest()
        {
            // 测试消息摘要
            var digester = new Crypto.Digest.Digester(Crypto.Digest.DigestAlgorithm.MD5);
            Assert.NotNull(digester);

            string result = digester.DigestHex(TestData);
            Assert.NotNull(result);
            Assert.Equal(32, result.Length);
        }
    }
}
