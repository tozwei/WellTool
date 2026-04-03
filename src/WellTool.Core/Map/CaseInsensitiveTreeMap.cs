using System.Collections.Concurrent;

namespace WellTool.Core.Map;

/// <summary>
/// 大小写不敏感的有序Map
/// </summary>
public class CaseInsensitiveTreeMap<V> : SortedDictionary<string, V>
{
    /// <summary>
    /// 构造
    /// </summary>
    public CaseInsensitiveTreeMap()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dictionary">初始字典</param>
    public CaseInsensitiveTreeMap(IDictionary<string, V> dictionary) : base(dictionary, StringComparer.OrdinalIgnoreCase)
    {
    }
}
