using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 日期转换器
    /// </summary>
    public class DateConverter : IConverter
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
                return null;
            }

            if (value is DateTime dateTimeValue)
            {
                if (targetType == typeof(DateTime))
                {
                    return dateTimeValue;
                }
                else if (targetType == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(dateTimeValue);
                }
            }
            else if (value is DateTimeOffset dateTimeOffsetValue)
            {
                if (targetType == typeof(DateTime))
                {
                    return dateTimeOffsetValue.DateTime;
                }
                else if (targetType == typeof(DateTimeOffset))
                {
                    return dateTimeOffsetValue;
                }
            }
            else if (value is string stringValue)
            {
                if (DateTime.TryParse(stringValue, out var dateTime))
                {
                    if (targetType == typeof(DateTime))
                    {
                        return dateTime;
                    }
                    else if (targetType == typeof(DateTimeOffset))
                    {
                        return new DateTimeOffset(dateTime);
                    }
                }
            }
            else if (value is long longValue)
            {
                if (targetType == typeof(DateTime))
                {
                    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(longValue).ToLocalTime();
                }
                else if (targetType == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddMilliseconds(longValue);
                }
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(long), typeof(DateTime), typeof(DateTimeOffset) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(DateTime), typeof(DateTimeOffset) };
        }
    }
}