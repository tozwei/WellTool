using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Convert
{
    /// <summary>
    /// 抽象转换器，提供通用的转换逻辑，同时通过convertInternal实现对应类型的专属逻辑
    /// 转换器不会抛出转换异常，转换失败时会返回null
    /// </summary>
    /// <typeparam name="T">转换的目标类型</typeparam>
    public abstract class AbstractConverter<T> : IConverter
    {
        private static readonly long serialVersionUID = 1L;

        /// <summary>
        /// 不抛异常转换
        /// 当转换失败时返回默认值
        /// </summary>
        /// <param name="value">被转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的值</returns>
        public T ConvertQuietly(object value, T defaultValue)
        {
            try
            {
                return Convert(value, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的值</returns>
        public virtual T Convert(object value)
        {
            return Convert(value, default(T));
        }

        /// <summary>
        /// 转换值，当转换失败时返回默认值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的值</returns>
        public virtual T Convert(object value, T defaultValue)
        {
            var targetType = GetTargetType();
            if (targetType == null && defaultValue == null)
            {
                throw new ArgumentException($"[type] and [defaultValue] are both null for Converter [{GetType().Name}], we can not know what type to convert!");
            }
            if (targetType == null)
            {
                targetType = defaultValue.GetType();
            }

            if (value == null)
            {
                return defaultValue;
            }

            // 如果值已经是目标类型，不需要转换
            if (targetType.IsInstanceOfType(value) && targetType != typeof(Dictionary<string, object>))
            {
                return (T)value;
            }

            var result = ConvertInternal(value);
            return result != null ? result : defaultValue;
        }

        /// <summary>
        /// 内部转换器，被 Convert 方法调用，实现基本转换逻辑
        /// 内部转换器转换后如果转换失败可以做如下操作，处理结果都为返回默认值：
        /// 1、返回null
        /// 2、抛出一个RuntimeException异常
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>转换后的类型</returns>
        protected abstract T ConvertInternal(object value);

        /// <summary>
        /// 值转为String，用于内部转换中需要使用String中转的情况
        /// 转换规则为：
        /// 1、字符串类型将被强转
        /// 2、数组将被转换为逗号分隔的字符串
        /// 3、其它类型将调用默认的toString()方法
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>String</returns>
        protected string ConvertToStr(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string str)
            {
                return str;
            }

            if (value.GetType().IsArray)
            {
                var array = (Array)value;
                var elements = new List<string>();
                foreach (var item in array)
                {
                    elements.Add(ConvertToStr(item));
                }
                return string.Join(", ", elements);
            }

            return value.ToString();
        }

        /// <summary>
        /// 获得此类实现类的泛型类型
        /// </summary>
        /// <returns>此类的泛型类型，可能为null</returns>
        public virtual Type GetTargetType()
        {
            var baseType = GetType();
            while (baseType != null && baseType != typeof(object))
            {
                var genericArgs = baseType.GetGenericArguments();
                if (genericArgs.Length > 0)
                {
                    return genericArgs[0];
                }
                baseType = baseType.BaseType;
            }
            return null;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        object IConverter.Convert(object value, Type targetType)
        {
            return Convert(value);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public virtual Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public virtual Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(T) };
        }
    }
}