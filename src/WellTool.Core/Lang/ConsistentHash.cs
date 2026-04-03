namespace WellTool.Core.Lang;

/// <summary>
/// 一致性哈希算法实现
/// </summary>
/// <typeparam name="T">节点类型</typeparam>
public class ConsistentHash<T>
{
    private readonly System.Collections.Generic.SortedDictionary<int, T> _circle = new System.Collections.Generic.SortedDictionary<int, T>();
    private readonly int _virtualNodes;
    private readonly System.Func<T, string> _keyExtractor;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="keyExtractor">节点键提取器</param>
    /// <param name="virtualNodes">虚拟节点数量</param>
    public ConsistentHash(System.Func<T, string> keyExtractor, int virtualNodes = 100)
    {
        _keyExtractor = keyExtractor ?? throw new System.ArgumentNullException(nameof(keyExtractor));
        _virtualNodes = virtualNodes;
    }

    /// <summary>
    /// 添加节点
    /// </summary>
    public void Add(T node)
    {
        string key = _keyExtractor(node);
        for (int i = 0; i < _virtualNodes; i++)
        {
            int hash = GetHash(key + "#" + i);
            _circle[hash] = node;
        }
    }

    /// <summary>
    /// 移除节点
    /// </summary>
    public void Remove(T node)
    {
        string key = _keyExtractor(node);
        for (int i = 0; i < _virtualNodes; i++)
        {
            int hash = GetHash(key + "#" + i);
            _circle.Remove(hash);
        }
    }

    /// <summary>
    /// 获取键对应的节点
    /// </summary>
    public T? Get(string key)
    {
        if (_circle.Count == 0)
        {
            return default;
        }

        int hash = GetHash(key);
        int first = FirstNode(hash);
        return _circle.TryGetValue(first, out var node) ? node : default;
    }

    private int FirstNode(int hash)
    {
        // 顺时针找到第一个节点
        foreach (var kvp in _circle)
        {
            if (kvp.Key >= hash)
            {
                return kvp.Key;
            }
        }

        // 回到第一个节点
        foreach (var kvp in _circle)
        {
            return kvp.Key;
        }

        return 0;
    }

    private static int GetHash(string key)
    {
        // 使用 FNV-1a 算法
        int hash = 2166136261;
        foreach (char c in key)
        {
            hash ^= c;
            hash *= 16777619;
        }
        return hash;
    }
}
