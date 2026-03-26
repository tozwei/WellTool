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
    /// 消息摘要测试类
    /// </summary>
    public class DigestTest
    {
        /// <summary>
        /// 测试MD5消息摘要
        /// </summary>
        [Fact]
        public void TestMD5()
        {
            // 测试数据
            string testData = "Hello, MD5!";

            // 计算MD5消息摘要
            string md5 = CryptoUtil.MD5(testData);

            // 验证消息摘要长度
            Assert.Equal(32, md5.Length);
        }

        /// <summary>
        /// 测试SHA1消息摘要
        /// </summary>
        [Fact]
        public void TestSHA1()
        {
            // 测试数据
            string testData = "Hello, SHA1!";

            // 计算SHA1消息摘要
            string sha1 = CryptoUtil.SHA1(testData);

            // 验证消息摘要长度
            Assert.Equal(40, sha1.Length);
        }

        /// <summary>
        /// 测试SHA256消息摘要
        /// </summary>
        [Fact]
        public void TestSHA256()
        {
            // 测试数据
            string testData = "Hello, SHA256!";

            // 计算SHA256消息摘要
            string sha256 = CryptoUtil.SHA256(testData);

            // 验证消息摘要长度
            Assert.Equal(64, sha256.Length);
        }

        /// <summary>
        /// 测试消息摘要的一致性
        /// </summary>
        [Fact]
        public void TestDigestConsistency()
        {
            // 测试数据
            string testData = "Test data for digest consistency";

            // 计算多次消息摘要
            string md5_1 = CryptoUtil.MD5(testData);
            string md5_2 = CryptoUtil.MD5(testData);
            string sha1_1 = CryptoUtil.SHA1(testData);
            string sha1_2 = CryptoUtil.SHA1(testData);
            string sha256_1 = CryptoUtil.SHA256(testData);
            string sha256_2 = CryptoUtil.SHA256(testData);

            // 验证消息摘要的一致性
            Assert.Equal(md5_1, md5_2);
            Assert.Equal(sha1_1, sha1_2);
            Assert.Equal(sha256_1, sha256_2);
        }

        /// <summary>
        /// 测试空数据的消息摘要
        /// </summary>
        [Fact]
        public void TestDigestWithEmptyData()
        {
            // 测试空数据
            string testData = "";

            // 计算消息摘要
            string md5 = CryptoUtil.MD5(testData);
            string sha1 = CryptoUtil.SHA1(testData);
            string sha256 = CryptoUtil.SHA256(testData);

            // 验证消息摘要长度
            Assert.Equal(32, md5.Length);
            Assert.Equal(40, sha1.Length);
            Assert.Equal(64, sha256.Length);
        }
    }
}