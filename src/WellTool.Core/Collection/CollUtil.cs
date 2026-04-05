using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 集合相关工具类
    /// </summary>
    public class CollUtil
    {
        /// <summary>
        /// 如果提供的集合为null，返回一个不可变的默认空集合，否则返回原集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="set">提供的集合，可能为null</param>
        /// <returns>原集合，若为null返回空集合</returns>
        public static ISet<T> EmptyIfNull<T>(ISet<T> set)
        {
            return set ?? new HashSet<T>();
        }

        /// <summary>
        /// 如果提供的集合为null，返回一个不可变的默认空集合，否则返回原集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">提供的集合，可能为null</param>
        /// <returns>原集合，若为null返回空集合</returns>
        public static IList<T> EmptyIfNull<T>(IList<T> list)
        {
            return list ?? new List<T>();
        }

        /// <summary>
        /// 两个集合的并集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>并集的集合，返回 List</returns>
        public static ICollection<T> Union<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            if (IsEmpty(coll1) && IsEmpty(coll2))
            {
                return new List<T>();
            }
            if (IsEmpty(coll1))
            {
                return new List<T>(coll2);
            }
            if (IsEmpty(coll2))
            {
                return new List<T>(coll1);
            }

            var result = new List<T>(coll1);
            foreach (var item in coll2)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 两个集合的交集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>交集的集合，返回 List</returns>
        public static ICollection<T> Intersection<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            var result = new List<T>();
            if (IsEmpty(coll1) || IsEmpty(coll2))
            {
                return result;
            }

            foreach (var item in coll1)
            {
                if (coll2.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 两个集合的差集，即存在于coll1中但不存在于coll2中的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>差集的集合，返回 List</returns>
        public static ICollection<T> Subtract<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            var result = new List<T>();
            if (IsEmpty(coll1))
            {
                return result;
            }
            if (IsEmpty(coll2))
            {
                return new List<T>(coll1);
            }

            foreach (var item in coll1)
            {
                if (!coll2.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合，可能为null</param>
        /// <returns>如果集合为null或空，返回true</returns>
        public static bool IsEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 检查集合是否非空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合，可能为null</param>
        /// <returns>如果集合非null且非空，返回true</returns>
        public static bool IsNotEmpty<T>(ICollection<T> collection)
        {
            return !IsEmpty(collection);
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <param name="collection">要检查的集合，可能为null</param>
        /// <returns>如果集合为null或空，返回true</returns>
        public static bool IsEmpty(ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 检查集合是否非空
        /// </summary>
        /// <param name="collection">要检查的集合，可能为null</param>
        /// <returns>如果集合非null且非空，返回true</returns>
        public static bool IsNotEmpty(ICollection collection)
        {
            return !IsEmpty(collection);
        }

        /// <summary>
        /// 将集合转换为数组
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <returns>转换后的数组</returns>
        public static T[] ToArray<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return Array.Empty<T>();
            }

            var array = new T[collection.Count];
            collection.CopyTo(array, 0);
            return array;
        }

        /// <summary>
        /// 将集合转换为列表
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <returns>转换后的列表</returns>
        public static List<T> ToList<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }

            return new List<T>(collection);
        }

        /// <summary>
        /// 将集合转换为哈希集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要转换的集合</param>
        /// <returns>转换后的哈希集</returns>
        public static HashSet<T> ToHashSet<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return new HashSet<T>();
            }

            return new HashSet<T>(collection);
        }

        /// <summary>
        /// 创建新的LinkedHashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <returns>新的LinkedHashSet</returns>
        public static HashSet<T> NewLinkedHashSet<T>()
        {
            return new HashSet<T>();
        }

        /// <summary>
        /// 创建新的LinkedHashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要添加到集合中的元素</param>
        /// <returns>新的LinkedHashSet</returns>
        public static HashSet<T> NewLinkedHashSet<T>(IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        /// <summary>
        /// 创建新的LinkedList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <returns>新的LinkedList</returns>
        public static LinkedList<T> NewLinkedList<T>()
        {
            return new LinkedList<T>();
        }

        /// <summary>
        /// 创建新的LinkedList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要添加到集合中的元素</param>
        /// <returns>新的LinkedList</returns>
        public static LinkedList<T> NewLinkedList<T>(IEnumerable<T> collection)
        {
            return new LinkedList<T>(collection);
        }

        /// <summary>
        /// 添加所有元素到集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">目标集合</param>
        /// <param name="items">要添加的元素</param>
        public static void AddAll<T>(ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection != null && items != null)
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }
        }

        /// <summary>
        /// 截取集合的子集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="fromIndex">起始索引</param>
        /// <param name="toIndex">结束索引</param>
        /// <param name="step">步长</param>
        /// <returns>子集合</returns>
        public static ICollection<T> Sub<T>(ICollection<T> collection, int fromIndex, int toIndex, int step = 1)
        {
            var list = ToList(collection);
            if (fromIndex < 0) fromIndex = 0;
            if (toIndex > list.Count) toIndex = list.Count;
            if (fromIndex >= toIndex || step <= 0) return new List<T>();
            var result = new List<T>();
            for (int i = fromIndex; i < toIndex; i += step)
            {
                result.Add(list[i]);
            }
            return result;
        }

        /// <summary>
        /// 截取IList的子集合
        /// </summary>
        /// <param name="list">IList</param>
        /// <param name="fromIndex">起始索引</param>
        /// <param name="toIndex">结束索引</param>
        /// <param name="step">步长</param>
        /// <returns>子集合</returns>
        public static IList Sub(IList list, int fromIndex, int toIndex, int step = 1)
        {
            if (list == null)
            {
                return new List<object>();
            }
            if (fromIndex < 0) fromIndex = 0;
            if (toIndex > list.Count) toIndex = list.Count;
            if (fromIndex >= toIndex || step <= 0) return new List<object>();
            var result = new List<object>();
            for (int i = fromIndex; i < toIndex; i += step)
            {
                result.Add(list[i]);
            }
            return result;
        }

        /// <summary>
        /// 获取集合中的任意一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>任意一个元素，如果集合为空则返回默认值</returns>
        public static T GetAny<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return default;
            }
            foreach (var item in collection)
            {
                return item;
            }
            return default;
        }

        /// <summary>
        /// 获取IList中指定索引的元素
        /// </summary>
        /// <param name="list">IList</param>
        /// <param name="indices">索引数组</param>
        /// <returns>元素列表</returns>
        public static IList GetAny(IList list, int[] indices)
        {
            if (list == null || indices == null)
            {
                return new List<object>();
            }
            var result = new List<object>();
            foreach (var index in indices)
            {
                if (index >= 0 && index < list.Count)
                {
                    result.Add(list[index]);
                }
            }
            return result;
        }

        /// <summary>
        /// 反转集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">要反转的列表</param>
        /// <returns>反转后的列表</returns>
        public static List<T> Reverse<T>(IList<T> list)
        {
            if (IsEmpty(list))
            {
                return new List<T>();
            }

            var result = new List<T>(list);
            result.Reverse();
            return result;
        }

        /// <summary>
        /// 打乱集合顺序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">要打乱的列表</param>
        /// <returns>打乱后的列表</returns>
        public static List<T> Shuffle<T>(IList<T> list)
        {
            if (IsEmpty(list))
            {
                return new List<T>();
            }

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
        /// 获取集合的第一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>第一个元素，如果集合为空返回默认值</returns>
        public static T First<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return default;
            }

            return collection.First();
        }

        /// <summary>
        /// 获取集合的最后一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>最后一个元素，如果集合为空返回默认值</returns>
        public static T Last<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return default;
            }

            return collection.Last();
        }

        /// <summary>
        /// 从集合中随机获取一个元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>随机元素，如果集合为空返回默认值</returns>
        public static T Random<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return default;
            }

            var random = new Random();
            int index = random.Next(collection.Count);
            return collection.ElementAt(index);
        }

        /// <summary>
        /// 限制集合大小
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="limit">限制大小</param>
        /// <returns>限制后的集合</returns>
        public static ICollection<T> Limit<T>(ICollection<T> collection, int limit)
        {
            if (IsEmpty(collection) || limit <= 0)
            {
                return new List<T>();
            }

            return collection.Take(limit).ToList();
        }

        /// <summary>
        /// 跳过指定数量的元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="skip">跳过数量</param>
        /// <returns>跳过指定数量后的集合</returns>
        public static ICollection<T> Skip<T>(ICollection<T> collection, int skip)
        {
            if (IsEmpty(collection) || skip <= 0)
            {
                return new List<T>(collection);
            }

            return collection.Skip(skip).ToList();
        }

        /// <summary>
        /// 跳过指定数量的元素并限制大小
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="limit">限制大小</param>
        /// <returns>跳过指定数量并限制大小后的集合</returns>
        public static ICollection<T> SkipLimit<T>(ICollection<T> collection, int skip, int limit)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }

            return collection.Skip(skip).Take(limit).ToList();
        }

        /// <summary>
        /// 分割集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="size">每个子集合的大小</param>
        /// <returns>分割后的集合</returns>
        public static IList<ICollection<T>> Split<T>(ICollection<T> collection, int size)
        {
            var result = new List<ICollection<T>>();
            if (IsEmpty(collection) || size <= 0)
            {
                return result;
            }

            var list = ToList(collection);
            for (int i = 0; i < list.Count; i += size)
            {
                int end = System.Math.Min(i + size, list.Count);
                result.Add(list.GetRange(i, end - i));
            }
            return result;
        }

        /// <summary>
        /// 合并集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collections">要合并的集合</param>
        /// <returns>合并后的集合</returns>
        public static ICollection<T> Merge<T>(params ICollection<T>[] collections)
        {
            var result = new List<T>();
            if (collections == null || collections.Length == 0)
            {
                return result;
            }

            foreach (var collection in collections)
            {
                if (!IsEmpty(collection))
                {
                    result.AddRange(collection);
                }
            }
            return result;
        }

        /// <summary>
        /// 复制集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要复制的集合</param>
        /// <returns>复制后的集合</returns>
        public static ICollection<T> Copy<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }

            return new List<T>(collection);
        }

        /// <summary>
        /// 检查两个集合是否相等
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>如果两个集合相等，返回true</returns>
        public static bool Equals<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            if (ReferenceEquals(coll1, coll2))
            {
                return true;
            }
            if (coll1 == null || coll2 == null)
            {
                return false;
            }
            if (coll1.Count != coll2.Count)
            {
                return false;
            }

            foreach (var item in coll1)
            {
                if (!coll2.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查集合是否包含指定元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="item">要检查的元素</param>
        /// <returns>如果集合包含指定元素，返回true</returns>
        public static bool Contains<T>(ICollection<T> collection, T item)
        {
            return !IsEmpty(collection) && collection.Contains(item);
        }

        /// <summary>
        /// 检查集合是否包含指定的所有元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="items">要检查的元素</param>
        /// <returns>如果集合包含指定的所有元素，返回true</returns>
        public static bool ContainsAll<T>(ICollection<T> collection, params T[] items)
        {
            if (IsEmpty(collection) || items == null || items.Length == 0)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (!collection.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查集合是否包含指定的任何元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="items">要检查的元素</param>
        /// <returns>如果集合包含指定的任何元素，返回true</returns>
        public static bool ContainsAny<T>(ICollection<T> collection, params T[] items)
        {
            if (IsEmpty(collection) || items == null || items.Length == 0)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (collection.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 计算集合的哈希码
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>集合的哈希码</returns>
        public static int GetHashCode<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return 0;
            }

            int hashCode = 1;
            foreach (var item in collection)
            {
                hashCode = 31 * hashCode + (item == null ? 0 : item.GetHashCode());
            }
            return hashCode;
        }

        /// <summary>
        /// 将集合转换为字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>转换后的字符串</returns>
        public static string ToString<T>(ICollection<T> collection, string separator = ", ")
        {
            if (IsEmpty(collection))
            {
                return "";
            }

            return string.Join(separator, collection);
        }
    }
}
