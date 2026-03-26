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

namespace WellTool.Core.Util
{
    /// <summary>
    /// 集合工具类
    /// </summary>
    public static class CollectionUtil
    {
        /// <summary>
        /// 检查集合是否为 null 或空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合</param>
        /// <returns>如果集合为 null 或空，则返回 true；否则返回 false</returns>
        public static bool IsEmpty<T>(IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// 检查集合是否不为 null 且不为空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合</param>
        /// <returns>如果集合不为 null 且不为空，则返回 true；否则返回 false</returns>
        public static bool IsNotEmpty<T>(IEnumerable<T> collection)
        {
            return !IsEmpty(collection);
        }

        /// <summary>
        /// 获取集合的大小
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要获取大小的集合</param>
        /// <returns>集合的大小</returns>
        public static int Size<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return 0;
            }

            if (collection is ICollection<T> col)
            {
                return col.Count;
            }

            return collection.Count();
        }

        /// <summary>
        /// 合并两个集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection1">第一个集合</param>
        /// <param name="collection2">第二个集合</param>
        /// <returns>合并后的集合</returns>
        public static IEnumerable<T> Merge<T>(IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            if (collection1 == null)
            {
                return collection2 ?? Enumerable.Empty<T>();
            }

            if (collection2 == null)
            {
                return collection1;
            }

            return collection1.Concat(collection2);
        }

