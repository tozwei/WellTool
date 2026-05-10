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
using WellTool.BloomFilter;
using WellTool.BloomFilter.BitMap;
using WellTool.BloomFilter.Filter;
using Xunit;

namespace WellTool.BloomFilter.Tests
{
    public class WellToolBloomFilterTests
    {
        [Fact]
        public void TestInitWhenMaxValueLessThanMachineNum()
        {
            // 测试 maxValue=1 且 machineNum=32 时 add 应无异常
            var filter1 = new DefaultFilter(1, 32);
            var result1 = filter1.Add("init");
            Assert.True(result1);
            Assert.True(filter1.Contains("init"));

            // 测试 maxValue=31 且 machineNum=32 时 add 应无异常
            var filter2 = new DefaultFilter(31, 32);
            var result2 = filter2.Add("init");
            Assert.True(result2);
            Assert.True(filter2.Contains("init"));

            // 测试 maxValue=1 且 machineNum=64 时 add 应无异常
            var filter3 = new DefaultFilter(1, 64);
            var result3 = filter3.Add("init");
            Assert.True(result3);
            Assert.True(filter3.Contains("init"));

            // 测试 maxValue=63 且 machineNum=64 时 add 应无异常
            var filter4 = new DefaultFilter(63, 64);
            var result4 = filter4.Add("init");
            Assert.True(result4);
            Assert.True(filter4.Contains("init"));
        }

        [Fact]
        public void TestBitMapBloomFilter()
        {
            var filter = new BitMapBloomFilter(10);
            filter.Add("123");
            filter.Add("abc");
            filter.Add("ddd");

            Assert.True(filter.Contains("abc"));
            Assert.True(filter.Contains("ddd"));
            Assert.True(filter.Contains("123"));
        }

        [Fact]
        public void TestIntMap()
        {
            var intMap = new IntMap(1); // 容量为1，可存储32位

            for (int i = 0; i < 32; i++)
            {
                intMap.Add(i);
            }
            intMap.Remove(30);

            for (int i = 0; i < 32; i++)
            {
                Console.WriteLine($"{i}是否存在-->{intMap.Contains(i)}");
            }
        }

        [Fact]
        public void TestLongMap()
        {
            var longMap = new LongMap(1); // 容量为1，可存储64位

            for (int i = 0; i < 64; i++)
            {
                longMap.Add(i);
            }
            longMap.Remove(30);

            for (int i = 0; i < 64; i++)
            {
                Console.WriteLine($"{i}是否存在-->{longMap.Contains(i)}");
            }
        }

        [Fact]
        public void TestBitSetBloomFilterConstructorWithInvalidParameters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(0, 10, 3);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(-5, 10, 3);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(10, 0, 3);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(10, -5, 3);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(10, 10, 0);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(10, 10, 9);
            });
        }
    }
}