using System;

namespace WellTool.Core.Lang.Loader;

/// <summary>
/// 加载器接口
/// </summary>
/// <typeparam name="T">被加载对象类型</typeparam>
public interface Loader<T>
{
    /// <summary>
    /// 获取加载的对象
    /// </summary>
    /// <returns>被加载的对象</returns>
    T Get();
}
