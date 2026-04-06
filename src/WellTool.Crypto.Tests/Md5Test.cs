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
using WellTool.Crypto;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// MD5摘要测试
    /// </summary>
    public class Md5Test
    {
        [Fact]
        public void DigestTest()
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes("test");
            byte[] result = Crypto.Digest.MD5.Digest(data);
            Assert.NotNull(result);
            Assert.Equal(16, result.Length);
        }

        [Fact]
        public void DigestHexTest()
        {
            string result = Crypto.Digest.MD5.DigestHex("test");
            Assert.NotNull(result);
            Assert.Equal(32, result.Length);
        }
    }
}
