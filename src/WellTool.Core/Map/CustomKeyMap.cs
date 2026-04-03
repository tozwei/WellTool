namespace WellTool.Core.Map;

/// <summary>
/// 自定义键的Map，通过传入自定义键比较器实现特定功能
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class CustomKeyMap<K, V> : Dictionary<K, V>
{
    private readonly IEqualityComparer<K> _comparer;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="comparer">自定义键比较器</param>
    public CustomKeyMap(IEqualityComparer<K> comparer) : base(comparer)
    {
        _comparer = comparer;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dictionary">初始字典</param>
    /// <param name="comparer">自定义键比较器</param>
    public CustomKeyMap(IDictionary<K, V> dictionary, IEqualityComparer<K> comparer) : base(dictionary, comparer)
    {
        _comparer = comparer;
    }
}
