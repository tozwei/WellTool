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
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// PBKDF2加密测试
    /// </summary>
    public class PBKDF2Test
    {
        [Fact]
        public void DeriveKeyTest()
        {
            // 测试PBKDF2密钥派生
            var pbkdf2 = new PBKDF2();
            byte[] password = Encoding.UTF8.GetBytes("password");
            byte[] salt = Encoding.UTF8.GetBytes("salt");
            
            byte[] derivedKey = pbkdf2.DeriveKey(password, salt, 1000, 32);
            Assert.NotNull(derivedKey);
            Assert.Equal(32, derivedKey.Length);
        }
    }
}
