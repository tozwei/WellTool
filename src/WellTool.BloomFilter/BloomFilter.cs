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
    /// Bloom filter 是由 Howard Bloom 在 1970 年提出的二进制向量数据结构，它具有很好的空间和时间效率，被用来检测一个元素是不是集合中的一个成员。<br>
    /// 如果检测结果为是，该元素不一定在集合中；但如果检测结果为否，该元素一定不在集合中。<br>
    /// 因此Bloom filter具有100%的召回率。这样每个检测请求返回有“在集合内（可能错误）”和“不在集合内（绝对不在集合内）”两种情况。<br>
    /// </summary>
    public interface BloomFilter
    {
        /// <summary>
        /// 判断一个字符串是否在 Bloom Filter 中存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>判断结果，存在返回 true，不存在返回 false</returns>
        bool Contains(string str);

        /// <summary>
        /// 在 Bloom Filter 中增加一个字符串<br>
        /// 如果存在就返回 false. 如果不存在，先增加这个字符串，再返回 true
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否加入成功，如果存在就返回 false. 如果不存在返回 true</returns>
        bool Add(string str);
    }
}