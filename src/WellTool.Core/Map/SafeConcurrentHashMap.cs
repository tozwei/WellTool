using System.Collections.Concurrent;

namespace WellTool.Core.Map;

/// <summary>
/// 安全的ConcurrentDictionary实现
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class SafeConcurrentHashMap<K, V> : ConcurrentDictionary<K, V>
{
    /// <summary>
    /// 构造，默认初始大小
    /// </summary>
    public SafeConcurrentHashMap()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="initialCapacity">预估初始大小</param>
    public SafeConcurrentHashMap(int initialCapacity) : base()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dictionary">初始键值对</param>
    public SafeConcurrentHashMap(IEnumerable<KeyValuePair<K, V>> dictionary) : base(dictionary)
    {
    }

    /// <summary>
    /// 如果不存在则添加计算后的值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="mappingFunction">计算函数</param>
    /// <returns>值</returns>
    public new V ComputeIfAbsent(K key, Func<K, V> mappingFunction)
    {
        if (TryGetValue(key, out var existingValue))
        {
            return existingValue;
        }

        var newValue = mappingFunction(key);
        if (TryAdd(key, newValue))
        {
            return newValue;
        }

        // 如果添加失败，可能其他线程已经添加
        return this[key];
    }
}
