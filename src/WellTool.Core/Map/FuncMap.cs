using System.Collections;
using System.Collections.Concurrent;

namespace WellTool.Core.Map;

/// <summary>
/// 函数键的Map，支持使用函数作为键
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class FuncMap<K, V> : IDictionary<K, V>
{
    private readonly ConcurrentDictionary<K, V> _map = new();
    private readonly Func<K, K, bool> _equalsFunc;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="equalsFunc">相等判断函数</param>
    public FuncMap(Func<K, K, bool> equalsFunc)
    {
        _equalsFunc = equalsFunc;
    }

    public V this[K key] { get => _map[key]; set => _map[key] = value; }
    public int Count => _map.Count;
    public bool IsReadOnly => false;
    public ICollection<K> Keys => _map.Keys;
    public ICollection<V> Values => _map.Values;

    public void Add(K key, V value) => _map.TryAdd(key, value);
    public void Add(KeyValuePair<K, V> item) => Add(item.Key, item.Value);
    public void Clear() => _map.Clear();
    public bool Contains(KeyValuePair<K, V> item) => _map.Contains(item);
    public bool ContainsKey(K key) => _map.ContainsKey(key);
    public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex) => ((IDictionary<K, V>)_map).CopyTo(array, arrayIndex);
    public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => _map.GetEnumerator();
    public bool Remove(K key) => _map.TryRemove(key, out _);
    public bool Remove(KeyValuePair<K, V> item) => Remove(item.Key);
    public bool TryGetValue(K key, out V value) => _map.TryGetValue(key, out value!);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
