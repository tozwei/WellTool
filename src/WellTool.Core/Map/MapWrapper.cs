using System.Collections;
using System.Runtime.Serialization;

namespace WellTool.Core.Map;

/// <summary>
/// Map包装类，通过包装一个已有Map实现特定功能
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class MapWrapper<K, V> : IDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>, ISerializable
{
    protected IDictionary<K, V> _raw;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="raw">被包装的Map</param>
    public MapWrapper(IDictionary<K, V> raw)
    {
        _raw = raw;
    }

    /// <summary>
    /// 获取原始的Map
    /// </summary>
    public IDictionary<K, V> Raw => _raw;

    public int Count => _raw.Count;
    public bool IsReadOnly => _raw.IsReadOnly;

    public V this[K key] { get => _raw[key]; set => _raw[key] = value; }
    public ICollection<K> Keys => _raw.Keys;
    public ICollection<V> Values => _raw.Values;

    public void Add(K key, V value) => _raw.Add(key, value);
    public void Add(KeyValuePair<K, V> item) => _raw.Add(item);
    public void Clear() => _raw.Clear();
    public bool Contains(KeyValuePair<K, V> item) => _raw.Contains(item);
    public bool ContainsKey(K key) => _raw.ContainsKey(key);
    public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) => _raw.CopyTo(array, arrayIndex);
    public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => _raw.GetEnumerator();
    public bool Remove(K key) => _raw.Remove(key);
    public bool Remove(KeyValuePair<K, V> item) => _raw.Remove(item);
    public bool TryGetValue(K key, out V value) => _raw.TryGetValue(key, out value!);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// 批量添加键值对
    /// </summary>
    /// <param name="other">其他Map</param>
    public void AddRange(IDictionary<K, V> other)
    {
        foreach (var item in other)
        {
            _raw.Add(item);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is MapWrapper<K, V> other)
        {
            return Equals(_raw, other._raw);
        }
        return false;
    }

    public override int GetHashCode() => _raw.GetHashCode();
    public override string ToString() => _raw.ToString() ?? "";

    public void ForEach(Action<K, V> action)
    {
        foreach (var kvp in _raw)
        {
            action(kvp.Key, kvp.Value);
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Raw", _raw);
    }

    protected MapWrapper(SerializationInfo info, StreamingContext context)
    {
        _raw = (IDictionary<K, V>)info.GetValue("Raw", typeof(IDictionary<K, V>))!;
    }
}
