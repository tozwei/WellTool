using System;

namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 懒加载加载器<br>
/// 在load方法被调用前，对象未被加载，直到被调用后才开始加载<br>
/// 此加载器常用于对象比较庞大而不一定被使用的情况，用于减少启动时资源占用问题<br>
/// 此加载器使用双重检查（Double-Check）方式检查对象是否被加载，避免多线程下重复加载或加载丢失问题
/// </summary>
/// <typeparam name="T">被加载对象类型</typeparam>
[Serializable]
public abstract class LazyLoader<T> : Loader<T>
{
    /// <summary>
    /// 被加载对象
    /// </summary>
    private volatile T _object;

    /// <summary>
    /// 获取一个对象，第一次调用此方法时初始化对象然后返回，之后调用此方法直接返回原对象
    /// </summary>
    public T Get()
    {
        var result = _object;
        if (result == null)
        {
            lock (this)
            {
                result = _object;
                if (result == null)
                {
                    _object = result = Init();
                }
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
