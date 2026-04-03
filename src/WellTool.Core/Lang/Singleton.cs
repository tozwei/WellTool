namespace WellTool.Core.Lang;

/// <summary>
/// 单例模式工具类
/// </summary>
public class Singleton
{
    /// <summary>
    /// 获取指定类型的单例实例
    /// </summary>
    public static T GetInstance<T>() where T : class, new()
    {
        return Nested<T>.Instance;
    }

    /// <summary>
    /// 获取指定类型的单例实例，支持工厂方法
    /// </summary>
    public static T GetInstance<T>(System.Func<T> factory) where T : class
    {
        return Nested<T>.GetInstance(factory);
    }

    private static class Nested<T> where T : class, new()
    {
        internal static readonly T Instance = new T();
        private static T? _customInstance;
        private static readonly object Lock = new object();

        internal static T GetInstance(System.Func<T> factory)
        {
            if (_customInstance == null)
            {
                lock (Lock)
                {
                    if (_customInstance == null)
                    {
                        _customInstance = factory();
                    }
                }
            }
            return _customInstance;
        }
    }
}

/// <summary>
/// 单例类基类
/// </summary>
/// <typeparam name="T">单例类型</typeparam>
public abstract class SingletonClass<T> where T : class, new()
{
    /// <summary>
    /// 获取实例
    /// </summary>
    public static T Instance => Nested.Instance;

    private static class Nested
    {
        internal static readonly T Instance = new T();
    }
}
