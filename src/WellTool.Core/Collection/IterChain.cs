namespace WellTool.Core.Collection;

/// <summary>
/// 迭代器链，用于链式操作迭代器
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class IterChain<T> : IEnumerable<T>
{
    private readonly List<IEnumerable<T>> _iterables = new();

    /// <summary>
    /// 添加迭代器
    /// </summary>
    /// <param name="iterable">迭代器</param>
    /// <returns>this</returns>
    public IterChain<T> Add(IEnumerable<T> iterable)
    {
        _iterables.Add(iterable);
        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var iterable in _iterables)
        {
            foreach (var item in iterable)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
