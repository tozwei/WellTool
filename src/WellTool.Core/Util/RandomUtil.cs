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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 随机工具类
    /// </summary>
    public static class RandomUtil
    {


        private static readonly Random Random = new Random();

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[Random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 生成指定范围内的随机整数
        /// </summary>
        /// <param name="min">最小值（包含）</param>
        /// <param name="max">最大值（包含）</param>
        /// <returns>随机整数</returns>
        public static int RandomInt(int min, int max)
        {
            return Random.Next(min, max + 1);
        }

        /// <summary>
        /// 生成指定范围内的随机长整数
        /// </summary>
        /// <param name="min">最小值（包含）</param>
        /// <param name="max">最大值（包含）</param>
        /// <returns>随机长整数</returns>
        public static long RandomLong(long min, long max)
        {
            // 生成0到(max-min)之间的随机数，然后加上min
            return min + (long)(Random.NextDouble() * (max - min + 1));
        }

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        /// <returns>随机布尔值</returns>
        public static bool RandomBoolean()
        {
            return Random.Next(2) == 0;
        }

        /// <summary>
        /// 生成随机浮点数
        /// </summary>
        /// <returns>随机浮点数</returns>
        public static float RandomFloat()
        {
            return (float)Random.NextDouble();
        }

        /// <summary>
        /// 生成随机双精度浮点数
        /// </summary>
        /// <returns>随机双精度浮点数</returns>
        public static double RandomDouble()
        {
            return Random.NextDouble();
        }

        /// <summary>
        /// 从集合中随机选择一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>随机选择的元素</returns>
        public static T RandomEle<T>(IEnumerable<T> collection)
        {
            if (collection == null || !collection.Any())
            {
                throw new ArgumentException("Collection cannot be null or empty");
            }

            var list = collection.ToList();
            return list[Random.Next(list.Count)];
        }

        /// <summary>
        /// 随机打乱集合中的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>打乱后的列表</returns>
        public static List<T> Shuffle<T>(List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var result = new List<T>(list);
            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = Random.Next(i + 1);
                (result[i], result[j]) = (result[j], result[i]);
            }
            return result;
        }
    }
}
