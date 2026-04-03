using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection;

/// <summary>
/// 可重置的迭代器
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class ResettableIter<T> : IEnumerable<T>, IEnumerator<T>
{
    private readonly List<T> _source;
    private int _index = -1;
    private readonly bool _loop;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="source">源集合</param>
    /// <param name="loop">是否循环</param>
    public ResettableIter(IEnumerable<T> source, bool loop = false)
    {
        _source = source is List<T> list ? list : source.ToList();
        _loop = loop;
    }

    public T Current => _source[_index];
    object IEnumerator.Current => Current;

    public void Dispose()
    {
        Reset();
    }

    public IEnumerator<T> GetEnumerator() => this;

    public bool MoveNext()
    {
        _index++;
        if (_index >= _source.Count)
        {
            if (_loop)
            {
                _index = 0;
                return _source.Count > 0;
            }
            return false;
        }
        return true;
    }

    public void Reset()
    {
        _index = -1;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
