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
using System.Text;
using Xunit;
using WellTool.Crypto;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// OpensslKeyUtil测试
    /// </summary>
    public class OpensslKeyUtilTest
    {
        [Fact]
        public void ReadWritePrivateKeyTest()
        {
            // 生成 RSA 密钥对
            var keyPair = GenerateRsaKeyPair();

            // 写入私钥为 PEM 格式
            var privateKeyPem = OpensslKeyUtil.WritePrivateKey(keyPair);
            Assert.NotNull(privateKeyPem);
            Assert.NotEmpty(privateKeyPem);

            // 读取私钥
            var readKeyPair = OpensslKeyUtil.ReadPrivateKey(privateKeyPem);
            Assert.NotNull(readKeyPair);
            Assert.NotNull(readKeyPair.Private);
            Assert.NotNull(readKeyPair.Public);
        }

        [Fact]
        public void ReadWritePublicKeyTest()
        {
            // 生成 RSA 密钥对
            var keyPair = GenerateRsaKeyPair();

            // 写入公钥为 PEM 格式
            var publicKeyPem = OpensslKeyUtil.WritePublicKey(keyPair.Public);
            Assert.NotNull(publicKeyPem);
            Assert.NotEmpty(publicKeyPem);

            // 读取公钥
            var readPublicKey = OpensslKeyUtil.ReadPublicKey(publicKeyPem);
            Assert.NotNull(readPublicKey);
        }

        private AsymmetricCipherKeyPair GenerateRsaKeyPair()
        {
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            return generator.GenerateKeyPair();
        }
    }
}
