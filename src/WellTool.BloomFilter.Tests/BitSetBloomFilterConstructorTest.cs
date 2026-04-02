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
using Xunit;

namespace WellTool.BloomFilter.Tests
{
    /// <summary>
    /// BitSetBloomFilter 构造函数测试
    /// </summary>
    public class BitSetBloomFilterConstructorTest
    {
    [Fact]
    public void TestConstructorWithInvalidParameters()
    {
        // 测试参数 size 的无效情况（size <= 0）
        Assert.Throws<ArgumentOutOfRangeException>(() => {
            new BitSetBloomFilter(0);
        });

        Assert.Throws<ArgumentOutOfRangeException>(() => {
            new BitSetBloomFilter(-5);
        });
    }

        [Fact]
        public void TestConstructorWithValidParameters()
        {
            // 测试有效参数
            var filter1 = new BitSetBloomFilter(100);
            Assert.NotNull(filter1);

            var filter2 = new BitSetBloomFilter(200);
            Assert.NotNull(filter2);
        }
    }
}
