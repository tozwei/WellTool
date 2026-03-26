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
    /// BitMap 接口，用于将某个值映射到一个数组中，从而判定某个值是否存在
    /// </summary>
    public interface BitMap
    {
        /// <summary>
        /// 32位机器
        /// </summary>
        const int MACHINE32 = 32;

        /// <summary>
        /// 64位机器
        /// </summary>
        const int MACHINE64 = 64;

        /// <summary>
        /// 加入值
        /// </summary>
        /// <param name="i">值</param>
        void Add(long i);

        /// <summary>
        /// 检查是否包含值
        /// </summary>
        /// <param name="i">值</param>
        /// <returns>是否包含</returns>
        bool Contains(long i);

        /// <summary>
        /// 移除值
        /// </summary>
        /// <param name="i">值</param>
        void Remove(long i);
    }
}