using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 列表工具类
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// 创建一个空列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <returns>空列表</returns>
        public static List<T> Empty<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// 创建一个包含指定元素的列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="elements">元素数组</param>
        /// <returns>包含指定元素的列表</returns>
        public static List<T> Of<T>(params T[] elements)
        {
            return new List<T>(elements);
        }

        /// <summary>
        /// 从迭代器创建列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="enumerable">可枚举对象</param>
        /// <returns>列表</returns>
        public static List<T> Of<T>(IEnumerable<T> enumerable)
        {
            return new List<T>(enumerable);
        }

        /// <summary>
        /// 反转列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>反转后的列表</returns>
        public static List<T> Reverse<T>(List<T> list)
        {
            var result = new List<T>(list);
            result.Reverse();
            return result;
        }

        /// <summary>
        /// 截取列表的一部分
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="count">截取数量</param>
        /// <returns>截取后的列表</returns>
        public static List<T> Sub<T>(List<T> list, int startIndex, int count)
        {
            return list.GetRange(startIndex, count);
        }

        /// <summary>
        /// 截取列表从指定索引到末尾
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="startIndex">开始索引</param>
        /// <returns>截取后的列表</returns>
        public static List<T> Sub<T>(List<T> list, int startIndex)
        {
            return list.GetRange(startIndex, list.Count - startIndex);
        }

        /// <summary>
        /// 合并多个列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="lists">列表数组</param>
        /// <returns>合并后的列表</returns>
        public static List<T> Merge<T>(params List<T>[] lists)
        {
            var result = new List<T>();
            foreach (var list in lists)
            {
                result.AddRange(list);
            }
            return result;
        }

        /// <summary>
        /// 去重列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>去重后的列表</returns>
        public static List<T> Distinct<T>(List<T> list)
        {
            return list.Distinct().ToList();
        }

        /// <summary>
        /// 随机打乱列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>打乱后的列表</returns>
        public static List<T> Shuffle<T>(List<T> list)
        {
            var result = new List<T>(list);
            var random = new Random();
            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (result[i], result[j]) = (result[j], result[i]);
            }
            return result;
        }

        /// <summary>
        /// 计算列表的交集
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list1">第一个列表</param>
        /// <param name="list2">第二个列表</param>
        /// <returns>交集列表</returns>
        public static List<T> Intersection<T>(List<T> list1, List<T> list2)
        {
            return list1.Intersect(list2).ToList();
        }

        /// <summary>
        /// 计算列表的并集
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list1">第一个列表</param>
        /// <param name="list2">第二个列表</param>
        /// <returns>并集列表</returns>
        public static List<T> Union<T>(List<T> list1, List<T> list2)
        {
            return list1.Union(list2).ToList();
        }

        /// <summary>
        /// 计算列表的差集（list1 - list2）
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list1">第一个列表</param>
        /// <param name="list2">第二个列表</param>
        /// <returns>差集列表</returns>
        public static List<T> Difference<T>(List<T> list1, List<T> list2)
        {
            return list1.Except(list2).ToList();
        }

        /// <summary>
        /// 检查列表是否为空
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// 检查列表是否不为空
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotEmpty<T>(List<T> list)
        {
            return !IsEmpty(list);
        }

        /// <summary>
        /// 获取列表的第一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>第一个元素，列表为空则返回默认值</returns>
        public static T GetFirst<T>(List<T> list)
        {
            return IsNotEmpty(list) ? list[0] : default;
        }

        /// <summary>
        /// 获取列表的最后一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>最后一个元素，列表为空则返回默认值</returns>
        public static T GetLast<T>(List<T> list)
        {
            return IsNotEmpty(list) ? list[list.Count - 1] : default;
        }

        /// <summary>
        /// 安全获取列表元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index">索引</param>
        /// <returns>元素，索引越界则返回默认值</returns>
        public static T Get<T>(List<T> list, int index)
        {
            return list != null && index >= 0 && index < list.Count ? list[index] : default;
        }

        /// <summary>
        /// 安全获取列表元素，带默认值
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="index">索引</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>元素，索引越界则返回默认值</returns>
        public static T Get<T>(List<T> list, int index, T defaultValue)
        {
            return list != null && index >= 0 && index < list.Count ? list[index] : defaultValue;
        }
    }
}