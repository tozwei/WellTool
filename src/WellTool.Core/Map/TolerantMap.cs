namespace WellTool.Core.Map;

/// <summary>
/// 容错Map，当get不存在的key时返回默认值而非抛出异常
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class TolerantMap<K, V> : Dictionary<K, V>
{
    /// <summary>
    /// 默认值
    /// </summary>
    public V DefaultValue { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    public TolerantMap()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="defaultValue">默认值</param>
    public TolerantMap(V defaultValue)
    {
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dictionary">初始字典</param>
    public TolerantMap(IDictionary<K, V> dictionary) : base(dictionary)
    {
    }

    /// <summary>
    /// 获取值，如果不存在返回默认值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值或默认值</returns>
    public new V Get(K key)
    {
        return TryGetValue(key, out var value) ? value : DefaultValue;
    }

    /// <summary>
    /// 获取值，如果不存在返回默认值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="defaultValue">默认值提供函数</param>
    /// <returns>值或默认值</returns>
    public V GetOrDefault(K key, Func<V> defaultValue)
    {
        return TryGetValue(key, out var value) ? value : defaultValue();
    }
}
