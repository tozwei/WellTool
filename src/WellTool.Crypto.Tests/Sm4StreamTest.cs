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
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// Sm4StreamTest
    /// </summary>
    public class Sm4StreamTest
    {
        [Fact]
        public void Sm4EncryptDecryptTest()
        {
            // 创建 SM4 实例，使用 ECB 模式和 PKCS5Padding
            var key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 字节密钥
            var sm4 = new SM4(key);
            var data = "测试SM4加密解密";

            // 加密
            var encryptedHex = sm4.EncryptHex(data);
            Assert.NotNull(encryptedHex);
            Assert.NotEmpty(encryptedHex);

            // 解密
            var decrypted = sm4.DecryptStr(encryptedHex);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);
            Assert.Equal(data, decrypted);
        }

        [Fact]
        public void Sm4WithDifferentModeTest()
        {
            // 创建 SM4 实例，使用 CBC 模式和 PKCS5Padding
            var key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 字节密钥
            var iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16 字节 IV
            var sm4 = new SM4(CipherMode.CBC, Padding.PKCS5Padding, key, iv);
            var data = "测试SM4不同模式";

            // 加密
            var encryptedHex = sm4.EncryptHex(data);
            Assert.NotNull(encryptedHex);
            Assert.NotEmpty(encryptedHex);

            // 解密
            var decrypted = sm4.DecryptStr(encryptedHex);
            Assert.NotNull(decrypted);
            Assert.NotEmpty(decrypted);
            Assert.Equal(data, decrypted);
        }
    }
}
