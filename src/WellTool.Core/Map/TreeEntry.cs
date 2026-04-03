namespace WellTool.Core.Map;

/// <summary>
/// TreeMap的Entry实现
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class TreeEntry<K, V> : KeyValuePair<K, V>
{
    /// <summary>
    /// 颜色，红色或黑色
    /// </summary>
    public bool Color { get; set; }

    /// <summary>
    /// 左子节点
    /// </summary>
    public TreeEntry<K, V>? Left { get; set; }

    /// <summary>
    /// 右子节点
    /// </summary>
    public TreeEntry<K, V>? Right { get; set; }

    /// <summary>
    /// 父节点
    /// </summary>
    public TreeEntry<K, V>? Parent { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    public TreeEntry()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public TreeEntry(K key, V value)
    {
        base.Key = key;
        base.Value = value;
    }

    public new K Key => base.Key;
    public new V Value { get => base.Value; set => base.Value = value; }

    /// <summary>
    /// 获取祖父节点
    /// </summary>
    public TreeEntry<K, V>? GrandParent
    {
        get
        {
            var p = Parent;
            return p?.Parent;
        }
    }

    /// <summary>
    /// 获取兄弟节点
    /// </summary>
    public TreeEntry<K, V>? Sibling
    {
        get
        {
            var p = Parent;
            if (p == null)
                return null;
            if (this == p.Left)
                return p.Right;
            else
                return p.Left;
        }
    }

    /// <summary>
    /// 获取叔伯节点
    /// </summary>
    public TreeEntry<K, V>? Uncle
    {
        get
        {
            var p = Parent;
            if (p == null)
                return null;
            return p.Sibling;
        }
    }

    public override string ToString()
    {
        return $"{Key}={Value}";
    }
}
