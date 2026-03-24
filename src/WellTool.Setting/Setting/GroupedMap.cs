using System.Collections.Concurrent;

namespace WellTool.Setting;

/// <summary>
/// 基于分组的 Map<br/>
/// 此对象方法线程安全
/// </summary>
public class GroupedMap : ConcurrentDictionary<string, ConcurrentDictionary<string, string>>
{
    /// <summary>
    /// 获取分组对应的值，如果分组不存在或者值不存在则返回 null
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="key">键</param>
    /// <returns>值，如果分组不存在或者值不存在则返回 null</returns>
    public string? Get(string group, string key)
    {
        group ??= string.Empty;
        if (this.TryGetValue(group, out var map) && map is not null)
        {
            return map.TryGetValue(key, out var value) ? value : null;
        }
        return null;
    }

    /// <summary>
    /// 总的键值对数
    /// </summary>
    /// <returns>总键值对数</returns>
    public new int Count
    {
        get
        {
            var total = 0;
            foreach (var kvp in this)
            {
                if (kvp.Value != null)
                {
                    total += kvp.Value.Count;
                }
            }
            return total;
        }
    }

    /// <summary>
    /// 将键值对加入到对应分组中
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>此 key 之前存在的值，如果没有返回 null</returns>
    public string? Put(string group, string key, string value)
    {
        group = (group ?? string.Empty).Trim();

        var valueMap = this.GetOrAdd(group, k => new ConcurrentDictionary<string, string>());
        return valueMap.AddOrUpdate(key, value, (k, oldValue) => value);
    }

    /// <summary>
    /// 加入多个键值对到某个分组下
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="dictionary">键值对</param>
    /// <returns>this</returns>
    public GroupedMap PutAll(string group, IDictionary<string, string> dictionary)
    {
        foreach (var entry in dictionary)
        {
            this.Put(group, entry.Key, entry.Value);
        }
        return this;
    }

    /// <summary>
    /// 从指定分组中删除指定值
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="key">键</param>
    /// <returns>被删除的值，如果值不存在，返回 null</returns>
    public string? Remove(string group, string key)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            if (valueMap.TryRemove(key, out var value))
            {
                return value;
            }
        }
        return null;
    }

    /// <summary>
    /// 某个分组对应的键值对是否为空
    /// </summary>
    /// <param name="group">分组</param>
    /// <returns>是否为空</returns>
    public bool IsEmpty(string group)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            return valueMap.IsEmpty;
        }
        return true;
    }

    /// <summary>
    /// 是否为空，如果多个分组同时为空，也按照空处理
    /// </summary>
    /// <returns>是否为空，如果多个分组同时为空，也按照空处理</returns>
    public new bool IsEmpty()
    {
        return this.Count == 0;
    }

    /// <summary>
    /// 指定分组中是否包含指定 key
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="key">键</param>
    /// <returns>是否包含 key</returns>
    public bool ContainsKey(string group, string key)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            return valueMap.ContainsKey(key);
        }
        return false;
    }

    /// <summary>
    /// 指定分组中是否包含指定值
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="value">值</param>
    /// <returns>是否包含值</returns>
    public bool ContainsValue(string group, string value)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            foreach (var v in valueMap.Values)
            {
                if (v == value)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 获得所有分组
    /// </summary>
    /// <returns>所有分组</returns>
    public ICollection<string> Groups()
    {
        return base.Keys;
    }

    /// <summary>
    /// 获得指定分组的所有键
    /// </summary>
    /// <param name="group">分组</param>
    /// <returns>键集合</returns>
    public ICollection<string> Keys(string group)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            return valueMap.Keys;
        }
        return Array.Empty<string>();
    }

    /// <summary>
    /// 获得指定分组的所有值
    /// </summary>
    /// <param name="group">分组</param>
    /// <returns>值集合</returns>
    public ICollection<string> Values(string group)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryGetValue(group, out var valueMap) && valueMap is not null)
        {
            return valueMap.Values;
        }
        return Array.Empty<string>();
    }

    /// <summary>
    /// 清空所有分组
    /// </summary>
    public new void Clear()
    {
        base.Clear();
    }

    /// <summary>
    /// 清空指定分组
    /// </summary>
    /// <param name="group">分组</param>
    public void Clear(string group)
    {
        group = (group ?? string.Empty).Trim();

        if (this.TryRemove(group, out _))
        {
            // 移除成功
        }
    }
}
