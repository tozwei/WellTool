namespace WellTool.Core.Lang;

/// <summary>
/// 匹配器接口
/// </summary>
/// <typeparam name="T">要匹配的对象类型</typeparam>
public interface Matcher<T>
{
    /// <summary>
    /// 判断对象是否匹配
    /// </summary>
    bool Match(T t);
}

/// <summary>
/// 匹配器接口（带索引）
/// </summary>
/// <typeparam name="T">要匹配的对象类型</typeparam>
public interface IndexedMatcher<T>
{
    /// <summary>
    /// 判断对象是否匹配
    /// </summary>
    bool Match(T t, int index);
}

/// <summary>
/// 匹配器辅助类
/// </summary>
public static class MatcherUtil
{
    /// <summary>
    /// 创建匹配器
    /// </summary>
    public static Matcher<T> Of<T>(System.Func<T, bool> predicate)
    {
        return new PredicateMatcher<T>(predicate);
    }
}

/// <summary>
/// 基于委托的匹配器
/// </summary>
public class PredicateMatcher<T> : Matcher<T>
{
    private readonly System.Func<T, bool> _predicate;

    public PredicateMatcher(System.Func<T, bool> predicate)
    {
        _predicate = predicate ?? throw new System.ArgumentNullException(nameof(predicate));
    }

    public bool Match(T t)
    {
        return _predicate(t);
    }
}
