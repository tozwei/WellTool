using System;
using WellTool.Core.Convert;
using System.Collections.Generic;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 枚举转换�?    /// </summary>
    public class EnumConverter : IConverter
    {
        /// <summary>
        /// 转换�?        /// </summary>
        /// <param name="value">要转换的�?/param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的�?/returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                throw new ConvertException("Cannot convert null to enum");
            }

            if (!targetType.IsEnum)
            {
                throw new ConvertException($"Target type {targetType.Name} is not an enum");
            }

            if (value is string stringValue)
            {
                return Enum.Parse(targetType, stringValue);
            }
            else if (value is int intValue)
            {
                return Enum.ToObject(targetType, intValue);
            }
            else if (value is long longValue)
            {
                return Enum.ToObject(targetType, longValue);
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to enum");
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(int), typeof(long) };
        }

        /// <summary>
        /// 获取支持的目标类�?        /// </summary>
        /// <returns>支持的目标类型数�?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Enum) };
        }
    }
}