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
    /// HMAC测试
    /// </summary>
    public class HmacTest
    {
        [Fact]
        public void HmacMD5Test()
        {
            var hmac = new HMac("HMACMD5", Encoding.UTF8.GetBytes("key"));
            string result = hmac.DigestHex("test");
            Assert.NotNull(result);
            Assert.Equal(32, result.Length);
        }

        [Fact]
        public void HmacSHA256Test()
        {
            var hmac = new HMac("HMACSHA256", Encoding.UTF8.GetBytes("key"));
            string result = hmac.DigestHex("test");
            Assert.NotNull(result);
            Assert.Equal(64, result.Length);
        }

        [Fact]
        public void HmacSHA1Test()
        {
            var hmac = new HMac("HMACSHA1", Encoding.UTF8.GetBytes("key"));
            string result = hmac.DigestHex("test");
            Assert.NotNull(result);
            Assert.Equal(40, result.Length);
        }
    }
}
