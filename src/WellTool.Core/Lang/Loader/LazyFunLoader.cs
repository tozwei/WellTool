using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 函数式懒加载加载器<br>
/// 传入用于生成对象的函数，在对象需要使用时调用生成对象，然后抛弃此生成对象的函数。<br>
/// 此加载器常用于对象比较庞大而不一定被使用的情况，用于减少启动时资源占用问题<br>
/// 继承自<see cref="LazyLoader{T}"/>，如何实现多线程安全，由LazyLoader完成。
/// </summary>
/// <typeparam name="T">被加载对象类型</typeparam>
[Serializable]
public class LazyFunLoader<T> : LazyLoader<T>
{
    /// <summary>
    /// 用于生成对象的函数
    /// </summary>
    private Func<T> _supplier;

    /// <summary>
    /// 静态工厂方法，提供语义性与编码便利性
    /// </summary>
    /// <param name="supplier">用于生成对象的函数</param>
    /// <typeparam name="TResult">对象类型</typeparam>
    /// <returns>函数式懒加载加载器对象</returns>
    public static LazyFunLoader<TResult> On<TResult>(Func<TResult> supplier)
    {
        if (supplier == null)
        {
            throw new ArgumentNullException(nameof(supplier));
        }
        return new LazyFunLoader<TResult>(supplier);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="supplier">用于生成对象的函数</param>
    public LazyFunLoader(Func<T> supplier)
    {
        if (supplier == null)
        {
            throw new ArgumentNullException(nameof(supplier));
        }
        _supplier = supplier;
    }

    /// <summary>
    /// 初始化被加载的对象
    /// </summary>
    /// <returns>被加载的对象</returns>
    protected override T Init()
    {
        var t = _supplier.Invoke();
        _supplier = null;
        return t;
    }

    /// <summary>
    /// 是否已经初始化
    /// </summary>
    /// <returns>是/否</returns>
    public bool IsInitialize()
    {
        return _supplier == null;
    }

    /// <summary>
    /// 如果已经初始化，就执行传入函数
    /// </summary>
    /// <param name="consumer">待执行函数</param>
    public void IfInitialized(Action<T> consumer)
    {
        if (consumer == null)
        {
            throw new ArgumentNullException(nameof(consumer));
        }

        // 已经初始化
        if (IsInitialize())
        {
            consumer(Get());
        }
    }
}
