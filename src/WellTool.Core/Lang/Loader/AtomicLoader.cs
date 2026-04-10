using System;
using System.Threading;

namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 原子引用加载器<br>
/// 使用Interlocked.CompareExchange实懒加载，过程如下
/// <pre>
/// 1. 检查引用中是否有加载好的对象，有则返回
/// 2. 如果没有则初始化一个对象，并再次比较引用中是否有其它线程加载好的对象，无则加入，有则返回已有的
/// </pre>
/// 当对象未被创建，对象的初始化操作在多线程情况下可能会被调用多次（多次创建对象），但是总是返回同一对象
/// </summary>
/// <typeparam name="T">被加载对象类型</typeparam>
[Serializable]
public abstract class AtomicLoader<T> : Loader<T>
{
    /// <summary>
    /// 被加载对象的引用
    /// </summary>
    private T _reference;

    /// <summary>
    /// 获取一个对象，第一次调用此方法时初始化对象然后返回，之后调用此方法直接返回原对象
    /// </summary>
    public T Get()
    {
        var result = _reference;

        if (result == null)
        {
            result = Init();
            if (Interlocked.CompareExchange(ref _reference, result, null) != null)
            {
                // 其它线程已经创建好此对象
                result = _reference;
            }
        }

        return result;
    }

    /// <summary>
    /// 初始化被加载的对象<br>
    /// 如果对象从未被加载过，调用此方法初始化加载对象，此方法只被调用一次
    /// </summary>
    /// <returns>被加载的对象</returns>
    protected abstract T Init();
}
