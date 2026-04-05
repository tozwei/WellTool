using System.Collections;
using WellTool.Core.Collection;

namespace WellTool.Core.Map;

/// <summary>
/// 可重复键和值的Map
/// 通过键值单独建立List方式，使键值对一一对应，实现正向和反向两种查找
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class TableMap<K, V> : IDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>
{
    private const int DefaultCapacity = 10;

    private readonly List<K> _keys = new();
    private readonly List<V> _values = new();

    /// <summary>
    /// 构造
    /// </summary>
    public TableMap()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="size">初始容量</param>
    public TableMap(int size)
    {
        _keys.Capacity = size;
        _values.Capacity = size;
    }

    public int Count => _keys.Count;
    public bool IsReadOnly => false;
    public ICollection<K> Keys => _keys.AsReadOnly();
    public ICollection<V> Values => _values.AsReadOnly();

    public V this[K key] { get => Get(key)!; set => Put(key, value); }
    V IDictionary<K, V>.this[K key] { get => Get(key)!; set => Put(key, value); }

    public V? Get(K key)
    {
        int index = _keys.IndexOf(key);
        if (index > -1)
        {
            return _values[index];
        }
        return default;
    }

    /// <summary>
    /// 根据value获得对应的key，只返回找到的第一个value对应的key值
    /// </summary>
    /// <param name="value">值</param>
    /// <returns>键</returns>
    public K? GetKey(V value)
    {
        int index = _values.IndexOf(value);
        if (index > -1)
        {
            return _keys[index];
        }
        return default;
    }

    /// <summary>
    /// 获取指定key对应的所有值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值列表</returns>
    public List<V> GetValues(K key)
    {
        var indices = CollUtil.IndexOfAll(_keys, ele => Equals(ele, key));
        return CollUtil.GetAny<V>(_values, indices);
    }

    /// <summary>
    /// 获取指定value对应的所有key
    /// </summary>
    /// <param name="value">值</param>
    /// <returns>键列表</returns>
    public List<K> GetKeys(V value)
    {
        var indices = CollUtil.IndexOfAll(_values, ele => Equals(ele, value));
        return CollUtil.GetAny<K>(_keys, indices);
    }

    /// <summary>
    /// 添加键值对
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public V Put(K key, V value)
    {
        _keys.Add(key);
        _values.Add(value);
        return value;
    }

    /// <summary>
    /// 移除指定的所有键和对应的所有值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>最后一个移除的值</returns>
    public V? RemoveByKey(K key)
    {
        V? lastValue = default;
        int index;
        while ((index = _keys.IndexOf(key)) > -1)
        {
            lastValue = RemoveByIndex(index);
        }
        return lastValue;
    }

    /// <summary>
    /// 移除指定位置的键值对
    /// </summary>
    /// <param name="index">位置</param>
    /// <returns>移除的值</returns>
    public V RemoveByIndex(int index)
    {
        _keys.RemoveAt(index);
        var value = _values[index];
        _values.RemoveAt(index);
        return value;
    }

    // IDictionary implementation
    public void Add(K key, V value) => Put(key, value);
    public bool ContainsKey(K key) => _keys.Contains(key);
    public bool Remove(K key) => throw new NotImplementedException();

    public bool TryGetValue(K key, out V value)
    {
        value = Get(key)!;
        return ContainsKey(key);
    }

    public void Add(KeyValuePair<K, V> item) => Put(item.Key, item.Value);
    public void Clear()
    {
        _keys.Clear();
        _values.Clear();
    }

    public bool Contains(KeyValuePair<K, V> item)
    {
        int index = _keys.IndexOf(item.Key);
        return index > -1 && Equals(_values[index], item.Value);
    }

    public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = new KeyValuePair<K, V>(_keys[i], _values[i]);
        }
    }

    public bool Remove(KeyValuePair<K, V> item)
    {
        int index = _keys.IndexOf(item.Key);
        if (index > -1 && Equals(_values[index], item.Value))
        {
            RemoveByIndex(index);
            return true;
        }
        return false;
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return new KeyValuePair<K, V>(_keys[i], _values[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => $"TableMap{{keys={string.Join(", ", _keys)}, values={string.Join(", ", _values)}}}";
}
