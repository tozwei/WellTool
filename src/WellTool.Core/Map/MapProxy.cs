namespace WellTool.Core.Map;

/// <summary>
/// Map的动态代理，用于通过字符串路径访问Map中的值
/// </summary>
public class MapProxy
{
    private readonly IDictionary<string, object?> _map;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="map">被代理的Map</param>
    public MapProxy(IDictionary<string, object?> map)
    {
        _map = map;
    }

    /// <summary>
    /// 创建MapProxy实例
    /// </summary>
    /// <param name="map">被代理的Map</param>
    /// <returns>MapProxy实例</returns>
    public static MapProxy Create(IDictionary<string, object?> map)
    {
        return new MapProxy(map);
    }

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>值</returns>
    public object? Get(string key)
    {
        return _map.TryGetValue(key, out var value) ? value : null;
    }

    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public void Set(string key, object? value)
    {
        _map[key] = value;
    }

    /// <summary>
    /// 检查是否包含指定的键
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否包含</returns>
    public bool ContainsKey(string key)
    {
        return _map.ContainsKey(key);
    }

    /// <summary>
    /// 移除指定的键
    /// </summary>
    /// <param name="key">键</param>
    public void Remove(string key)
    {
        _map.Remove(key);
    }

    /// <summary>
    /// 清空所有键值对
    /// </summary>
    public void Clear()
    {
        _map.Clear();
    }

    /// <summary>
    /// 获取键值对的数量
    /// </summary>
    /// <returns>数量</returns>
    public int Size()
    {
        return _map.Count;
    }

    /// <summary>
    /// 获取嵌套值，支持点号路径，如 "user.name"
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>值</returns>
    public object? GetByPath(string path)
        {
            var parts = path.Split('.');
            object? current = _map;

            foreach (var part in parts)
            {
                if (current is IDictionary<string, object?> dict)
                {
                    current = dict.TryGetValue(part, out var val) ? val : null;
                }
                else if (current is Dictionary<string, object?> dict2)
                {
                    current = dict2.TryGetValue(part, out var val) ? val : null;
                }
                else
                {
                    return null;
                }
            }

            return current;
        }

    /// <summary>
    /// 设置嵌套值，支持点号路径，如 "user.name"
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="value">值</param>
    public void SetByPath(string path, object? value)
    {
        var parts = path.Split('.');
        if (parts.Length == 1)
        {
            _map[path] = value;
            return;
        }

        var current = _map;
        for (int i = 0; i < parts.Length - 1; i++)
        {
            var part = parts[i];
            if (!current.ContainsKey(part))
            {
                current[part] = new Dictionary<string, object?>();
            }
            current = (IDictionary<string, object?>)current[part]!;
        }

        current[parts[^1]] = value;
    }
}
