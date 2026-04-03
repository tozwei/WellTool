using System;
using System.Collections.Generic;
using WellTool.Core.Convert.impl;

namespace WellTool.Core.Convert
{
    /// <summary>
    /// 转换器注册表
    /// </summary>
    public class ConverterRegistry
    {
        private static readonly ConverterRegistry _instance = new ConverterRegistry();
        private readonly Dictionary<Type, Dictionary<Type, IConverter>> _converters;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static ConverterRegistry Instance => _instance;

        /// <summary>
        /// 构造函数
        /// </summary>
        private ConverterRegistry()
        {
            _converters = new Dictionary<Type, Dictionary<Type, IConverter>>();
            RegisterDefaultConverters();
        }

        /// <summary>
        /// 注册默认转换器
        /// </summary>
        private void RegisterDefaultConverters()
        {
            // 注册基本类型转换器
            Register(new StringConverter());
            Register(new NumberConverter());
            Register(new BooleanConverter());
            Register(new DateConverter());
            Register(new CollectionConverter());
            Register(new MapConverter());
            Register(new EnumConverter());
            Register(new ArrayConverter());
            Register(new CharacterConverter());
            Register(new ClassConverter());
            Register(new UUIDConverter());
        }

        /// <summary>
        /// 注册转换器
        /// </summary>
        /// <param name="converter">转换器</param>
        public void Register(IConverter converter)
        {
            var sourceTypes = converter.GetSupportedSourceTypes();
            var targetTypes = converter.GetSupportedTargetTypes();

            foreach (var sourceType in sourceTypes)
            {
                if (!_converters.TryGetValue(sourceType, out var targetConverters))
                {
                    targetConverters = new Dictionary<Type, IConverter>();
                    _converters[sourceType] = targetConverters;
                }

                foreach (var targetType in targetTypes)
                {
                    targetConverters[targetType] = converter;
                }
            }
        }

        /// <summary>
        /// 获取转换器
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换器</returns>
        public IConverter GetConverter(Type sourceType, Type targetType)
        {
            if (_converters.TryGetValue(sourceType, out var targetConverters))
            {
                if (targetConverters.TryGetValue(targetType, out var converter))
                {
                    return converter;
                }
            }

            // 尝试找到可以处理该类型的转换器
            foreach (var (srcType, typeConverters) in _converters)
            {
                if (srcType.IsAssignableFrom(sourceType))
                {
                    foreach (var (tgtType, converter) in typeConverters)
                    {
                        if (tgtType.IsAssignableFrom(targetType))
                        {
                            return converter;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 移除转换器
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        public void Remove(Type sourceType, Type targetType)
        {
            if (_converters.TryGetValue(sourceType, out var targetConverters))
            {
                targetConverters.Remove(targetType);
            }
        }

        /// <summary>
        /// 清空所有转换器
        /// </summary>
        public void Clear()
        {
            _converters.Clear();
        }

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的值</returns>
        public T Convert<T>(object value)
        {
            var result = Convert(value, typeof(T));
            if (result is bool boolValue && typeof(T) == typeof(int))
            {
                return (T)(object)(boolValue ? 1 : 0);
            }
            if (result is bool boolValueLong && typeof(T) == typeof(long))
            {
                return (T)(object)(boolValueLong ? 1L : 0L);
            }
            return (T)result;
        }

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return GetDefaultValue(targetType);
            }

            var sourceType = value.GetType();
            if (targetType.IsAssignableFrom(sourceType))
            {
                return value;
            }

            var converter = GetConverter(sourceType, targetType);
            if (converter != null)
            {
                return converter.Convert(value, targetType);
            }

            throw new ConvertException($"No converter found for converting from {sourceType.Name} to {targetType.Name}");
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}