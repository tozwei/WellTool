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
using Xunit;
using WellTool.Crypto;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// 对称加密测试
    /// </summary>
    public class SymmetricTest
    {
        [Fact]
        public void AESEncryptDecryptTest()
        {
            // 测试AES加密解密
            byte[] key = new byte[32];
            new Random().NextBytes(key);
            
            var aes = new AES(key);
            string content = "test中文";
            
            string encryptHex = aes.EncryptHex(content);
            string decryptStr = aes.DecryptStr(encryptHex);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void DESedeEncryptDecryptTest()
        {
            // 测试3DES加密解密
            byte[] key = new byte[24];
            new Random().NextBytes(key);
            
            var desede = new DESede(key);
            string content = "test中文";
            
            string encryptHex = desede.EncryptHex(content);
            string decryptStr = desede.DecryptStr(encryptHex);
            
            Assert.Equal(content, decryptStr);
        }
    }
}
