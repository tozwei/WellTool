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
    /// 数组工具类
    /// </summary>
    public static class ArrayUtil
    {
        /// <summary>
        /// 检查数组是否为 null 或空
        /// </summary>
        /// <param name="array">要检查的数组</param>
        /// <returns>如果数组为 null 或空，则返回 true；否则返回 false</returns>
        public static bool IsEmpty(Array array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// 检查数组是否不为 null 且不为空
        /// </summary>
        /// <param name="array">要检查的数组</param>
        /// <returns>如果数组不为 null 且不为空，则返回 true；否则返回 false</returns>
        public static bool IsNotEmpty(Array array)
        {
            return !IsEmpty(array);
        }

        /// <summary>
        /// 检查数组是否为 null 或空
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">要检查的数组</param>
        /// <returns>如果数组为 null 或空，则返回 true；否则返回 false</returns>
        public static bool IsEmpty<T>(T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// 检查数组是否不为 null 且不为空
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">要检查的数组</param>
        /// <returns>如果数组不为 null 且不为空，则返回 true；否则返回 false</returns>
        public static bool IsNotEmpty<T>(T[] array)
        {
            return !IsEmpty(array);
        }

        /// <summary>
        /// 获取数组的长度
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>数组的长度</returns>
        public static int Length(Array array)
        {
            return array?.Length ?? 0;
        }

        /// <summary>
        /// 获取数组的长度
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>数组的长度</returns>
        public static int Length<T>(T[] array)
        {
            return array?.Length ?? 0;
        }

        /// <summary>
        /// 合并多个数组
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="arrays">要合并的数组</param>
        /// <returns>合并后的数组</returns>
        public static T[] Merge<T>(params T[][] arrays)
        {
            if (arrays == null || arrays.Length == 0)
            {
                return Array.Empty<T>();
            }

            int totalLength = arrays.Sum(a => a?.Length ?? 0);
            var result = new T[totalLength];
            int index = 0;

            foreach (var array in arrays)
            {
                if (array != null)
                {
                    Array.Copy(array, 0, result, index, array.Length);
                    index += array.Length;
                }
            }

            return result;
        }

        /// <summary>
        /// 数组转列表
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>列表</returns>
        public static List<T> ToList<T>(T[] array)
        {
            if (array == null)
            {
                return new List<T>();
            }

            return new List<T>(array);
        }

        /// <summary>
        /// 列表转数组
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>数组</returns>
        public static T[] ToArray<T>(List<T> list)
        {
            if (list == null)
            {
                return Array.Empty<T>();
            }

            return list.ToArray();
        }

        /// <summary>
        /// 从数组中获取指定索引的元素
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index">索引</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>指定索引的元素，如果索引越界则返回默认值</returns>
        public static T Get<T>(T[] array, int index, T defaultValue = default)
        {
            if (array == null || index < 0 || index >= array.Length)
            {
                return defaultValue;
            }

            return array[index];
        }

        /// <summary>
        /// 检查数组中是否包含指定的元素
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="element">元素</param>
        /// <returns>如果数组包含指定的元素，则返回 true；否则返回 false</returns>
        public static bool Contains<T>(T[] array, T element)
        {
            if (array == null)
            {
                return false;
            }

            return array.Contains(element);
        }

        /// <summary>
        /// 查找元素在数组中的索引
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="element">元素</param>
        /// <returns>元素在数组中的索引，如果不存在则返回 -1</returns>
        public static int IndexOf<T>(T[] array, T element)
        {
            if (array == null)
            {
                return -1;
            }

            return Array.IndexOf(array, element);
        }

        /// <summary>
        /// 反转数组
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>反转后的数组</returns>
        public static T[] Reverse<T>(T[] array)
        {
            if (array == null)
            {
                return null;
            }

            var result = (T[])array.Clone();
            Array.Reverse(result);
            return result;
        }

        /// <summary>
        /// 对数组进行排序
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>排序后的数组</returns>
        public static T[] Sort<T>(T[] array)
        {
            if (array == null)
            {
                return null;
            }

            var result = (T[])array.Clone();
            Array.Sort(result);
            return result;
        }

        /// <summary>
        /// 对数组进行排序
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="comparer">比较器</param>
        /// <returns>排序后的数组</returns>
        public static T[] Sort<T>(T[] array, IComparer<T> comparer)
        {
            if (array == null)
            {
                return null;
            }

            var result = (T[])array.Clone();
            Array.Sort(result, comparer);
            return result;
        }
    }
}
