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
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// Argon2密码哈希测试
    /// </summary>
    public class Argon2Test
    {
        [Fact]
        public void HashAndVerifyTest()
        {
            // 测试Argon2哈希和验证
            string password = "password123";
            string hash = Argon2.Hash(password);
            
            Assert.NotNull(hash);
            Assert.True(hash.StartsWith("$argon2"));
            
            bool isValid = Argon2.Verify(password, hash);
            Assert.True(isValid);
        }
    }
}
