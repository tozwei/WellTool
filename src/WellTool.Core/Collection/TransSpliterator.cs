namespace WellTool.Core.Collection;

/// <summary>
/// 转换Spliterator
/// </summary>
/// <typeparam name="T">源类型</typeparam>
/// <typeparam name="R">目标类型</typeparam>
public class TransSpliterator<T, R> : IEnumerable<R>
{
    private readonly IEnumerable<T> _source;
    private readonly Func<T, R> _converter;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="source">源集合</param>
    /// <param name="converter">转换器</param>
    public TransSpliterator(IEnumerable<T> source, Func<T, R> converter)
    {
        _source = source;
        _converter = converter;
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
