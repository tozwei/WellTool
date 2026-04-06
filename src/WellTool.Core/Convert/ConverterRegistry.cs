using System;
using System.Collections.Generic;

namespace WellTool.Core.Convert
{
    /// <summary>
    /// 转换器注册表
    /// </summary>
    public static class ConverterRegistry
    {
        private static readonly Dictionary<Type, Dictionary<Type, IConverter>> _converters = new Dictionary<Type, Dictionary<Type, IConverter>>();

        /// <summary>
        /// 注册转换器
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="converter">转换器</param>
        public static void Register(Type sourceType, Type targetType, IConverter converter)
        {
            if (!_converters.ContainsKey(sourceType))
            {
                _converters[sourceType] = new Dictionary<Type, IConverter>();
            }
            _converters[sourceType][targetType] = converter;
        }

        /// <summary>
        /// 注册转换器（泛型版本）
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="converter">转换函数</param>
        public static void Register<TSource, TTarget>(Func<TSource, TTarget> converter)
        {
            Register(typeof(TSource), typeof(TTarget), new FuncConverter<TSource, TTarget>(converter));
        }

        /// <summary>
        /// 检查是否包含转换器
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <returns>是否包含</returns>
        public static bool Contains<TSource, TTarget>()
        {
            return GetConverter(typeof(TSource), typeof(TTarget)) != null;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>转换后的值</returns>
        public static TTarget Convert<TSource, TTarget>(TSource value) where TTarget : class
        {
            var converter = GetConverter(typeof(TSource), typeof(TTarget)) as IConverter<TSource, TTarget>;
            return converter != null ? converter.Convert(value) : default(TTarget);
        }

        /// <summary>
        /// 获取转换器
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <returns>转换器</returns>
        public static IConverter<TSource, TTarget> GetConverter<TSource, TTarget>()
        {
            var converter = GetConverter(typeof(TSource), typeof(TTarget));
            return converter as IConverter<TSource, TTarget>;
        }

        /// <summary>
        /// 获取转换器
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换器</returns>
        public static IConverter GetConverter(Type sourceType, Type targetType)
        {
            if (_converters.TryGetValue(sourceType, out var targetConverters) && targetConverters.TryGetValue(targetType, out var converter))
            {
                return converter;
            }
            return null;
        }

        /// <summary>
        /// 清除所有转换器
        /// </summary>
        public static void Clear()
        {
            _converters.Clear();
        }
    }

    /// <summary>
    /// 转换器接口（泛型版本）
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    public interface IConverter<TSource, TTarget>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>转换后的值</returns>
        TTarget Convert(TSource value);
    }

    /// <summary>
    /// 函数转换器
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    internal class FuncConverter<TSource, TTarget> : IConverter<TSource, TTarget>, IConverter
    {
        private readonly Func<TSource, TTarget> _func;

        public FuncConverter(Func<TSource, TTarget> func)
        {
            _func = func;
        }

        public TTarget Convert(TSource value) => _func(value);

        public object Convert(object value, Type targetType)
        {
            if (value is TSource source)
            {
                return _func(source);
            }
            return default(TTarget);
        }

        public Type[] GetSupportedSourceTypes() => new[] { typeof(TSource) };

        public Type[] GetSupportedTargetTypes() => new[] { typeof(TTarget) };
    }

    /// <summary>
    /// 转换器基类
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    public abstract class Converter<TSource, TTarget> : IConverter, IConverter<TSource, TTarget>
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>转换后的值</returns>
        public abstract TTarget Convert(TSource value);

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public object Convert(object value, Type targetType)
        {
            if (value is TSource source)
            {
                return Convert(source);
            }
            return default(TTarget);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(TSource) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(TTarget) };
        }
    }
}