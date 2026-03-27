using System;
using System.Collections.Generic;
using WellTool.Core.Converter.impl;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 转换器注册表
    /// </summary>
    public class ConverterRegistry
    {
        private readonly Dictionary<Type, Dictionary<Type, IConverter>> _converters;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConverterRegistry()
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
    }
}