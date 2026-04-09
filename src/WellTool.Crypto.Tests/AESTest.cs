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
    /// AES加密测试类
    /// </summary>
    public class AESTest
    {
        [Fact]
        public void TestAES_CBC()
        {
            // 测试 CBC 模式加密
            byte[] key = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);
            
            string plaintext = "123456";
            byte[] encrypted = aes.Encrypt(System.Text.Encoding.UTF8.GetBytes(plaintext));
            byte[] decrypted = aes.Decrypt(encrypted);
            string decryptedText = System.Text.Encoding.UTF8.GetString(decrypted);
            
            Assert.Equal(plaintext, decryptedText);
        }
        
        [Fact]
        public void TestAES_WithoutIV()
        {
            // 测试没有 IV 的情况
            byte[] key = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key);
            
            string plaintext = "123456";
            byte[] encrypted = aes.Encrypt(System.Text.Encoding.UTF8.GetBytes(plaintext));
            byte[] decrypted = aes.Decrypt(encrypted);
            string decryptedText = System.Text.Encoding.UTF8.GetString(decrypted);
            
            Assert.Equal(plaintext, decryptedText);
        }
        
        [Fact]
        public void TestAES_Chinese()
        {
            // 测试中文加密
            byte[] key = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = System.Text.Encoding.UTF8.GetBytes("1234567890123456");
            var aes = new AES(key, iv);
            
            string plaintext = "测试中文";
            byte[] encrypted = aes.Encrypt(System.Text.Encoding.UTF8.GetBytes(plaintext));
            byte[] decrypted = aes.Decrypt(encrypted);
            string decryptedText = System.Text.Encoding.UTF8.GetString(decrypted);
            
            Assert.Equal(plaintext, decryptedText);
        }
    }
}