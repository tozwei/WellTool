using System.Collections.Concurrent;

namespace WellTool.Core.Map;

/// <summary>
/// 弱引用的ConcurrentHashMap实现
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class WeakConcurrentMap<K, V> where K : class
{
    private readonly ConcurrentDictionary<WeakReference<K>, V> _map = new();
    private readonly ConcurrentQueue<WeakReference<K>> _staleKeys = new();

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
    public void Put(K key, V value)
    {
        PurgeStaleKeys();
        var weakKey = new WeakReference<K>(key);
        _map[weakKey] = value;
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否成功移除</returns>
    public bool Remove(K key)
    {
        var weakKey = new WeakReference<K>(key);
        return _map.TryRemove(weakKey, out _);
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
    /// 清理已回收的键
    /// </summary>
    private void PurgeStaleKeys()
    {
        foreach (var key in _map.Keys)
        {
            if (!key.TryGetTarget(out _))
            {
                _map.TryRemove(key, out _);
            }
        }
    }
}
