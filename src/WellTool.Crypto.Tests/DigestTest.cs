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
    /// 消息摘要测试类
    /// </summary>
    public class DigestTest
    {
        [Fact]
        public void MD5Test()
        {
            var data = "Hello, MD5!";
            var md5 = CryptoUtil.MD5(data);
            Assert.NotNull(md5);
            Assert.NotEmpty(md5);
            Assert.Equal(32, md5.Length); // MD5 摘要长度为 32 个十六进制字符
        }

        [Fact]
        public void SHA1Test()
        {
            var data = "Hello, SHA1!";
            var sha1 = CryptoUtil.SHA1(data);
            Assert.NotNull(sha1);
            Assert.NotEmpty(sha1);
            Assert.Equal(40, sha1.Length); // SHA1 摘要长度为 40 个十六进制字符
        }

        [Fact]
        public void SHA256Test()
        {
            var data = "Hello, SHA256!";
            var sha256 = CryptoUtil.SHA256(data);
            Assert.NotNull(sha256);
            Assert.NotEmpty(sha256);
            Assert.Equal(64, sha256.Length); // SHA256 摘要长度为 64 个十六进制字符
        }

        [Fact]
        public void SHA384Test()
        {
            var data = "Hello, SHA384!";
            var sha384 = CryptoUtil.SHA384(data);
            Assert.NotNull(sha384);
            Assert.NotEmpty(sha384);
            Assert.Equal(96, sha384.Length); // SHA384 摘要长度为 96 个十六进制字符
        }

        [Fact]
        public void SHA512Test()
        {
            var data = "Hello, SHA512!";
            var sha512 = CryptoUtil.SHA512(data);
            Assert.NotNull(sha512);
            Assert.NotEmpty(sha512);
            Assert.Equal(128, sha512.Length); // SHA512 摘要长度为 128 个十六进制字符
        }

        [Fact]
        public void SM3Test()
        {
            var sm3 = new SM3();
            var data = "Hello, SM3!";
            var digest = sm3.DigestHex(data);
            Assert.NotNull(digest);
            Assert.NotEmpty(digest);
            Assert.Equal(64, digest.Length); // SM3 摘要长度为 64 个十六进制字符
        }
    }
}