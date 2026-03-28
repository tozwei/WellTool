using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 布尔值转换器
    /// </summary>
    public class BooleanConverter : IConverter
    {
        /// <summary>
        /// 转换值
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

            if (value is bool boolValue)
            {
                if (targetType == typeof(bool))
                {
                    return boolValue;
                }
                else if (targetType == typeof(string))
                {
                    return boolValue.ToString();
                }
            }

            if (value is string stringValue)
            {
                stringValue = stringValue.Trim().ToLower();
                return stringValue == "true" || stringValue == "1" || stringValue == "yes" || stringValue == "y";
            }

            if (value is int intValue)
            {
                return intValue != 0;
            }

            if (value is long longValue)
            {
                return longValue != 0;
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(int), typeof(long), typeof(bool) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(bool), typeof(string) };
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        private static object GetDefaultValue(Type type)
        {
            if (type == typeof(bool))
            {
                return false;
            }
            else if (type == typeof(int))
            {
                return 0;
            }
            else if (type == typeof(long))
            {
                return 0L;
            }
            else if (type == typeof(string))
            {
                return null;
            }
            return null;
        }
    }
}