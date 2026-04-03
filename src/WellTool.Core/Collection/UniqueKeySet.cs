namespace WellTool.Core.Collection;

/// <summary>
/// 具有唯一键的集合
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class UniqueKeySet<K, V> : ICollection<V>
{
    private readonly Dictionary<K, V> _dict = new();
    private readonly Func<V, K> _keySelector;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="keySelector">键选择器</param>
    public UniqueKeySet(Func<V, K> keySelector)
    {
        _keySelector = keySelector;
    }

    public int Count => _dict.Count;
    public bool IsReadOnly => false;

    public void Add(V item)
    {
        var key = _keySelector(item);
        _dict[key] = item;
    }

    public void Clear() => _dict.Clear();

    public bool Contains(V item)
    {
        var key = _keySelector(item);
        return _dict.ContainsKey(key);
    }

    public void CopyTo(V[] array, int arrayIndex)
    {
        _dict.Values.CopyTo(array, arrayIndex);
    }

    public IEnumerator<V> GetEnumerator() => _dict.Values.GetEnumerator();

    public bool Remove(V item)
    {
        var key = _keySelector(item);
        return _dict.Remove(key);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// 获取所有键
    /// </summary>
    public ICollection<K> Keys => _dict.Keys;

    /// <summary>
    /// 获取所有值
    /// </summary>
    public ICollection<V> Values => _dict.Values;
}
