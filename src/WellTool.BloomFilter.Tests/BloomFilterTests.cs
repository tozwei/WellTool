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

using WellTool.BloomFilter;

namespace WellTool.BloomFilter.Tests
{
    public class BloomFilterTests
    {
        [Fact]
        public void TestBitMapBloomFilter()
        {
            // 创建 BitMapBloomFilter
            var bloomFilter = new BitMapBloomFilter(10);

            // 测试添加和包含
            Assert.True(bloomFilter.Add("test1"));
            Assert.True(bloomFilter.Add("test2"));
            Assert.True(bloomFilter.Add("test3"));

            // 测试已存在的元素
            Assert.False(bloomFilter.Add("test1"));

            // 测试包含
            Assert.True(bloomFilter.Contains("test1"));
            Assert.True(bloomFilter.Contains("test2"));
            Assert.True(bloomFilter.Contains("test3"));

            // 测试不存在的元素
            Assert.False(bloomFilter.Contains("test4"));
        }

        [Fact]
        public void TestBitSetBloomFilter()
        {
            // 创建 BitSetBloomFilter
            var bloomFilter = new BitSetBloomFilter(1000);

            // 测试添加和包含
            Assert.True(bloomFilter.Add("test1"));
            Assert.True(bloomFilter.Add("test2"));
            Assert.True(bloomFilter.Add("test3"));

            // 测试已存在的元素
            Assert.False(bloomFilter.Add("test1"));

            // 测试包含
            Assert.True(bloomFilter.Contains("test1"));
            Assert.True(bloomFilter.Contains("test2"));
            Assert.True(bloomFilter.Contains("test3"));

            // 测试不存在的元素
            Assert.False(bloomFilter.Contains("test4"));
        }

        [Fact]
        public void TestBloomFilterUtil()
        {
            // 测试创建 BitMapBloomFilter
            var bitMapBloomFilter = BloomFilterUtil.CreateBitMapBloomFilter(10);
            Assert.NotNull(bitMapBloomFilter);

            // 测试创建 BitSetBloomFilter
            var bitSetBloomFilter = BloomFilterUtil.CreateBitSetBloomFilter(1000);
            Assert.NotNull(bitSetBloomFilter);
        }
    }
}