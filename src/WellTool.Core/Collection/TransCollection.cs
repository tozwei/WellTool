namespace WellTool.Core.Collection;

/// <summary>
/// 转换集合
/// </summary>
/// <typeparam name="T">源类型</typeparam>
/// <typeparam name="R">目标类型</typeparam>
public class TransCollection<T, R> : ICollection<R>
{
    private readonly ICollection<T> _source;
    private readonly Func<T, R> _converter;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="source">源集合</param>
    /// <param name="converter">转换器</param>
    public TransCollection(ICollection<T> source, Func<T, R> converter)
    {
        _source = source;
        _converter = converter;
    }

    public int Count => _source.Count;
    public bool IsReadOnly => true;

    public void Add(R item) => throw new NotSupportedException();
    public void Clear() => throw new NotSupportedException();
    public bool Remove(R item) => throw new NotSupportedException();

    public bool Contains(R item)
    {
        foreach (var t in _source)
        {
            if (Equals(_converter(t), item))
            {
                return true;
            }
        }
        return false;
    }

    public void CopyTo(R[] array, int arrayIndex)
    {
        foreach (var item in _source)
        {
            array[arrayIndex++] = _converter(item);
        }
    }

    public IEnumerator<R> GetEnumerator()
    {
        foreach (var item in _source)
        {
            yield return _converter(item);
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
