using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Convert
{
    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public static class Converter
    {
        private static readonly ConverterRegistry _registry = new ConverterRegistry();

        /// <summary>
        /// 获取转换器注册表
        /// </summary>
        public static ConverterRegistry Registry => _registry;

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的值</returns>
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的值，转换失败则返回默认值</returns>
        public static T To<T>(object value, T defaultValue)
        {
            try
            {
                return To<T>(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public static object To(object value, Type targetType)
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

            var converter = _registry.GetConverter(sourceType, targetType);
            if (converter != null)
            {
                return converter.Convert(value, targetType);
            }

            throw new ConvertException($"No converter found for converting from {sourceType.Name} to {targetType.Name}");
        }

        /// <summary>
        /// 转换对象为字符串
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的字符串</returns>
        public static string ToString(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// 转换对象为Int32
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Int32</returns>
        public static int ToInt32(object value)
        {
            return System.Convert.ToInt32(value);
        }

        /// <summary>
        /// 转换对象为Int64
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Int64</returns>
        public static long ToInt64(object value)
        {
            return System.Convert.ToInt64(value);
        }

        /// <summary>
        /// 转换字节数组为Base64字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>Base64字符串</returns>
        public static string ToBase64String(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public static object ChangeType(object value, Type targetType)
        {
            return System.Convert.ChangeType(value, targetType);
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