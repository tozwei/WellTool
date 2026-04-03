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

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// SM4国密算法测试
    /// </summary>
    public class SmTest
    {
        [Fact]
        public void Sm3Test()
        {
            // SM3摘要测试
            var sm3 = new WellTool.Crypto.Digest.SM3();
            string result = sm3.DigestHex("aaaaa");
            Assert.Equal("136ce3c86e4ed909b76082055a61586af20b4dab674732ebd4b599eef080c9be", result);
        }

        [Fact]
        public void Sm4Test()
        {
            string content = "test中文";
            var sm4 = new Crypto.Symmetric.SM4();
            
            string encryptHex = sm4.EncryptHex(content);
            string decryptStr = sm4.DecryptStr(encryptHex, Encoding.UTF8);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void Sm4WithKeyTest()
        {
            string content = "test中文";
            byte[] key = new byte[16];
            for (int i = 0; i < 16; i++) key[i] = (byte)i;
            
            var sm4 = new Crypto.Symmetric.SM4(key);
            
            string encryptHex = sm4.EncryptHex(content);
            string decryptStr = sm4.DecryptStr(encryptHex, Encoding.UTF8);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void Sm4CBCModeTest()
        {
            string content = "test中文";
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++) iv[i] = (byte)(15 - i);
            
            var sm4 = new Crypto.Symmetric.SM4(CipherMode.CBC, Padding.PKCS5Padding, 
                Encoding.UTF8.GetBytes("1234567890123456"), iv);
            
            string encryptHex = sm4.EncryptHex(content);
            string decryptStr = sm4.DecryptStr(encryptHex, Encoding.UTF8);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void Sm4ECBModeTest()
        {
            string content = "test中文";
            var sm4 = new Crypto.Symmetric.SM4(CipherMode.ECB, Padding.PKCS5Padding);
            
            string encryptHex = sm4.EncryptHex(content);
            string decryptStr = sm4.DecryptStr(encryptHex, Encoding.UTF8);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void Sm4LongContentTest()
        {
            string content = "test中文frfewrewrwerwer---------------------------------------------------";
            
            var sm4 = new Crypto.Symmetric.SM4();
            string encryptHex = sm4.EncryptHex(content);
            string decryptStr = sm4.DecryptStr(encryptHex, Encoding.UTF8);
            
            Assert.Equal(content, decryptStr);
        }

        [Fact]
        public void HmacSm3Test()
        {
            // HMAC-SM3测试
            string content = "test中文";
            // HMac 支持通过算法名称字符串创建
            var hmac = new Crypto.Digest.HMac("HMACSM3", Encoding.UTF8.GetBytes("password"));
            string digest = hmac.DigestHex(content);
            Assert.Equal("493e3f9a1896b43075fbe54658076727960d69632ac6b6ed932195857a6840c6", digest);
        }
    }
}
