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

            var list = new List<T>(Math.Max(coll1.Count, coll2.Count));
            var map1 = CountMap(coll1);
            var map2 = CountMap(coll2);
            var elts = NewHashSet(coll2);
            elts.UnionWith(coll1);

            foreach (var t in elts)
            {
                int m = Math.Max(map1.GetValueOrDefault(t, 0), map2.GetValueOrDefault(t, 0));
                for (int i = 0; i < m; i++)
                {
                    list.Add(t);
                }
            }
            return list;
        }

        /// <summary>
        /// 多个集合的并集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="otherColls">其它集合</param>
        /// <returns>并集的集合，返回 List</returns>
        public static ICollection<T> Union<T>(ICollection<T> coll1, ICollection<T> coll2, params ICollection<T>[] otherColls)
        {
            var union = Union(coll1, coll2);
            if (otherColls != null)
            {
                foreach (var coll in otherColls)
                {
                    if (!IsEmpty(coll))
                    {
                        union = Union(union, coll);
                    }
                }
            }
            return union;
        }

        /// <summary>
        /// 多个集合的非重复并集，类似于SQL中的"UNION DISTINCT"
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="otherColls">其它集合</param>
        /// <returns>并集的集合，返回 LinkedHashSet</returns>
        public static ISet<T> UnionDistinct<T>(ICollection<T> coll1, ICollection<T> coll2, params ICollection<T>[] otherColls)
        {
            var result = new HashSet<T>();
            if (coll1 != null)
            {
                result.UnionWith(coll1);
            }
            if (coll2 != null)
            {
                result.UnionWith(coll2);
            }
            if (otherColls != null)
            {
                foreach (var otherColl in otherColls)
                {
                    if (otherColl != null)
                    {
                        result.UnionWith(otherColl);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 多个集合的完全并集，类似于SQL中的"UNION ALL"
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="otherColls">其它集合</param>
        /// <returns>并集的集合，返回 List</returns>
        public static List<T> UnionAll<T>(ICollection<T> coll1, ICollection<T> coll2, params ICollection<T>[] otherColls)
        {
            if (IsEmpty(coll1) && IsEmpty(coll2) && (otherColls == null || otherColls.Length == 0))
            {
                return new List<T>();
            }

            // 计算元素总数
            int totalSize = 0;
            totalSize += Size(coll1);
            totalSize += Size(coll2);
            if (otherColls != null)
            {
                foreach (var otherColl in otherColls)
                {
                    totalSize += Size(otherColl);
                }
            }

            // 根据size创建，防止多次扩容
            var res = new List<T>(totalSize);
            if (coll1 != null)
            {
                res.AddRange(coll1);
            }
            if (coll2 != null)
            {
                res.AddRange(coll2);
            }
            if (otherColls != null)
            {
                foreach (var otherColl in otherColls)
                {
                    if (otherColl != null)
                    {
                        res.AddRange(otherColl);
                    }
                }
            }
            return res;
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
            if (IsNotEmpty(coll1) && IsNotEmpty(coll2))
            {
                var list = new List<T>(Math.Min(coll1.Count, coll2.Count));
                var map1 = CountMap(coll1);
                var map2 = CountMap(coll2);
                var elts = NewHashSet(coll2);

                foreach (var t in elts)
                {
                    int m = Math.Min(map1.GetValueOrDefault(t, 0), map2.GetValueOrDefault(t, 0));
                    for (int i = 0; i < m; i++)
                    {
                        list.Add(t);
                    }
                }
                return list;
            }
            return new List<T>();
        }

        /// <summary>
        /// 多个集合的交集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="otherColls">其它集合</param>
        /// <returns>交集的集合，返回 List</returns>
        public static ICollection<T> Intersection<T>(ICollection<T> coll1, ICollection<T> coll2, params ICollection<T>[] otherColls)
        {
            var intersection = Intersection(coll1, coll2);
            if (IsEmpty(intersection))
            {
                return intersection;
            }
            if (otherColls != null)
            {
                foreach (var coll in otherColls)
                {
                    intersection = Intersection(intersection, coll);
                    if (IsEmpty(intersection))
                    {
                        return intersection;
                    }
                }
            }
            return intersection;
        }

        /// <summary>
        /// 多个集合的交集
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="otherColls">其它集合</param>
        /// <returns>交集的集合，返回 LinkedHashSet</returns>
        public static ISet<T> IntersectionDistinct<T>(ICollection<T> coll1, ICollection<T> coll2, params ICollection<T>[] otherColls)
        {
            if (IsEmpty(coll1) || IsEmpty(coll2))
            {
                return new HashSet<T>();
            }

            var result = new HashSet<T>(coll1);
            result.IntersectWith(coll2);

            if (otherColls != null)
            {
                foreach (var otherColl in otherColls)
                {
                    if (IsNotEmpty(otherColl))
                    {
                        result.IntersectWith(otherColl);
                    }
                    else
                    {
                        return new HashSet<T>();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 两个集合的对称差集 (A-B)∪(B-A)
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>差集的集合，返回 List</returns>
        public static ICollection<T> Disjunction<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            if (IsEmpty(coll1))
            {
                return coll2;
            }
            if (IsEmpty(coll2))
            {
                return coll1;
            }

            var result = new List<T>();
            var map1 = CountMap(coll1);
            var map2 = CountMap(coll2);
            var elts = NewHashSet(coll2);
            elts.UnionWith(coll1);

            foreach (var t in elts)
            {
                int m = Math.Abs(map1.GetValueOrDefault(t, 0) - map2.GetValueOrDefault(t, 0));
                for (int i = 0; i < m; i++)
                {
                    result.Add(t);
                }
            }
            return result;
        }

        /// <summary>
        /// 计算集合的单差集，即只返回【集合1】中有，但是【集合2】中没有的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>单差集</returns>
        public static ICollection<T> Subtract<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            if (IsEmpty(coll1) || IsEmpty(coll2))
            {
                return coll1;
            }

            var result = new List<T>(coll1);
            result.RemoveAll(item => coll2.Contains(item));
            return result;
        }

        /// <summary>
        /// 计算集合的单差集，即只返回【集合1】中有，但是【集合2】中没有的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>单差集</returns>
        public static List<T> SubtractToList<T>(ICollection<T> coll1, ICollection<T> coll2)
        {
            return SubtractToList(coll1, coll2, true);
        }

        /// <summary>
        /// 计算集合的单差集，即只返回【集合1】中有，但是【集合2】中没有的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <param name="isLinked">返回的集合类型是否是LinkedList</param>
        /// <returns>单差集结果</returns>
        public static List<T> SubtractToList<T>(ICollection<T> coll1, ICollection<T> coll2, bool isLinked)
        {
            if (IsEmpty(coll1))
            {
                return new List<T>();
            }

            if (IsEmpty(coll2))
            {
                return new List<T>(coll1);
            }

            var result = new List<T>(coll1.Count);
            var set = new HashSet<T>(coll2);
            foreach (var t in coll1)
            {
                if (!set.Contains(t))
                {
                    result.Add(t);
                }
            }

            return result;
        }

        /// <summary>
        /// 判断指定集合是否包含指定值
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="value">需要查找的值</param>
        /// <returns>如果集合为空（null或者空），返回false，否则找到元素返回true</returns>
        public static bool Contains(ICollection collection, object value)
        {
            if (IsEmpty(collection))
            {
                return false;
            }
            foreach (var item in collection)
            {
                if (object.Equals(item, value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 安全地判断指定集合是否包含指定值
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="value">需要查找的值</param>
        /// <returns>如果集合为空（null或者空），返回false，否则找到元素返回true</returns>
        public static bool SafeContains(ICollection collection, object value)
        {
            try
            {
                return Contains(collection, value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 自定义函数判断集合是否包含某类值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="containFunc">自定义判断函数</param>
        /// <returns>是否包含自定义规则的值</returns>
        public static bool Contains<T>(ICollection<T> collection, Func<T, bool> containFunc)
        {
            if (IsEmpty(collection))
            {
                return false;
            }
            foreach (var t in collection)
            {
                if (containFunc(t))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 其中一个集合在另一个集合中是否至少包含一个元素，即是两个集合是否至少有一个共同的元素
        /// </summary>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>其中一个集合在另一个集合中是否至少包含一个元素</returns>
        public static bool ContainsAny(ICollection coll1, ICollection coll2)
        {
            if (IsEmpty(coll1) || IsEmpty(coll2))
            {
                return false;
            }
            if (coll1.Count < coll2.Count)
            {
                foreach (var item in coll1)
                {
                    if (Contains(coll2, item))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (var item in coll2)
                {
                    if (Contains(coll1, item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 集合1中是否包含集合2中所有的元素
        /// </summary>
        /// <param name="coll1">集合1</param>
        /// <param name="coll2">集合2</param>
        /// <returns>集合1中是否包含集合2中所有的元素</returns>
        public static bool ContainsAll(ICollection coll1, ICollection coll2)
        {
            if (IsEmpty(coll1))
            {
                return IsEmpty(coll2);
            }

            if (IsEmpty(coll2))
            {
                return true;
            }

            // Set直接判定
            if (coll1 is ISet<object> set)
            {
                return set.IsSupersetOf(coll2.Cast<object>());
            }

            // 参考Apache commons collection4
            // 将时间复杂度降低到O(n + m)
            var elementsAlreadySeen = new HashSet<object>(coll1.Count);
            foreach (var nextElement in coll2)
            {
                if (elementsAlreadySeen.Contains(nextElement))
                {
                    continue;
                }

                bool foundCurrentElement = false;
                foreach (var p in coll1)
                {
                    elementsAlreadySeen.Add(p);
                    if (object.Equals(nextElement, p))
                    {
                        foundCurrentElement = true;
                        break;
                    }
                }

                if (!foundCurrentElement)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 根据集合返回一个元素计数的 Dictionary
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<T, int> CountMap<T>(IEnumerable<T> collection)
        {
            var map = new Dictionary<T, int>();
            if (collection == null)
            {
                return map;
            }

            foreach (var item in collection)
            {
                if (map.ContainsKey(item))
                {
                    map[item]++;
                }
                else
                {
                    map[item] = 1;
                }
            }
            return map;
        }

        /// <summary>
        /// 以 conjunction 为分隔符将集合转换为字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="iterable">可枚举对象</param>
        /// <param name="conjunction">分隔符</param>
        /// <param name="func">集合元素转换器，将元素转换为字符串</param>
        /// <returns>连接后的字符串</returns>
        public static string Join<T>(IEnumerable<T> iterable, string conjunction, Func<T, string> func)
        {
            if (iterable == null)
            {
                return null;
            }
            return string.Join(conjunction, iterable.Select(func));
        }

        /// <summary>
        /// 以 conjunction 为分隔符将集合转换为字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="iterable">可枚举对象</param>
        /// <param name="conjunction">分隔符</param>
        /// <returns>连接后的字符串</returns>
        public static string Join<T>(IEnumerable<T> iterable, string conjunction)
        {
            if (iterable == null)
            {
                return null;
            }
            return string.Join(conjunction, iterable);
        }

        /// <summary>
        /// 以 conjunction 为分隔符将集合转换为字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="iterable">可枚举对象</param>
        /// <param name="conjunction">分隔符</param>
        /// <param name="prefix">每个元素添加的前缀，null表示不添加</param>
        /// <param name="suffix">每个元素添加的后缀，null表示不添加</param>
        /// <returns>连接后的字符串</returns>
        public static string Join<T>(IEnumerable<T> iterable, string conjunction, string prefix, string suffix)
        {
            if (iterable == null)
            {
                return null;
            }
            return string.Join(conjunction, iterable.Select(item => $"{prefix}{item}{suffix}"));
        }

        /// <summary>
        /// 切取部分数据
        /// 切取后的栈将减少这些元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="surplusAlaDatas">原数据</param>
        /// <param name="partSize">每部分数据的长度</param>
        /// <returns>切取出的数据或null</returns>
        public static List<T> PopPart<T>(Stack<T> surplusAlaDatas, int partSize)
        {
            if (IsEmpty(surplusAlaDatas))
            {
                return new List<T>();
            }

            var currentAlaDatas = new List<T>();
            int size = surplusAlaDatas.Count;
            // 切割
            if (size > partSize)
            {
                for (int i = 0; i < partSize; i++)
                {
                    currentAlaDatas.Add(surplusAlaDatas.Pop());
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    currentAlaDatas.Add(surplusAlaDatas.Pop());
                }
            }
            return currentAlaDatas;
        }

        /// <summary>
        /// 切取部分数据
        /// 切取后的栈将减少这些元素
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="surplusAlaDatas">原数据</param>
        /// <param name="partSize">每部分数据的长度</param>
        /// <returns>切取出的数据或null</returns>
        public static List<T> PopPart<T>(Queue<T> surplusAlaDatas, int partSize)
        {
            if (IsEmpty(surplusAlaDatas))
            {
                return new List<T>();
            }

            var currentAlaDatas = new List<T>();
            int size = surplusAlaDatas.Count;
            // 切割
            if (size > partSize)
            {
                for (int i = 0; i < partSize; i++)
                {
                    currentAlaDatas.Add(surplusAlaDatas.Dequeue());
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    currentAlaDatas.Add(surplusAlaDatas.Dequeue());
                }
            }
            return currentAlaDatas;
        }

        /// <summary>
        /// 是否至少有一个符合判断条件
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">自定义判断函数</param>
        /// <returns>是否有一个值匹配 布尔值</returns>
        public static bool AnyMatch<T>(ICollection<T> collection, Func<T, bool> predicate)
        {
            if (IsEmpty(collection))
            {
                return false;
            }
            return collection.Any(predicate);
        }

        /// <summary>
        /// 是否全部匹配判断条件
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="predicate">自定义判断函数</param>
        /// <returns>是否全部匹配 布尔值</returns>
        public static bool AllMatch<T>(ICollection<T> collection, Func<T, bool> predicate)
        {
            if (IsEmpty(collection))
            {
                return false;
            }
            return collection.All(predicate);
        }

        /// <summary>
        /// 新建一个HashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="ts">元素数组</param>
        /// <returns>HashSet对象</returns>
        public static HashSet<T> NewHashSet<T>(params T[] ts)
        {
            return Set(false, ts);
        }

        /// <summary>
        /// 新建一个LinkedHashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="ts">元素数组</param>
        /// <returns>LinkedHashSet对象</returns>
        public static HashSet<T> NewLinkedHashSet<T>(params T[] ts)
        {
            return (HashSet<T>)Set(true, ts);
        }

        /// <summary>
        /// 新建一个HashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="isSorted">是否有序，有序返回 LinkedHashSet，否则返回 HashSet</param>
        /// <param name="ts">元素数组</param>
        /// <returns>HashSet对象</returns>
        public static HashSet<T> Set<T>(bool isSorted, params T[] ts)
        {
            if (ts == null)
            {
                return new HashSet<T>();
            }
            int initialCapacity = Math.Max((int)(ts.Length / 0.75f) + 1, 16);
            var set = new HashSet<T>(initialCapacity);
            foreach (var t in ts)
            {
                set.Add(t);
            }
            return set;
        }

        /// <summary>
        /// 新建一个HashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>HashSet对象</returns>
        public static HashSet<T> NewHashSet<T>(ICollection<T> collection)
        {
            return NewHashSet(false, collection);
        }

        /// <summary>
        /// 新建一个HashSet
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="isSorted">是否有序，有序返回 LinkedHashSet，否则返回HashSet</param>
        /// <param name="collection">集合，用于初始化Set</param>
        /// <returns>HashSet对象</returns>
        public static HashSet<T> NewHashSet<T>(bool isSorted, ICollection<T> collection)
        {
            return new HashSet<T>(collection);
        }

        /// <summary>
        /// 新建一个ArrayList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="values">数组</param>
        /// <returns>ArrayList对象</returns>
        public static List<T> NewArrayList<T>(params T[] values)
        {
            return values == null ? new List<T>() : new List<T>(values);
        }

        /// <summary>
        /// 数组转为ArrayList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="values">数组</param>
        /// <returns>ArrayList对象</returns>
        public static List<T> ToList<T>(params T[] values)
        {
            return NewArrayList(values);
        }

        /// <summary>
        /// 新建一个ArrayList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>ArrayList对象</returns>
        public static List<T> NewArrayList<T>(ICollection<T> collection)
        {
            return collection == null ? new List<T>() : new List<T>(collection);
        }

        /// <summary>
        /// 新建一个ArrayList
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="iterable">可枚举对象</param>
        /// <returns>ArrayList对象</returns>
        public static List<T> NewArrayList<T>(IEnumerable<T> iterable)
        {
            return iterable == null ? new List<T>() : new List<T>(iterable);
        }

        /// <summary>
        /// 新建LinkedList
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="values">数组</param>
        /// <returns>LinkedList</returns>
        public static LinkedList<T> NewLinkedList<T>(params T[] values)
        {
            return values == null ? new LinkedList<T>() : new LinkedList<T>(values);
        }

        /// <summary>
        /// 去重集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>ArrayList</returns>
        public static List<T> Distinct<T>(ICollection<T> collection)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }
            if (collection is ISet<T>)
            {
                return new List<T>(collection);
            }
            return new List<T>(new HashSet<T>(collection));
        }

        /// <summary>
        /// 根据函数生成的KEY去重集合，如根据Bean的某个或者某些字段完成去重
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="K">唯一键类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="uniqueGenerator">唯一键生成器</param>
        /// <param name="override">是否覆盖模式，如果为true，加入的新值会覆盖相同key的旧值，否则会忽略新加值</param>
        /// <returns>ArrayList</returns>
        public static List<T> Distinct<T, K>(ICollection<T> collection, Func<T, K> uniqueGenerator, bool @override)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }

            var set = new Dictionary<K, T>();
            foreach (var item in collection)
            {
                var key = uniqueGenerator(item);
                if (@override || !set.ContainsKey(key))
                {
                    set[key] = item;
                }
            }
            return new List<T>(set.Values);
        }

        /// <summary>
        /// 截取集合的部分
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">被截取的数组</param>
        /// <param name="start">开始位置（包含）</param>
        /// <param name="end">结束位置（不包含）</param>
        /// <returns>截取后的数组，当开始位置超过最大时，返回空集合</returns>
        public static List<T> Sub<T>(ICollection<T> collection, int start, int end)
        {
            return Sub(collection, start, end, 1);
        }

        /// <summary>
        /// 截取集合的部分
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">被截取的数组</param>
        /// <param name="start">开始位置（包含）</param>
        /// <param name="end">结束位置（不包含）</param>
        /// <param name="step">步进</param>
        /// <returns>截取后的数组，当开始位置超过最大时，返回空集合</returns>
        public static List<T> Sub<T>(ICollection<T> collection, int start, int end, int step)
        {
            if (IsEmpty(collection))
            {
                return new List<T>();
            }

            var list = collection as List<T> ?? new List<T>(collection);
            if (start >= list.Count)
            {
                return new List<T>();
            }

            start = Math.Max(0, start);
            end = Math.Min(list.Count, end);
            var result = new List<T>();
            for (int i = start; i < end; i += step)
            {
                result.Add(list[i]);
            }
            return result;
        }

        /// <summary>
        /// 对集合按照指定长度分段，每一个段为单独的集合，返回这个集合的列表
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="size">每个段的长度</param>
        /// <returns>分段列表</returns>
        public static List<List<T>> Split<T>(ICollection<T> collection, int size)
        {
            var result = new List<List<T>>();
            if (IsEmpty(collection))
            {
                return result;
            }

            var initSize = Math.Min(collection.Count, size);
            var subList = new List<T>(initSize);
            foreach (var t in collection)
            {
                if (subList.Count >= size)
                {
                    result.Add(subList);
                    subList = new List<T>(initSize);
                }
                subList.Add(t);
            }
            result.Add(subList);
            return result;
        }

        /// <summary>
        /// 编辑，此方法产生一个新集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="editor">编辑器接口，null返回原集合</param>
        /// <returns>过滤后的集合</returns>
        public static ICollection<T> Edit<T>(ICollection<T> collection, Func<T, T> editor)
        {
            if (collection == null || editor == null)
            {
                return collection;
            }

            var collection2 = Create<T>(collection.GetType());
            if (IsEmpty(collection))
            {
                return collection2;
            }

            foreach (var t in collection)
            {
                var modified = editor(t);
                if (modified != null)
                {
                    collection2.Add(modified);
                }
            }
            return collection2;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="filter">过滤器，null返回原集合</param>
        /// <returns>过滤后的数组</returns>
        public static ICollection<T> FilterNew<T>(ICollection<T> collection, Func<T, bool> filter)
        {
            if (collection == null || filter == null)
            {
                return collection;
            }
            return Edit(collection, t => filter(t) ? t : default);
        }

        /// <summary>
        /// 去掉集合中的多个元素，此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <typeparam name="E">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="elesRemoved">被去掉的元素数组</param>
        /// <returns>原集合</returns>
        public static T RemoveAny<T, E>(T collection, params E[] elesRemoved) where T : ICollection<E>
        {
            var set = NewHashSet(elesRemoved);
            var itemsToRemove = collection.Where(item => set.Contains(item)).ToList();
            foreach (var item in itemsToRemove)
            {
                collection.Remove(item);
            }
            return collection;
        }

        /// <summary>
        /// 去除指定元素，此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <typeparam name="E">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="filter">过滤器</param>
        /// <returns>处理后的集合</returns>
        public static T Filter<T, E>(T collection, Func<E, bool> filter) where T : ICollection<E>
        {
            var itemsToRemove = collection.Where(item => !filter(item)).ToList();
            foreach (var item in itemsToRemove)
            {
                collection.Remove(item);
            }
            return collection;
        }

        /// <summary>
        /// 去除null 元素，此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <typeparam name="E">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>处理后的集合</returns>
        public static T RemoveNull<T, E>(T collection) where T : ICollection<E>
        {
            return Filter<T, E>(collection, item => item != null);
        }

        /// <summary>
        /// 去除null或者"" 元素，此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>处理后的集合</returns>
        public static T RemoveEmpty<T>(T collection) where T : ICollection<string>
        {
            return Filter<T, string>(collection, item => !string.IsNullOrEmpty(item));
        }

        /// <summary>
        /// 去除null或者""或者空白字符串 元素，此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>处理后的集合</returns>
        public static T RemoveBlank<T>(T collection) where T : ICollection<string>
        {
            return Filter<T, string>(collection, item => !string.IsNullOrWhiteSpace(item));
        }

        /// <summary>
        /// 移除集合中的多个元素，并将结果存放到指定的集合
        /// 此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <typeparam name="E">集合元素类型</typeparam>
        /// <param name="targetCollection">被操作移除元素的集合</param>
        /// <param name="resultCollection">存放移除结果的集合</param>
        /// <param name="predicate">用于是否移除判断的过滤器</param>
        /// <returns>移除结果的集合</returns>
        public static T RemoveWithAddIf<T, E>(T targetCollection, T resultCollection, Func<E, bool> predicate) where T : ICollection<E>
        {
            var itemsToRemove = targetCollection.Where(predicate).ToList();
            foreach (var item in itemsToRemove)
            {
                targetCollection.Remove(item);
                resultCollection.Add(item);
            }
            return resultCollection;
        }

        /// <summary>
        /// 移除集合中的多个元素，并将结果存放到生成的新集合中后返回
        /// 此方法直接修改原集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <typeparam name="E">集合元素类型</typeparam>
        /// <param name="targetCollection">被操作移除元素的集合</param>
        /// <param name="predicate">用于是否移除判断的过滤器</param>
        /// <returns>移除结果的集合</returns>
        public static List<E> RemoveWithAddIf<T, E>(T targetCollection, Func<E, bool> predicate) where T : ICollection<E>
        {
            var removed = new List<E>();
            RemoveWithAddIf(targetCollection, (ICollection<E>)removed, predicate);
            return removed;
        }

        /// <summary>
        /// 通过func自定义一个规则，此规则将原集合中的元素转换成新的元素，生成新的列表返回
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="R">返回集合元素类型</typeparam>
        /// <param name="collection">原集合</param>
        /// <param name="func">编辑函数</param>
        /// <param name="ignoreNull">是否忽略空值，这里的空值包括函数处理前和处理后的null值</param>
        /// <returns>抽取后的新列表</returns>
        public static List<R> Map<T, R>(IEnumerable<T> collection, Func<T, R> func, bool ignoreNull)
        {
            var fieldValueList = new List<R>();
            if (collection == null)
            {
                return fieldValueList;
            }

            foreach (var t in collection)
            {
                if (t == null && ignoreNull)
                {
                    continue;
                }
                var value = func(t);
                if (value == null && ignoreNull)
                {
                    continue;
                }
                fieldValueList.Add(value);
            }
            return fieldValueList;
        }

        /// <summary>
        /// 查找第一个匹配元素对象
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="filter">过滤器，满足过滤条件的第一个元素将被返回</param>
        /// <returns>满足过滤条件的第一个元素</returns>
        public static T FindOne<T>(IEnumerable<T> collection, Func<T, bool> filter)
        {
            if (collection != null)
            {
                foreach (var t in collection)
                {
                    if (filter(t))
                    {
                        return t;
                    }
                }
            }
            return default;
        }

        /// <summary>
        /// 集合中匹配规则的数量
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="iterable">可枚举对象</param>
        /// <param name="matcher">匹配器，为空则全部匹配</param>
        /// <returns>匹配数量</returns>
        public static int Count<T>(IEnumerable<T> iterable, Func<T, bool> matcher)
        {
            int count = 0;
            if (iterable != null)
            {
                foreach (var t in iterable)
                {
                    if (matcher == null || matcher(t))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 获取匹配规则定义中匹配到元素的第一个位置
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="matcher">匹配器，为空则全部匹配</param>
        /// <returns>第一个位置</returns>
        public static int IndexOf<T>(ICollection<T> collection, Func<T, bool> matcher)
        {
            if (IsNotEmpty(collection))
            {
                int index = 0;
                foreach (var t in collection)
                {
                    if (matcher == null || matcher(t))
                    {
                        return index;
                    }
                    index++;
                }
            }
            return -1;
        }

        // 辅助方法

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty(ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 检查集合是否为空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 检查集合是否非空
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>是否非空</returns>
        public static bool IsNotEmpty(ICollection collection)
        {
            return !IsEmpty(collection);
        }

        /// <summary>
        /// 检查集合是否非空
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>是否非空</returns>
        public static bool IsNotEmpty<T>(ICollection<T> collection)
        {
            return !IsEmpty(collection);
        }

        /// <summary>
        /// 获取集合大小
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>集合大小</returns>
        public static int Size(ICollection collection)
        {
            return collection?.Count ?? 0;
        }

        /// <summary>
        /// 获取集合大小
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>集合大小</returns>
        public static int Size<T>(ICollection<T> collection)
        {
            return collection?.Count ?? 0;
        }

        /// <summary>
        /// 向集合中添加所有元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="target">目标集合</param>
        /// <param name="items">要添加的元素</param>
        public static void AddAll<T>(ICollection<T> target, params T[] items)
        {
            if (target != null && items != null)
            {
                foreach (var item in items)
                {
                    target.Add(item);
                }
            }
        }

        /// <summary>
        /// 向集合中添加所有元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="target">目标集合</param>
        /// <param name="items">要添加的元素</param>
        public static void AddAll<T>(ICollection<T> target, ICollection<T> items)
        {
            if (target != null && items != null)
            {
                foreach (var item in items)
                {
                    target.Add(item);
                }
            }
        }

        /// <summary>
        /// 创建新的集合对象
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collectionType">集合类型</param>
        /// <returns>集合类型对应的实例</returns>
        public static ICollection<T> Create<T>(Type collectionType)
        {
            if (collectionType == typeof(List<T>))
            {
                return new List<T>();
            }
            if (collectionType == typeof(HashSet<T>))
            {
                return new HashSet<T>();
            }
            if (collectionType == typeof(LinkedList<T>))
            {
                return new LinkedList<T>();
            }
            // 默认返回List
            return new List<T>();
        }
    }

    /// <summary>
    /// 集合相关工具类，包括数组，是 CollUtil 的别名工具类
    /// </summary>
    public class CollectionUtil : CollUtil
    {
    }
}