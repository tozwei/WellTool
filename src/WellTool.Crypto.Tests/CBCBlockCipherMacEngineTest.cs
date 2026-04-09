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
using WellTool.Crypto.Digest.Mac;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// CBCBlockCipherMacEngineTest
    /// </summary>
    public class CBCBlockCipherMacEngineTest
    {
        [Fact]
        public void HmacMd5Test()
        {
            var key = Encoding.UTF8.GetBytes("testKey");
            var data = Encoding.UTF8.GetBytes("test中文");
            var result = Mac.HmacMd5(key, data);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void HmacSha1Test()
        {
            var key = Encoding.UTF8.GetBytes("testKey");
            var data = Encoding.UTF8.GetBytes("test中文");
            var result = Mac.HmacSha1(key, data);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void HmacSha256Test()
        {
            var key = Encoding.UTF8.GetBytes("testKey");
            var data = Encoding.UTF8.GetBytes("test中文");
            var result = Mac.HmacSha256(key, data);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void HmacSha384Test()
        {
            var key = Encoding.UTF8.GetBytes("testKey");
            var data = Encoding.UTF8.GetBytes("test中文");
            var result = Mac.HmacSha384(key, data);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void HmacSha512Test()
        {
            var key = Encoding.UTF8.GetBytes("testKey");
            var data = Encoding.UTF8.GetBytes("test中文");
            var result = Mac.HmacSha512(key, data);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
