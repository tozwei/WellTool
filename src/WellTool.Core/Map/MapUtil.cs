using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace WellTool.Core.Map
{
    /// <summary>
    /// Map工具类
    /// </summary>
    public static class MapUtil
    {
        /// <summary>
        /// 默认初始大小
        /// </summary>
        public static readonly int DEFAULT_INITIAL_CAPACITY = 16;
        /// <summary>
        /// 默认增长因子，当Map的size达到 容量*增长因子时，开始扩充Map
        /// </summary>
        public static readonly float DEFAULT_LOAD_FACTOR = 0.75f;

        /// <summary>
        /// 创建一个空的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <returns>空字典</returns>
        public static Dictionary<K, V> Empty<K, V>() where K : notnull
        {
            return new Dictionary<K, V>();
        }

        /// <summary>
        /// 如果提供的字典为null，返回一个空字典，否则返回原字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">提供的字典，可能为null</param>
        /// <returns>原字典，若为null返回空字典</returns>
        public static Dictionary<K, V> EmptyIfNull<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return map ?? new Dictionary<K, V>();
        }

        /// <summary>
        /// 如果给定字典为空，返回默认字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="defaultMap">默认字典</param>
        /// <returns>非空的原字典或默认字典</returns>
        public static Dictionary<K, V> DefaultIfEmpty<K, V>(Dictionary<K, V> map, Dictionary<K, V> defaultMap) where K : notnull
        {
            return IsEmpty(map) ? defaultMap : map;
        }

        /// <summary>
        /// 将单一键值对转换为字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(K key, V value) where K : notnull
        {
            var map = new Dictionary<K, V>();
            map[key] = value;
            return map;
        }

        /// <summary>
        /// 将单一键值对转换为字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isLinked">是否有序</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(K key, V value, bool isLinked) where K : notnull
        {
            var map = NewHashMap<K, V>(isLinked);
            map[key] = value;
            return map;
        }

        /// <summary>
        /// 将数组转换为字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="array">数组，元素类型为KeyValuePair、数组、IEnumerable</param>
        /// <returns>字典</returns>
        public static Dictionary<object, object> Of(object[] array)
        {
            if (array == null)
            {
                return null;
            }
            var map = new Dictionary<object, object>();
            for (int i = 0; i < array.Length; i++)
            {
                var obj = array[i];
                if (obj is KeyValuePair<object, object> entry)
                {
                    map[entry.Key] = entry.Value;
                }
                else if (obj is object[] entryArray)
                {
                    if (entryArray.Length > 1)
                    {
                        map[entryArray[0]] = entryArray[1];
                    }
                }
                else if (obj is IEnumerable enumerable)
                {
                    var enumerator = enumerable.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        var key = enumerator.Current;
                        if (enumerator.MoveNext())
                        {
                            var val = enumerator.Current;
                            map[key] = val;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Array element {i}, '{obj}', is not type of KeyValuePair or Array or IEnumerable");
                }
            }
            return map;
        }

        /// <summary>
        /// 将键和值转换为KeyValuePair
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isImmutable">是否不可变（在C#中KeyValuePair本身是不可变的）</param>
        /// <returns>KeyValuePair</returns>
        public static KeyValuePair<K, V> Entry<K, V>(K key, V value, bool isImmutable)
        {
            return new KeyValuePair<K, V>(key, value);
        }

        /// <summary>
        /// 根据字典类型返回对应类型的空字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <typeparam name="T">字典类型</typeparam>
        /// <param name="mapType">字典类型</param>
        /// <returns>空字典</returns>
        public static T Empty<K, V, T>(Type mapType) where T : Dictionary<K, V>, new() where K : notnull
        {
            return new T();
        }

        /// <summary>
        /// 从字典中获取指定键列表对应的值列表
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="keys">键迭代器</param>
        /// <returns>值列表</returns>
        public static List<V> ValuesOfKeys<K, V>(Dictionary<K, V> map, IEnumerator<K> keys) where K : notnull
        {
            var values = new List<V>();
            if (map == null || keys == null)
            {
                return values;
            }
            while (keys.MoveNext())
            {
                if (map.TryGetValue(keys.Current, out var value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(default);
                }
            }
            return values;
        }

        /// <summary>
        /// 新建一个HashMap
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <returns>HashMap对象</returns>
        public static Dictionary<K, V> NewHashMap<K, V>() where K : notnull
        {
            return new Dictionary<K, V>();
        }

        /// <summary>
        /// 新建一个HashMap
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="size">初始大小</param>
        /// <param name="isLinked">Map的Key是否有序，有序返回LinkedDictionary，否则返回Dictionary</param>
        /// <returns>HashMap对象</returns>
        public static Dictionary<K, V> NewHashMap<K, V>(int size, bool isLinked) where K : notnull
        {
            if (isLinked)
            {
                return new Dictionary<K, V>(size);
            }
            return new Dictionary<K, V>(size);
        }

        /// <summary>
        /// 新建一个HashMap
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="size">初始大小</param>
        /// <returns>HashMap对象</returns>
        public static Dictionary<K, V> NewHashMap<K, V>(int size) where K : notnull
        {
            return NewHashMap<K, V>(size, false);
        }

        /// <summary>
        /// 新建一个HashMap
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="isLinked">Map的Key是否有序，有序返回LinkedDictionary，否则返回Dictionary</param>
        /// <returns>HashMap对象</returns>
        public static Dictionary<K, V> NewHashMap<K, V>(bool isLinked) where K : notnull
        {
            return NewHashMap<K, V>(DEFAULT_INITIAL_CAPACITY, isLinked);
        }

        /// <summary>
        /// 新建TreeMap，Key有序的Map
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="comparator">Key比较器</param>
        /// <returns>TreeMap</returns>
        public static SortedDictionary<K, V> NewTreeMap<K, V>(IComparer<K> comparator) where K : notnull
        {
            return new SortedDictionary<K, V>(comparator);
        }

        /// <summary>
        /// 新建TreeMap，Key有序的Map
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="comparator">Key比较器</param>
        /// <returns>TreeMap</returns>
        public static SortedDictionary<K, V> NewTreeMap<K, V>(Dictionary<K, V> map, IComparer<K> comparator) where K : notnull
        {
            var treeMap = new SortedDictionary<K, V>(comparator);
            if (IsNotEmpty(map))
            {
                foreach (var entry in map)
                {
                    treeMap[entry.Key] = entry.Value;
                }
            }
            return treeMap;
        }

        /// <summary>
        /// 创建键不重复Map
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="size">初始容量</param>
        /// <returns>IdentityDictionary</returns>
        public static Dictionary<K, V> NewIdentityMap<K, V>(int size) where K : notnull
        {
            return new Dictionary<K, V>(size);
        }

        /// <summary>
        /// 新建一个初始容量为DEFAULT_INITIAL_CAPACITY的ConcurrentHashMap
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <returns>ConcurrentHashMap</returns>
        public static ConcurrentDictionary<K, V> NewConcurrentHashMap<K, V>() where K : notnull
        {
            return new ConcurrentDictionary<K, V>();
        }

        /// <summary>
        /// 新建一个ConcurrentHashMap
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="size">初始容量，当传入的容量小于等于0时，容量为DEFAULT_INITIAL_CAPACITY</param>
        /// <returns>ConcurrentHashMap</returns>
        public static ConcurrentDictionary<K, V> NewConcurrentHashMap<K, V>(int size) where K : notnull
        {
            var initCapacity = size <= 0 ? DEFAULT_INITIAL_CAPACITY : size;
            return new ConcurrentDictionary<K, V>();
        }

        /// <summary>
        /// 传入一个Map将其转化为ConcurrentHashMap类型
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">map</param>
        /// <returns>ConcurrentHashMap</returns>
        public static ConcurrentDictionary<K, V> NewConcurrentHashMap<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (IsEmpty(map))
            {
                return new ConcurrentDictionary<K, V>();
            }
            var concurrentMap = new ConcurrentDictionary<K, V>();
            foreach (var entry in map)
            {
                concurrentMap[entry.Key] = entry.Value;
            }
            return concurrentMap;
        }

        /// <summary>
        /// 创建Map
        /// </summary>
        /// <typeparam name="K">map键类型</typeparam>
        /// <typeparam name="V">map值类型</typeparam>
        /// <param name="mapType">map类型</param>
        /// <returns>Map实例</returns>
        public static Dictionary<K, V> CreateMap<K, V>(Type mapType) where K : notnull
        {
            if (mapType == null || typeof(Dictionary<K, V>).IsAssignableFrom(mapType))
            {
                return new Dictionary<K, V>();
            }
            else
            {
                try
                {
                    return (Dictionary<K, V>)Activator.CreateInstance(mapType);
                }
                catch
                {
                    // 不支持的map类型，返回默认的Dictionary
                    return new Dictionary<K, V>();
                }
            }
        }

        /// <summary>
        /// 行转列，合并相同的键，值合并为列表
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="mapList">Map列表</param>
        /// <returns>Map</returns>
        public static Dictionary<K, List<V>> ToListMap<K, V>(IEnumerable<Dictionary<K, V>> mapList) where K : notnull
        {
            var resultMap = new Dictionary<K, List<V>>();
            if (mapList == null)
            {
                return resultMap;
            }

            foreach (var map in mapList)
            {
                if (map == null)
                {
                    continue;
                }
                foreach (var entry in map)
                {
                    var key = entry.Key;
                    if (!resultMap.TryGetValue(key, out var valueList))
                    {
                        valueList = new List<V>();
                        resultMap[key] = valueList;
                    }
                    valueList.Add(entry.Value);
                }
            }

            return resultMap;
        }

        /// <summary>
        /// 列转行。将Map中值列表分别按照其位置与key组成新的map。
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="listMap">列表Map</param>
        /// <returns>Map列表</returns>
        public static List<Dictionary<K, V>> ToMapList<K, V>(Dictionary<K, IEnumerable<V>> listMap) where K : notnull
        {
            var resultList = new List<Dictionary<K, V>>();
            if (IsEmpty(listMap))
            {
                return resultList;
            }

            bool isEnd; // 是否结束。标准是元素列表已耗尽
            int index = 0; // 值索引
            do
            {
                isEnd = true;
                var map = new Dictionary<K, V>();
                foreach (var entry in listMap)
                {
                    var vList = entry.Value?.ToList();
                    if (vList != null && index < vList.Count)
                    {
                        map[entry.Key] = vList[index];
                        if (index != vList.Count - 1)
                        {
                            // 当值列表中还有更多值（非最后一个），继续循环
                            isEnd = false;
                        }
                    }
                }
                if (map.Count > 0)
                {
                    resultList.Add(map);
                }
                index++;
            } while (!isEnd);

            return resultList;
        }

        /// <summary>
        /// 根据给定的entry列表，根据entry的key进行分组;
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="entries">entry列表</param>
        /// <returns>entries</returns>
        public static Dictionary<K, List<V>> Grouping<K, V>(IEnumerable<KeyValuePair<K, V>> entries) where K : notnull
        {
            var map = new Dictionary<K, List<V>>();
            if (entries == null)
            {
                return map;
            }
            foreach (var pair in entries)
            {
                if (!map.TryGetValue(pair.Key, out var values))
                {
                    values = new List<V>();
                    map[pair.Key] = values;
                }
                values.Add(pair.Value);
            }
            return map;
        }

        /// <summary>
        /// 将已知Map转换为key为驼峰风格的Map
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">原Map</param>
        /// <returns>驼峰风格Map</returns>
        public static Dictionary<K, V> ToCamelCaseMap<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (IsEmpty(map))
            {
                return new Dictionary<K, V>();
            }
            var result = new Dictionary<K, V>();
            foreach (var entry in map)
            {
                if (entry.Key is string keyStr)
                {
                    var camelCaseKey = ToCamelCase(keyStr);
                    result[(K)(object)camelCaseKey] = entry.Value;
                }
                else
                {
                    result[entry.Key] = entry.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// 将字符串转换为驼峰风格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>驼峰风格字符串</returns>
        private static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            var parts = str.Split('_');
            var result = parts[0].ToLower();
            for (int i = 1; i < parts.Length; i++)
            {
                if (!string.IsNullOrEmpty(parts[i]))
                {
                    result += char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
                }
            }
            return result;
        }

        /// <summary>
        /// 将键值对转换为二维数组，第一维是key，第二维是value
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">map</param>
        /// <returns>数组</returns>
        public static object[][] ToObjectArray<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (map == null)
            {
                return null;
            }
            var result = new object[map.Count][];
            if (map.Count == 0)
            {
                return result;
            }
            int index = 0;
            foreach (var entry in map)
            {
                result[index] = new object[] { entry.Key, entry.Value };
                index++;
            }
            return result;
        }

        /// <summary>
        /// 将map转成字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="separator">entry之间的连接符</param>
        /// <param name="keyValueSeparator">kv之间的连接符</param>
        /// <param name="otherParams">其它附加参数字符串（例如密钥）</param>
        /// <returns>连接字符串</returns>
        public static string Join<K, V>(IDictionary<K, V> map, string separator, string keyValueSeparator, params string[] otherParams) where K : notnull
        {
            return Join(map, separator, keyValueSeparator, false, otherParams);
        }

        /// <summary>
        /// 根据参数排序后拼接为字符串，常用于签名
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="params">参数</param>
        /// <param name="separator">entry之间的连接符</param>
        /// <param name="keyValueSeparator">kv之间的连接符</param>
        /// <param name="isIgnoreNull">是否忽略null的键和值</param>
        /// <param name="otherParams">其它附加参数字符串（例如密钥）</param>
        /// <returns>签名字符串</returns>
        public static string SortJoin<K, V>(IDictionary<K, V> @params, string separator, string keyValueSeparator, bool isIgnoreNull, params string[] otherParams) where K : notnull
        {
            var sortedMap = Sort(@params);
            return Join<K, V>(sortedMap, separator, keyValueSeparator, isIgnoreNull, otherParams);
        }

        /// <summary>
        /// 将map转成字符串，忽略null的键和值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="separator">entry之间的连接符</param>
        /// <param name="keyValueSeparator">kv之间的连接符</param>
        /// <param name="otherParams">其它附加参数字符串（例如密钥）</param>
        /// <returns>连接后的字符串</returns>
        public static string JoinIgnoreNull<K, V>(IDictionary<K, V> map, string separator, string keyValueSeparator, params string[] otherParams) where K : notnull
        {
            return Join(map, separator, keyValueSeparator, true, otherParams);
        }

        /// <summary>
        /// 将map转成字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map，为空返回otherParams拼接</param>
        /// <param name="separator">entry之间的连接符</param>
        /// <param name="keyValueSeparator">kv之间的连接符</param>
        /// <param name="isIgnoreNull">是否忽略null的键和值</param>
        /// <param name="otherParams">其它附加参数字符串（例如密钥）</param>
        /// <returns>连接后的字符串，map和otherParams为空返回""</returns>
        public static string Join<K, V>(IDictionary<K, V> map, string separator, string keyValueSeparator, bool isIgnoreNull, params string[] otherParams) where K : notnull
        {
            var strBuilder = new System.Text.StringBuilder();
            bool isFirst = true;
            if (IsNotEmpty(map))
            {
                foreach (var entry in map)
                {
                    if (!isIgnoreNull || (entry.Key != null && entry.Value != null))
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            strBuilder.Append(separator);
                        }
                        strBuilder.Append(entry.Key).Append(keyValueSeparator).Append(entry.Value);
                    }
                }
            }
            // 补充其它字符串到末尾，默认无分隔符
            if (otherParams != null)
            {
                foreach (var otherParam in otherParams)
                {
                    strBuilder.Append(otherParam);
                }
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// 编辑器接口
        /// </summary>
        /// <typeparam name="T">编辑的类型</typeparam>
        public interface Editor<T>
        {
            /// <summary>
            /// 编辑
            /// </summary>
            /// <param name="t">被编辑的对象</param>
            /// <returns>编辑后的对象</returns>
            T Edit(T t);
        }

        /// <summary>
        /// 过滤器接口
        /// </summary>
        /// <typeparam name="T">过滤的类型</typeparam>
        public interface IFilter<T>
        {
            /// <summary>
            /// 是否接受
            /// </summary>
            /// <param name="t">被过滤的对象</param>
            /// <returns>是否接受</returns>
            bool Accept(T t);
        }

        /// <summary>
        /// 编辑Map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="editor">编辑器接口</param>
        /// <returns>编辑后的Map</returns>
        public static Dictionary<K, V> Edit<K, V>(Dictionary<K, V> map, Editor<KeyValuePair<K, V>> editor) where K : notnull
        {
            if (map == null || editor == null)
            {
                return map;
            }

            var map2 = new Dictionary<K, V>();
            if (IsEmpty(map))
            {
                return map2;
            }

            foreach (var entry in map)
            {
                var modified = editor.Edit(entry);
                if (modified.Key != null)
                {
                    map2[modified.Key] = modified.Value;
                }
            }
            return map2;
        }

        /// <summary>
        /// 编辑Map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="func">编辑函数</param>
        /// <returns>编辑后的Map</returns>
        public static Dictionary<K, V> Edit<K, V>(Dictionary<K, V> map, Func<KeyValuePair<K, V>, KeyValuePair<K, V>> func) where K : notnull
        {
            if (map == null || func == null)
            {
                return map;
            }
            return Edit(map, new FuncEditor<K, V>(func));
        }

        /// <summary>
        /// 基于Func的编辑器
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        private class FuncEditor<K, V> : Editor<KeyValuePair<K, V>>
        {
            private readonly Func<KeyValuePair<K, V>, KeyValuePair<K, V>> _func;

            public FuncEditor(Func<KeyValuePair<K, V>, KeyValuePair<K, V>> func)
            {
                _func = func;
            }

            public KeyValuePair<K, V> Edit(KeyValuePair<K, V> t)
            {
                return _func(t);
            }
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="filter">过滤器接口，null返回原Map</param>
        /// <returns>过滤后的Map</returns>
        public static Dictionary<K, V> Filter<K, V>(Dictionary<K, V> map, IFilter<KeyValuePair<K, V>> filter) where K : notnull
        {
            if (map == null || filter == null)
            {
                return map;
            }
            return Edit(map, new FilterEditor<K, V>(filter));
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="predicate">过滤条件</param>
        /// <returns>过滤后的Map</returns>
        public static Dictionary<K, V> Filter<K, V>(Dictionary<K, V> map, Func<KeyValuePair<K, V>, bool> predicate) where K : notnull
        {
            if (map == null || predicate == null)
            {
                return map;
            }
            return Filter(map, new PredicateFilter<KeyValuePair<K, V>>(predicate));
        }

        /// <summary>
        /// 过滤器编辑器
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        private class FilterEditor<K, V> : Editor<KeyValuePair<K, V>>
        {
            private readonly IFilter<KeyValuePair<K, V>> _filter;

            public FilterEditor(IFilter<KeyValuePair<K, V>> filter)
            {
                _filter = filter;
            }

            public KeyValuePair<K, V> Edit(KeyValuePair<K, V> t)
            {
                return _filter.Accept(t) ? t : new KeyValuePair<K, V>(default, default);
            }
        }

        /// <summary>
        /// 基于Predicate的过滤器
        /// </summary>
        /// <typeparam name="T">过滤类型</typeparam>
        private class PredicateFilter<T> : IFilter<T>
        {
            private readonly Func<T, bool> _predicate;

            public PredicateFilter(Func<T, bool> predicate)
            {
                _predicate = predicate;
            }

            public bool Accept(T t)
            {
                return _predicate(t);
            }
        }

        /// <summary>
        /// 通过biFunction自定义一个规则，此规则将原Map中的元素转换成新的元素，生成新的Map返回
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <typeparam name="R">新的，修改后的value的类型</typeparam>
        /// <param name="map">原有的map</param>
        /// <param name="biFunction">lambda，参数包含key,value，返回值会作为新的value</param>
        /// <returns>值可以为不同类型的Map</returns>
        public static Dictionary<K, R> Transform<K, V, R>(Dictionary<K, V> map, Func<K, V, R> biFunction) where K : notnull
        {
            if (map == null || biFunction == null)
            {
                return new Dictionary<K, R>();
            }
            var result = new Dictionary<K, R>();
            foreach (var entry in map)
            {
                result[entry.Key] = biFunction(entry.Key, entry.Value);
            }
            return result;
        }

        /// <summary>
        /// 通过biFunction自定义一个规则，此规则将原Map中的元素转换成新的元素，生成新的Map返回
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <typeparam name="R">新的，修改后的value的类型</typeparam>
        /// <param name="map">原有的map</param>
        /// <param name="biFunction">lambda，参数包含key,value，返回值会作为新的value</param>
        /// <returns>值可以为不同类型的Map</returns>
        public static Dictionary<K, R> Map<K, V, R>(Dictionary<K, V> map, Func<K, V, R> biFunction) where K : notnull
        {
            return Transform(map, biFunction);
        }

        /// <summary>
        /// 过滤Map保留指定键值对，如果键不存在跳过
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">原始Map</param>
        /// <param name="keys">键列表，null返回原Map</param>
        /// <returns>Map结果，结果的Map类型与原Map保持一致</returns>
        public static Dictionary<K, V> Filter<K, V>(Dictionary<K, V> map, params K[] keys) where K : notnull
        {
            if (map == null || keys == null)
            {
                return map;
            }

            var map2 = new Dictionary<K, V>();
            if (IsEmpty(map))
            {
                return map2;
            }

            foreach (var key in keys)
            {
                if (map.ContainsKey(key))
                {
                    map2[key] = map[key];
                }
            }
            return map2;
        }

        /// <summary>
        /// 去掉Map中指定key的键值对，修改原Map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="keys">键列表</param>
        /// <returns>修改后的key</returns>
        public static Dictionary<K, V> RemoveAny<K, V>(Dictionary<K, V> map, params K[] keys) where K : notnull
        {
            if (map == null || keys == null)
            {
                return map;
            }
            foreach (var key in keys)
            {
                map.Remove(key);
            }
            return map;
        }

        /// <summary>
        /// 获取Map的部分key生成新的Map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="keys">键列表</param>
        /// <returns>新Map，只包含指定的key</returns>
        public static Dictionary<K, V> GetAny<K, V>(Dictionary<K, V> map, params K[] keys) where K : notnull
        {
            return Filter(map, keys);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetStr<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, string.Empty);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static string GetStr<K, V>(Dictionary<K, V> map, K key, string defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Integer
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static int GetInt<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            if (map == null)
            {
                return 0;
            }
            if (map.TryGetValue(key, out var value) && value != null)
            {
                return Convert.ToInt32(value);
            }
            return 0;
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Integer
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static int GetInt<K, V>(Dictionary<K, V> map, K key, int defaultValue) where K : notnull
        {
            if (map == null)
            {
                return defaultValue;
            }
            if (map.TryGetValue(key, out var value) && value != null)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Double
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static double GetDouble<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, 0.0);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Double
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static double GetDouble<K, V>(Dictionary<K, V> map, K key, double defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Float
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static float GetFloat<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, 0.0f);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Float
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static float GetFloat<K, V>(Dictionary<K, V> map, K key, float defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Short
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static short GetShort<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, (short)0);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Short
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static short GetShort<K, V>(Dictionary<K, V> map, K key, short defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Bool
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static bool GetBool<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, false);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Bool
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static bool GetBool<K, V>(Dictionary<K, V> map, K key, bool defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Character
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static char GetChar<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, '\0');
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Character
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static char GetChar<K, V>(Dictionary<K, V> map, K key, char defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Long
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static long GetLong<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, 0L);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为Long
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static long GetLong<K, V>(Dictionary<K, V> map, K key, long defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为DateTime
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static DateTime GetDate<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return Get(map, key, DateTime.MinValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为DateTime
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static DateTime GetDate<K, V>(Dictionary<K, V> map, K key, DateTime defaultValue) where K : notnull
        {
            return Get(map, key, defaultValue);
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为指定类型
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <typeparam name="T">目标值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static T Get<K, V, T>(Dictionary<K, V> map, K key, T defaultValue) where K : notnull
        {
            if (map == null)
            {
                return defaultValue;
            }
            if (map.TryGetValue(key, out var value) && value != null)
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取Map指定key的值，并转换为指定类型，此方法在转换失败后不抛异常，返回null。
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <typeparam name="T">目标值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static T GetQuietly<K, V, T>(Dictionary<K, V> map, K key, T defaultValue) where K : notnull
        {
            try
            {
                return Get(map, key, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Map的键和值互换
        /// </summary>
        /// <typeparam name="T">键和值类型</typeparam>
        /// <param name="map">Map对象，键值类型必须一致</param>
        /// <returns>互换后的Map</returns>
        public static Dictionary<T, T> Reverse<T>(Dictionary<T, T> map)
        {
            if (map == null)
            {
                return null;
            }
            var result = new Dictionary<T, T>();
            foreach (var entry in map)
            {
                result[entry.Value] = entry.Key;
            }
            return result;
        }

        /// <summary>
        /// Map的键和值互换
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map对象</param>
        /// <returns>互换后的Map</returns>
        public static Dictionary<V, K> Inverse<K, V>(Dictionary<K, V> map) where K : notnull where V : notnull
        {
            if (map == null)
            {
                return null;
            }
            var result = new Dictionary<V, K>();
            foreach (var entry in map)
            {
                result[entry.Value] = entry.Key;
            }
            return result;
        }

        /// <summary>
        /// 排序已有Map，Key有序的Map，使用默认Key排序方式（字母顺序）
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <returns>TreeMap</returns>
        public static SortedDictionary<K, V> Sort<K, V>(IDictionary<K, V> map) where K : notnull
        {
            return Sort(map, null);
        }

        /// <summary>
        /// 排序已有Map，Key有序的Map
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map，为null返回null</param>
        /// <param name="comparator">Key比较器</param>
        /// <returns>TreeMap，map为null返回null</returns>
        public static SortedDictionary<K, V> Sort<K, V>(IDictionary<K, V> map, IComparer<K> comparator) where K : notnull
        {
            if (map == null)
            {
                return null;
            }
            var treeMap = new SortedDictionary<K, V>(comparator);
            foreach (var entry in map)
            {
                treeMap[entry.Key] = entry.Value;
            }
            return treeMap;
        }

        /// <summary>
        /// 按照值排序，可选是否倒序
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">需要对值排序的map</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns>排序后新的Map</returns>
        public static Dictionary<K, V> SortByValue<K, V>(Dictionary<K, V> map, bool isDesc)
            where K : notnull
            where V : IComparable<V>
        {
            if (map == null)
            {
                return null;
            }
            var result = new Dictionary<K, V>();
            var sortedEntries = map.OrderBy(entry => entry.Value);
            if (isDesc)
            {
                sortedEntries = map.OrderByDescending(entry => entry.Value);
            }
            foreach (var entry in sortedEntries)
            {
                result[entry.Key] = entry.Value;
            }
            return result;
        }

        /// <summary>
        /// 创建代理Map
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">被代理的Map</param>
        /// <returns>MapProxy</returns>
        public static MapWrapper<K, V> CreateProxy<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return Wrap(map);
        }

        /// <summary>
        /// 创建Map包装类MapWrapper
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">被代理的Map</param>
        /// <returns>MapWrapper</returns>
        public static MapWrapper<K, V> Wrap<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return new MapWrapper<K, V>(map);
        }

        /// <summary>
        /// 将对应Map转换为不可修改的Map
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <returns>不修改Map</returns>
        public static IReadOnlyDictionary<K, V> Unmodifiable<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return new System.Collections.ObjectModel.ReadOnlyDictionary<K, V>(map);
        }

        /// <summary>
        /// Map构建器
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        public class MapBuilder<K, V>
        {
            private readonly Dictionary<K, V> _map;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="map">底层Map</param>
            public MapBuilder(Dictionary<K, V> map)
            {
                _map = map;
            }

            /// <summary>
            /// 添加键值对
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="value">值</param>
            /// <returns>当前构建器</returns>
            public MapBuilder<K, V> Put(K key, V value)
            {
                _map[key] = value;
                return this;
            }

            /// <summary>
            /// 构建Map
            /// </summary>
            /// <returns>Map</returns>
            public Dictionary<K, V> Build()
            {
                return _map;
            }
        }

        /// <summary>
        /// 创建链接调用map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <returns>map创建类</returns>
        public static MapBuilder<K, V> Builder<K, V>()
        {
            return Builder(new Dictionary<K, V>());
        }

        /// <summary>
        /// 创建链接调用map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="map">实际使用的map</param>
        /// <returns>map创建类</returns>
        public static MapBuilder<K, V> Builder<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return new MapBuilder<K, V>(map);
        }

        /// <summary>
        /// 创建链接调用map
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        /// <returns>map创建类</returns>
        public static MapBuilder<K, V> Builder<K, V>(K k, V v) where K : notnull
        {
            return Builder(new Dictionary<K, V>()).Put(k, v);
        }

        /// <summary>
        /// 重命名键
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="oldKey">原键</param>
        /// <param name="newKey">新键</param>
        /// <returns>map</returns>
        public static Dictionary<K, V> RenameKey<K, V>(Dictionary<K, V> map, K oldKey, K newKey) where K : notnull
        {
            if (IsNotEmpty(map) && map.ContainsKey(oldKey))
            {
                if (map.ContainsKey(newKey))
                {
                    throw new ArgumentException($"The key '{newKey}' exist !");
                }
                map[newKey] = map[oldKey];
                map.Remove(oldKey);
            }
            return map;
        }

        /// <summary>
        /// 去除Map中值为null的键值对
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <returns>map</returns>
        public static Dictionary<K, V> RemoveNullValue<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (IsEmpty(map))
            {
                return map;
            }
            var keysToRemove = new List<K>();
            foreach (var entry in map)
            {
                if (entry.Value == null)
                {
                    keysToRemove.Add(entry.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                map.Remove(key);
            }
            return map;
        }

        /// <summary>
        /// 去除Map中值为指定值的键值对
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="value">给定值</param>
        /// <returns>map</returns>
        public static Dictionary<K, V> RemoveByValue<K, V>(Dictionary<K, V> map, V value) where K : notnull
        {
            if (IsEmpty(map))
            {
                return map;
            }
            var keysToRemove = new List<K>();
            foreach (var entry in map)
            {
                if (Equals(entry.Value, value))
                {
                    keysToRemove.Add(entry.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                map.Remove(key);
            }
            return map;
        }

        /// <summary>
        /// 去除Map中满足条件的键值对
        /// </summary>
        /// <typeparam name="K">key的类型</typeparam>
        /// <typeparam name="V">value的类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="predicate">移除条件</param>
        /// <returns>map</returns>
        public static Dictionary<K, V> RemoveIf<K, V>(Dictionary<K, V> map, Func<KeyValuePair<K, V>, bool> predicate) where K : notnull
        {
            if (IsEmpty(map))
            {
                return map;
            }
            var keysToRemove = new List<K>();
            foreach (var entry in map)
            {
                if (predicate(entry))
                {
                    keysToRemove.Add(entry.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                map.Remove(key);
            }
            return map;
        }



        /// <summary>
        /// 清除一个或多个Map集合内的元素，每个Map调用clear()方法
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="maps">一个或多个Map</param>
        public static void Clear<K, V>(params Dictionary<K, V>[] maps) where K : notnull
        {
            foreach (var map in maps)
            {
                if (IsNotEmpty(map))
                {
                    map.Clear();
                }
            }
        }

        /// <summary>
        /// 从Map中获取指定键列表对应的值列表
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="keys">键列表</param>
        /// <returns>值列表</returns>
        public static List<V> ValuesOfKeys<K, V>(Dictionary<K, V> map, IEnumerable<K> keys) where K : notnull
        {
            var values = new List<V>();
            if (map == null || keys == null)
            {
                return values;
            }
            foreach (var key in keys)
            {
                if (map.TryGetValue(key, out var value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(default);
                }
            }
            return values;
        }

        /// <summary>
        /// 将键和值转换为KeyValuePair
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>KeyValuePair</returns>
        public static KeyValuePair<K, V> Entry<K, V>(K key, V value)
        {
            return new KeyValuePair<K, V>(key, value);
        }

        /// <summary>
        /// 如果 key 对应的 value 不存在，则使用获取 mappingFunction 重新计算后的值，并保存为该 key 的 value，否则返回 value。
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="key">键</param>
        /// <param name="mappingFunction">值不存在时值的生成函数</param>
        /// <returns>值</returns>
        public static V ComputeIfAbsent<K, V>(Dictionary<K, V> map, K key, Func<K, V> mappingFunction) where K : notnull
        {
            if (map == null)
            {
                return default;
            }
            if (!map.TryGetValue(key, out var value) || value == null)
            {
                value = mappingFunction(key);
                map[key] = value;
            }
            return value;
        }

        /// <summary>
        /// 将一个Map按照固定大小拆分成多个子Map
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="size">子Map的大小</param>
        /// <returns>子Map列表</returns>
        public static List<Dictionary<K, V>> Partition<K, V>(Dictionary<K, V> map, int size) where K : notnull
        {
            if (map == null)
            {
                throw new ArgumentException("Map cannot be null");
            }
            if (size <= 0)
            {
                throw new ArgumentException("Size must be greater than 0");
            }
            var result = new List<Dictionary<K, V>>();
            var currentMap = new Dictionary<K, V>();
            int count = 0;
            foreach (var entry in map)
            {
                currentMap[entry.Key] = entry.Value;
                count++;
                if (count >= size)
                {
                    result.Add(currentMap);
                    currentMap = new Dictionary<K, V>();
                    count = 0;
                }
            }
            if (currentMap.Count > 0)
            {
                result.Add(currentMap);
            }
            return result;
        }

        /// <summary>
        /// 将多层级Map处理为一个层级Map类型
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">入参Map</param>
        /// <returns>单层级Map返回值</returns>
        public static Dictionary<K, V> Flatten<K, V>(Dictionary<K, V> map) where K : notnull
        {
            var result = new Dictionary<K, V>();
            if (map == null)
            {
                return result;
            }
            Flatten(map, result);
            return result;
        }

        /// <summary>
        /// 递归调用将多层级Map处理为一个层级Map类型
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">入参Map</param>
        /// <param name="flatMap">单层级Map返回值</param>
        private static void Flatten<K, V>(Dictionary<K, V> map, Dictionary<K, V> flatMap) where K : notnull
        {
            if (map == null)
            {
                return;
            }
            foreach (var entry in map)
            {
                if (entry.Value is Dictionary<K, V> nestedMap)
                {
                    Flatten(nestedMap, flatMap);
                }
                else
                {
                    flatMap[entry.Key] = entry.Value;
                }
            }
        }

        /// <summary>
        /// 创建一个包含指定键值对的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="pairs">键值对数组</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(params KeyValuePair<K, V>[] pairs)
        {
            var map = new Dictionary<K, V>();
            foreach (var pair in pairs)
            {
                map[pair.Key] = pair.Value;
            }
            return map;
        }

        /// <summary>
        /// 创建一个包含指定键值对的字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="keys">键数组</param>
        /// <param name="values">值数组</param>
        /// <returns>字典</returns>
        public static Dictionary<K, V> Of<K, V>(K[] keys, V[] values)
        {
            var map = new Dictionary<K, V>();
            for (int i = 0; i < keys.Length && i < values.Length; i++)
            {
                map[keys[i]] = values[i];
            }
            return map;
        }

        /// <summary>
        /// 检查字典是否为空
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<K, V>(IDictionary<K, V> map) where K : notnull
        {
            return map == null || map.Count == 0;
        }

        /// <summary>
        /// 检查字典是否不为空
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotEmpty<K, V>(IDictionary<K, V> map) where K : notnull
        {
            return !IsEmpty(map);
        }

        /// <summary>
        /// 获取字典的大小
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>字典大小</returns>
        public static int Size<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return map?.Count ?? 0;
        }

        /// <summary>
        /// 检查字典是否包含指定键
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public static bool ContainsKey<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return map?.ContainsKey(key) ?? false;
        }

        /// <summary>
        /// 检查字典是否包含指定值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="value">值</param>
        /// <returns>是否包含</returns>
        public static bool ContainsValue<K, V>(Dictionary<K, V> map, V value) where K : notnull
        {
            return map?.ContainsValue(value) ?? false;
        }

        /// <summary>
        /// 获取字典中的值，如果键不存在则返回默认值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public static V Get<K, V>(Dictionary<K, V> map, K key, V defaultValue = default)
        {
            if (map == null)
            {
                return defaultValue;
            }
            if (map.TryGetValue(key, out var value) && value != null)
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 设置字典中的值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Put<K, V>(Dictionary<K, V> map, K key, V value) where K : notnull
        {
            map[key] = value;
        }

        /// <summary>
        /// 移除字典中的键值对
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public static bool Remove<K, V>(Dictionary<K, V> map, K key) where K : notnull
        {
            return map?.Remove(key) ?? false;
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        public static void Clear<K, V>(Dictionary<K, V> map)
        {
            map?.Clear();
        }

        /// <summary>
        /// 获取字典的所有键
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>键集合</returns>
        public static ICollection<K> Keys<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return map != null ? map.Keys.ToList() : new List<K>();
        }

        /// <summary>
        /// 获取字典的所有值
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>值集合</returns>
        public static ICollection<V> Values<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return map != null ? map.Values.ToList() : new List<V>();
        }

        /// <summary>
        /// 获取字典的所有键值对
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>键值对集合</returns>
        public static ICollection<KeyValuePair<K, V>> Entries<K, V>(Dictionary<K, V> map) where K : notnull
        {
            return map?.ToList() ?? new List<KeyValuePair<K, V>>();
        }

        /// <summary>
        /// 合并两个字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map1">第一个字典</param>
        /// <param name="map2">第二个字典</param>
        /// <returns>合并后的字典</returns>
        public static Dictionary<K, V> Merge<K, V>(Dictionary<K, V> map1, Dictionary<K, V> map2) where K : notnull
        {
            var result = new Dictionary<K, V>();
            if (map1 != null)
            {
                foreach (var pair in map1)
                {
                    result[pair.Key] = pair.Value;
                }
            }
            if (map2 != null)
            {
                foreach (var pair in map2)
                {
                    result[pair.Key] = pair.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// 克隆字典
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>克隆后的字典</returns>
        public static Dictionary<K, V> Clone<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (map == null)
            {
                return null;
            }
            return new Dictionary<K, V>(map);
        }

        /// <summary>
        /// 将字典转换为字符串
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="map">字典</param>
        /// <returns>字符串表示</returns>
        public static string ToString<K, V>(Dictionary<K, V> map) where K : notnull
        {
            if (IsEmpty(map))
            {
                return "{}";
            }
            var entries = map.Select(pair => $"{pair.Key}={pair.Value}");
            return "{" + string.Join(", ", entries) + "}";
        }

        /// <summary>
        /// 将嵌套的字典扁平化为单层字典
        /// </summary>
        /// <param name="map">嵌套字典</param>
        /// <returns>扁平化后的字典</returns>
        public static Dictionary<string, object> Flatten(Dictionary<string, object> map)
        {
            var result = new Dictionary<string, object>();
            if (map == null)
            {
                return result;
            }
            FlattenRecursive(map, "", result);
            return result;
        }

        /// <summary>
        /// 递归扁平化字典
        /// </summary>
        /// <param name="map">当前字典</param>
        /// <param name="prefix">前缀</param>
        /// <param name="result">结果字典</param>
        private static void FlattenRecursive(Dictionary<string, object> map, string prefix, Dictionary<string, object> result)
        {
            foreach (var entry in map)
            {
                var key = string.IsNullOrEmpty(prefix) ? entry.Key : prefix + entry.Key;
                if (entry.Value is Dictionary<string, object> nestedMap)
                {
                    FlattenRecursive(nestedMap, key, result);
                }
                else if (entry.Value is Dictionary<string, string> stringMap)
                {
                    // 处理 Dictionary<string, string> 类型的嵌套字典
                    foreach (var stringEntry in stringMap)
                    {
                        var nestedKey = key + stringEntry.Key;
                        result[nestedKey] = stringEntry.Value;
                    }
                }
                else
                {
                    result[key] = entry.Value;
                }
            }
        }
    }
}