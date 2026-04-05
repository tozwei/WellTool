using System;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// 提供者接口
    /// </summary>
    public interface ISupplier<T>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        T Get();
    }

    /// <summary>
    /// 提供者接口（无参数）
    /// </summary>
    public interface ISupplier
    {
        /// <summary>
        /// 获取值
        /// </summary>
        object Get();
    }

    /// <summary>
    /// 提供者
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class Supplier<T> : ISupplier<T>
    {
        private readonly System.Func<T> _func;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Supplier(System.Func<T> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T Get()
        {
            return _func();
        }

        /// <summary>
        /// 转换
        /// </summary>
        public R Map<R>(System.Func<T, R> mapper)
        {
            return mapper(_func());
        }
    }

    /// <summary>
    /// 提供者扩展
    /// </summary>
    public static class SupplierExtensions
    {
        /// <summary>
        /// 创建提供者
        /// </summary>
        public static Supplier<T> ToSupplier<T>(this System.Func<T> func)
        {
            return new Supplier<T>(func);
        }

        /// <summary>
        /// 获取值，如果为null则返回默认值
        /// </summary>
        public static T GetOrDefault<T>(this Supplier<T> supplier, T defaultValue = default)
        {
            if (supplier == null)
            {
                return defaultValue;
            }
            return supplier.Get();
        }

        /// <summary>
        /// 获取值，如果为null则使用工厂方法
        /// </summary>
        public static T GetOrDefault<T>(this Supplier<T> supplier, System.Func<T> factory)
        {
            if (supplier == null)
            {
                return factory();
            }
            return supplier.Get();
        }
    }

    /// <summary>
    /// 空提供者
    /// </summary>
    public class NullSupplier<T> : ISupplier<T>
    {
        private static readonly NullSupplier<T> _instance = new NullSupplier<T>();

        public static NullSupplier<T> Instance => _instance;

        private NullSupplier() { }

        public T Get()
        {
            return default;
        }
    }
}
