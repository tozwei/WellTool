namespace WellTool.Core.Lang;

/// <summary>
/// 替换器接口
/// </summary>
/// <typeparam name="T">要替换的对象类型</typeparam>
public interface Replacer<T>
{
    /// <summary>
    /// 替换对象
    /// </summary>
    /// <param name="t">要替换的对象</param>
    /// <returns>替换后的对象</returns>
    T Replace(T t);
}

/// <summary>
/// 链式替换器
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class ChainReplacer<T> : Replacer<T>
{
    private readonly System.Collections.Generic.List<Replacer<T>> _replacers = new System.Collections.Generic.List<Replacer<T>>();

    /// <summary>
    /// 添加替换器
    /// </summary>
    public ChainReplacer<T> Add(Replacer<T> replacer)
    {
        _replacers.Add(replacer);
        return this;
    }

    /// <summary>
    /// 执行替换
    /// </summary>
    public T Replace(T t)
    {
        foreach (var replacer in _replacers)
        {
            t = replacer.Replace(t);
        }
        return t;
    }
}
