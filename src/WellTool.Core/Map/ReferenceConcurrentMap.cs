using System.Collections.Concurrent;

namespace WellTool.Core.Map;

/// <summary>
/// ReferenceConcurrentMap - 使用弱引用或软引用的并发Map
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class ReferenceConcurrentMap<K, V> where K : class
{
    private readonly ConcurrentDictionary<WeakReference<K>, V> _map = new();
    private readonly ConcurrentQueue<WeakReference<K>> _queue = new();

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值</returns>
    public V? Get(K key)
    {
        PurgeStaleKeys();
        var weakKey = new WeakReference<K>(key);
        return _map.TryGetValue(weakKey, out var value) ? value : default;
    }

    /// <summary>
    /// 添加或更新值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>原值</returns>
    public V Put(K key, V value)
    {
        PurgeStaleKeys();
        var weakKey = new WeakReference<K>(key);
        _map[weakKey] = value;
        return value;
    }

    /// <summary>
    /// 如果不存在则添加
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="valueFactory">值工厂</param>
    /// <returns>值</returns>
    public V GetOrAdd(K key, Func<K, V> valueFactory)
    {
        var existing = Get(key);
        if (existing != null)
        {
            return existing;
        }

        var value = valueFactory(key);
        Put(key, value);
        return value;
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>被移除的值</returns>
    public V? Remove(K key)
    {
        PurgeStaleKeys();
        var weakKey = new WeakReference<K>(key);
        _map.TryRemove(weakKey, out var value);
        return value;
    }

    /// <summary>
    /// 获取元素数量
    /// </summary>
    public int Count
    {
        get
        {
            PurgeStaleKeys();
            return _map.Count;
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        _map.Clear();
    }

    /// <summary>
    /// 清理已回收的键
    /// </summary>
    private void PurgeStaleKeys()
    {
        var staleKeys = _map.Keys.Where(k => !k.TryGetTarget(out _)).ToList();
        foreach (var key in staleKeys)
        {
            _map.TryRemove(key, out _);
        }
    }
}