        /// <summary>
        /// 将集合转换为列表
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <returns>转换后的列表</returns>
        public static List<T> ToList<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return new List<T>();
            }

            if (collection is List<T> list)
            {
                return list;
            }

            return collection.ToList();
        }

        /// <summary>
        /// 将集合转换为数组
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <returns>转换后的数组</returns>
        public static T[] ToArray<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return Array.Empty<T>();
            }

            if (collection is T[] array)
            {
                return array;
            }

            return collection.ToArray();
        }

        /// <summary>
        /// 将集合转换为字典
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="valueSelector">值选择器</param>
        /// <returns>转换后的字典</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(IEnumerable<TValue> collection, Func<TValue, TKey> keySelector)
        {
            if (collection == null)
            {
                return new Dictionary<TKey, TValue>();
            }

            return collection.ToDictionary(keySelector);
        }

        /// <summary>
        /// 将集合转换为字典
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="valueSelector">值选择器</param>
        /// <returns>转换后的字典</returns>
        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(IEnumerable<T> collection, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            if (collection == null)
            {
                return new Dictionary<TKey, TValue>();
            }

            return collection.ToDictionary(keySelector, valueSelector);
        }

        /// <summary>
        /// 过滤集合中的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要过滤的集合</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns>过滤后的集合</returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.Where(predicate);
        }

        /// <summary>
        /// 映射集合中的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="R">映射后的元素类型</typeparam>
        /// <param name="collection">要映射的集合</param>
        /// <param name="selector">映射函数</param>
        /// <returns>映射后的集合</returns>
        public static IEnumerable<R> Map<T, R>(IEnumerable<T> collection, Func<T, R> selector)
        {
            if (collection == null)
            {
                return Enumerable.Empty<R>();
            }

            return collection.Select(selector);
        }

        /// <summary>
        /// 查找集合中的第一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要查找的集合</param>
        /// <param name="predicate">查找条件</param>
        /// <returns>找到的元素，如果没有找到则返回默认值</returns>
        public static T FindFirst<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                return default;
            }

            return collection.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查找集合中的最后一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要查找的集合</param>
        /// <param name="predicate">查找条件</param>
        /// <returns>找到的元素，如果没有找到则返回默认值</returns>
        public static T FindLast<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                return default;
            }

            return collection.LastOrDefault(predicate);
        }

        /// <summary>
        /// 检查集合中是否包含指定的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合</param>
        /// <param name="item">要检查的元素</param>
        /// <returns>如果集合包含指定的元素，则返回 true；否则返回 false</returns>
        public static bool Contains<T>(IEnumerable<T> collection, T item)
        {
            if (collection == null)
            {
                return false;
            }

            return collection.Contains(item);
        }

        /// <summary>
        /// 检查集合中是否包含满足条件的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合</param>
        /// <param name="predicate">检查条件</param>
        /// <returns>如果集合包含满足条件的元素，则返回 true；否则返回 false</returns>
        public static bool Exists<T>(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                return false;
            }

            return collection.Any(predicate);
        }

        /// <summary>
        /// 对集合中的每个元素执行指定的操作
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要操作的集合</param>
        /// <param name="action">要执行的操作</param>
        public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null || action == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// 对集合中的每个元素执行指定的操作（带索引）
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要操作的集合</param>
        /// <param name="action">要执行的操作</param>
        public static void ForEach<T>(IEnumerable<T> collection, Action<T, int> action)
        {
            if (collection == null || action == null)
            {
                return;
            }

            int index = 0;
            foreach (var item in collection)
            {
                action(item, index);
                index++;
            }
        }

        /// <summary>
        /// 对集合中的元素进行排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要排序的集合</param>
        /// <returns>排序后的集合</returns>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.OrderBy(x => x);
        }

        /// <summary>
        /// 对集合中的元素进行排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="TKey">排序键类型</typeparam>
        /// <param name="collection">要排序的集合</param>
        /// <param name="keySelector">排序键选择器</param>
        /// <returns>排序后的集合</returns>
        public static IEnumerable<T> Sort<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.OrderBy(keySelector);
        }

        /// <summary>
        /// 对集合中的元素进行倒序排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要排序的集合</param>
        /// <returns>倒序排序后的集合</returns>
        public static IEnumerable<T> SortDescending<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.OrderByDescending(x => x);
        }

        /// <summary>
        /// 对集合中的元素进行倒序排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="TKey">排序键类型</typeparam>
        /// <param name="collection">要排序的集合</param>
        /// <param name="keySelector">排序键选择器</param>
        /// <returns>倒序排序后的集合</returns>
        public static IEnumerable<T> SortDescending<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.OrderByDescending(keySelector);
        }

        /// <summary>
        /// 去重集合中的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要去重的集合</param>
        /// <returns>去重后的集合</returns>
        public static IEnumerable<T> Distinct<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.Distinct();
        }

        /// <summary>
        /// 去重集合中的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="TKey">去重键类型</typeparam>
        /// <param name="collection">要去重的集合</param>
        /// <param name="keySelector">去重键选择器</param>
        /// <returns>去重后的集合</returns>
        public static IEnumerable<T> Distinct<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            return collection.GroupBy(keySelector).Select(g => g.First());
        }

        /// <summary>
        /// 计算集合中元素的平均值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要计算平均值的集合</param>
        /// <param name="selector">值选择器</param>
        /// <returns>集合中元素的平均值</returns>
        public static double Average<T>(IEnumerable<T> collection, Func<T, double> selector)
        {
            if (collection == null)
            {
                return 0;
            }

            return collection.Average(selector);
        }

        /// <summary>
        /// 计算集合中元素的最大值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要计算最大值的集合</param>
        /// <returns>集合中元素的最大值</returns>
        public static T Max<T>(IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection == null)
            {
                return default;
            }

            return collection.Max();
        }

        /// <summary>
        /// 计算集合中元素的最大值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="TKey">最大值键类型</typeparam>
        /// <param name="collection">要计算最大值的集合</param>
        /// <param name="keySelector">最大值键选择器</param>
        /// <returns>集合中元素的最大值</returns>
        public static T Max<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (collection == null)
            {
                return default;
            }

            return collection.OrderByDescending(keySelector).FirstOrDefault();
        }

        /// <summary>
        /// 计算集合中元素的最小值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要计算最小值的集合</param>
        /// <returns>集合中元素的最小值</returns>
        public static T Min<T>(IEnumerable<T> collection) where T : IComparable<T>
        {
            if (collection == null)
            {
                return default;
            }

            return collection.Min();
        }

        /// <summary>
        /// 计算集合中元素的最小值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="TKey">最小值键类型</typeparam>
        /// <param name="collection">要计算最小值的集合</param>
        /// <param name="keySelector">最小值键选择器</param>
        /// <returns>集合中元素的最小值</returns>
        public static T Min<T, TKey>(IEnumerable<T> collection, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if (collection == null)
            {
                return default;
            }

            return collection.OrderBy(keySelector).FirstOrDefault();
        }

        /// <summary>
        /// 将集合分割为指定大小的子集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要分割的集合</param>
        /// <param name="size">每个子集合的大小</param>
        /// <returns>分割后的子集合</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> collection, int size)
        {
            if (collection == null || size <= 0)
            {
                yield break;
            }

            var list = new List<T>(size);
            foreach (var item in collection)
            {
                list.Add(item);
                if (list.Count >= size)
                {
                    yield return list;
                    list = new List<T>(size);
                }
            }

            if (list.Count > 0)
            {
                yield return list;
            }
        }

        /// <summary>
        /// 将集合随机打乱
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要打乱的集合</param>
        /// <returns>打乱后的集合</returns>
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            var list = ToList(collection);
            var random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
            return list;
        }
    }
}
