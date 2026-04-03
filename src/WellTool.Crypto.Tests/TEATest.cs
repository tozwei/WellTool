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
using System.Security.Cryptography;
using Xunit;
using WellTool.Crypto.Symmetric;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// TEA加密测试
    /// </summary>
    public class TEATest
    {
        [Fact]
        public void EncryptDecryptTest()
        {
            byte[] key = new byte[16];
            RandomNumberGenerator.Fill(key);
            
            var tea = new TEA(key);
            string content = "test中文";
            
            string encryptHex = tea.EncryptHex(content);
            string decryptStr = tea.DecryptStr(encryptHex);
            
            Assert.Equal(content, decryptStr);
        }
    }
}
