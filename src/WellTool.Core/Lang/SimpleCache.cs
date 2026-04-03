namespace WellTool.Core.Lang;

/// <summary>
/// 简单缓存实现
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class SimpleCache<K, V>
{
    private readonly System.Collections.Generic.Dictionary<K, V> _cache = new System.Collections.Generic.Dictionary<K, V>();
    private readonly object _lock = new object();

    /// <summary>
    /// 获取缓存值
    /// </summary>
    public V? Get(K key)
    {
        lock (_lock)
        {
            return _cache.TryGetValue(key, out var value) ? value : default;
        }
    }

    /// <summary>
    /// 获取缓存值，如果不存在则调用工厂方法创建
    /// </summary>
    public V GetOrCreate(K key, System.Func<V> factory)
    {
        lock (_lock)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                return value;
            }

            V newValue = factory();
            _cache[key] = newValue;
            return newValue;
        }
    }

    /// <summary>
    /// 设置缓存值
    /// </summary>
    public void Put(K key, V value)
    {
        lock (_lock)
        {
            _cache[key] = value;
        }
    }

    /// <summary>
    /// 移除缓存值
    /// </summary>
    public bool Remove(K key)
    {
        lock (_lock)
        {
            return _cache.Remove(key);
        }
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public void Clear()
    {
        lock (_lock)
        {
            _cache.Clear();
        }
    }

    /// <summary>
    /// 获取缓存数量
    /// </summary>
    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _cache.Count;
            }
        }
    }

    /// <summary>
    /// 是否包含键
    /// </summary>
    public bool ContainsKey(K key)
    {
        lock (_lock)
        {
            return _cache.ContainsKey(key);
        }
    }
}
