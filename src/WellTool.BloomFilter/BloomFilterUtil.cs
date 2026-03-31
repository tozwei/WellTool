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

namespace WellTool.BloomFilter
{
    /// <summary>
    /// Bloom Filter 工具类
    /// </summary>
    public static class BloomFilterUtil
    {


        /// <summary>
        /// 创建 BitMapBloomFilter
        /// </summary>
        /// <param name="size">大小</param>
        /// <returns>BitMapBloomFilter</returns>
        public static BitMapBloomFilter CreateBitMapBloomFilter(int size)
        {
            return new BitMapBloomFilter(size);
        }

        /// <summary>
        /// 创建 BitSetBloomFilter
        /// </summary>
        /// <param name="size">大小</param>
        /// <returns>BitSetBloomFilter</returns>
        public static BitSetBloomFilter CreateBitSetBloomFilter(int size)
        {
            return new BitSetBloomFilter(size);
        }
    }
}