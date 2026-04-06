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
using WellTool.Crypto.Digest;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// BCrypt测试
    /// </summary>
    public class BCryptTest
    {
        [Fact]
        public void HashAndVerifyTest()
        {
            // 测试BCrypt哈希和验证
            string password = "password123";
            string hash = BCryptUtil.Hash(password);
            
            Assert.NotNull(hash);
            Assert.True(hash.StartsWith("$2"));
            
            bool isValid = BCryptUtil.Verify(password, hash);
            Assert.True(isValid);
            
            bool isInvalid = BCryptUtil.Verify("wrongpassword", hash);
            Assert.False(isInvalid);
        }
    }
}
