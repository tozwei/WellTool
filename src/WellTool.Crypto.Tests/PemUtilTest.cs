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
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Asymmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// PemUtil测试
    /// </summary>
    public class PemUtilTest
    {
        [Fact]
        public void ReadWritePemTest()
        {
            // 生成 RSA 密钥对
            var (publicKey, privateKey) = CryptoUtil.GenerateRsaKeyPair();
            var rsa = new Asymmetric.RSA(publicKey, privateKey);
            var keyPair = rsa.GetKeyPair();

            // 写入私钥为 PEM 字符串
            var privateKeyPem = PemUtil.WritePem(keyPair.Private);
            Assert.NotNull(privateKeyPem);
            Assert.NotEmpty(privateKeyPem);

            // 读取私钥
            var readPrivateKey = PemUtil.ReadPem(privateKeyPem);
            Assert.NotNull(readPrivateKey);

            // 写入公钥为 PEM 字符串
            var publicKeyPem = PemUtil.WritePem(keyPair.Public);
            Assert.NotNull(publicKeyPem);
            Assert.NotEmpty(publicKeyPem);

            // 读取公钥
            var readPublicKey = PemUtil.ReadPem(publicKeyPem);
            Assert.NotNull(readPublicKey);
        }

        [Fact]
        public void ReadWritePemFileTest()
        {
            // 生成 RSA 密钥对
            var (publicKey, privateKey) = CryptoUtil.GenerateRsaKeyPair();
            var rsa = new Asymmetric.RSA(publicKey, privateKey);
            var keyPair = rsa.GetKeyPair();

            // 创建临时文件
            var privateKeyPath = Path.GetTempFileName();
            var publicKeyPath = Path.GetTempFileName();

            try
            {
                // 写入私钥为 PEM 文件
                PemUtil.WritePemFile(keyPair.Private, privateKeyPath);
                Assert.True(File.Exists(privateKeyPath));

                // 读取私钥
                var readPrivateKey = PemUtil.ReadPemFile(privateKeyPath);
                Assert.NotNull(readPrivateKey);

                // 写入公钥为 PEM 文件
                PemUtil.WritePemFile(keyPair.Public, publicKeyPath);
                Assert.True(File.Exists(publicKeyPath));

                // 读取公钥
                var readPublicKey = PemUtil.ReadPemFile(publicKeyPath);
                Assert.NotNull(readPublicKey);
            }
            finally
            {
                // 清理临时文件
                if (File.Exists(privateKeyPath))
                {
                    File.Delete(privateKeyPath);
                }
                if (File.Exists(publicKeyPath))
                {
                    File.Delete(publicKeyPath);
                }
            }
        }
    }
}
