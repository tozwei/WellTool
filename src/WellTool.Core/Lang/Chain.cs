namespace WellTool.Core.Lang;

/// <summary>
/// 链式调用接口
/// </summary>
/// <typeparam name="T">调用者类型</typeparam>
public interface Chain<T>
{
    /// <summary>
    /// 添加一个对象到链中
    /// </summary>
    Chain<T> Add(T item);

    /// <summary>
    /// 获取链中的所有对象
    /// </summary>
    System.Collections.Generic.IEnumerable<T> GetAll();
}

/// <summary>
/// 链式调用基类
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public abstract class ChainBase<T> : Chain<T>
{
    protected readonly System.Collections.Generic.List<T> _items = new System.Collections.Generic.List<T>();

    /// <summary>
    /// 添加一个对象到链中
    /// </summary>
    public ChainBase<T> Add(T item)
    {
        _items.Add(item);
        return this;
    }

    /// <summary>
    /// 添加多个对象到链中
    /// </summary>
    public ChainBase<T> AddAll(System.Collections.Generic.IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            _items.Add(item);
        }
        return this;
    }

    /// <summary>
    /// 获取链中的所有对象
    /// </summary>
    public System.Collections.Generic.IEnumerable<T> GetAll()
    {
        return _items.AsReadOnly();
    }

    /// <summary>
    /// 获取链中的对象数量
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// 清空链
    /// </summary>
    public void Clear()
    {
        _items.Clear();
    }
}
