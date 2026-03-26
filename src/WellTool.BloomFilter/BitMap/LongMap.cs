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

namespace WellTool.BloomFilter.BitMap
{
    /// <summary>
    /// 64位位图实现
    /// </summary>
    public class LongMap : BitMap
    {
        private readonly long[] bits;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="size">容量</param>
        public LongMap(int size)
        {
            bits = new long[size];
        }

        /// <summary>
        /// 加入值
        /// </summary>
        /// <param name="i">值</param>
        public void Add(long i)
        {
            int index = (int)(i / BitMap.MACHINE64);
            int position = (int)(i % BitMap.MACHINE64);
            bits[index] |= 1L << position;
        }

        /// <summary>
        /// 检查是否包含值
        /// </summary>
        /// <param name="i">值</param>
        /// <returns>是否包含</returns>
        public bool Contains(long i)
        {
            int index = (int)(i / BitMap.MACHINE64);
            int position = (int)(i % BitMap.MACHINE64);
            return (bits[index] & (1L << position)) != 0;
        }

        /// <summary>
        /// 移除值
        /// </summary>
        /// <param name="i">值</param>
        public void Remove(long i)
        {
            int index = (int)(i / BitMap.MACHINE64);
            int position = (int)(i % BitMap.MACHINE64);
            bits[index] &= ~(1L << position);
        }
    }
}