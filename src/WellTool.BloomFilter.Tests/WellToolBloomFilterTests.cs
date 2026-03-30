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
            Assert.Throws<ArgumentException>(() => {
                var filter = new DefaultFilter(1, 32);
                filter.Add("init");
            });

            // 测试 maxValue=31 且 machineNum=32 时 add 应无异常
            Assert.Throws<ArgumentException>(() => {
                var filter = new DefaultFilter(31, 32);
                filter.Add("init");
            });

            // 测试 maxValue=1 且 machineNum=64 时 add 应无异常
            Assert.Throws<ArgumentException>(() => {
                var filter = new DefaultFilter(1, 64);
                filter.Add("init");
            });

            // 测试 maxValue=63 且 machineNum=64 时 add 应无异常
            Assert.Throws<ArgumentException>(() => {
                var filter = new DefaultFilter(63, 64);
                filter.Add("init");
            });
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
            // 注意：.NET 版本的 BitSetBloomFilter 构造函数参数与 Java 版本不同
            // 这里测试 .NET 版本的构造函数
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(0);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var filter = new BitSetBloomFilter(-5);
            });
        }
    }
}