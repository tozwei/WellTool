using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// Optional转换器
    /// </summary>
    public class OptionalConverter : IConverter
    {
        private readonly Type _elementType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elementType">Optional内部类型</param>
        public OptionalConverter(Type elementType)
        {
            _elementType = elementType;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                // 返回空的Optional
                var optionalType = typeof(Optional<>).MakeGenericType(_elementType);
                return Activator.CreateInstance(optionalType);
            }

            // 如果已经是Optional类型，直接返回
            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(Optional<>))
            {
                return value;
            }

            // 转换值并包装为Optional
            var convertedValue = WellTool.Core.Converter.Converter.To(value, _elementType);
            var optionalType2 = typeof(Optional<>).MakeGenericType(_elementType);
            return Activator.CreateInstance(optionalType2, convertedValue);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(Optional<>).MakeGenericType(_elementType) };
        }
    }

    /// <summary>
    /// Optional类型（可空值包装）
    /// </summary>
    /// <typeparam name="T">内部类型</typeparam>
    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">值</param>
        public Optional(T value)
        {
            _value = value;
            _hasValue = true;
        }

        /// <summary>
        /// 私有构造函数创建空Optional
        /// </summary>
        private Optional()
        {
            _hasValue = false;
        }

        /// <summary>
        /// 是否有值
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// 获取值，如果为空则返回默认值
        /// </summary>
        public T Value => _hasValue ? _value : default;

        /// <summary>
        /// 获取值或默认值
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        public T OrElse(T defaultValue) => _hasValue ? _value : defaultValue;

        /// <summary>
        /// 空Optional实例
        /// </summary>
        public static Optional<T> Empty => new Optional<T>();

        /// <summary>
        /// 创建Optional
        /// </summary>
        public static Optional<T> Of(T value) => new Optional<T>(value);
    }
}
