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
                else if (IsDateOnlyType(targetType))
                {
                    // 尝试创建 DateOnly 实例
                    var dateOnly = CreateDateOnly(dateTimeValue);
                    if (dateOnly != null)
                    {
                        return dateOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 DateTime 作为替代
                    return dateTimeValue.Date;
                }
                else if (IsTimeOnlyType(targetType))
                {
                    // 尝试创建 TimeOnly 实例
                    var timeOnly = CreateTimeOnly(dateTimeValue);
                    if (timeOnly != null)
                    {
                        return timeOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 TimeSpan 作为替代
                    return dateTimeValue.TimeOfDay;
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
                else if (IsDateOnlyType(targetType))
                {
                    // 尝试创建 DateOnly 实例
                    var dateOnly = CreateDateOnly(dateTimeOffsetValue.DateTime);
                    if (dateOnly != null)
                    {
                        return dateOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 DateTime 作为替代
                    return dateTimeOffsetValue.DateTime.Date;
                }
                else if (IsTimeOnlyType(targetType))
                {
                    // 尝试创建 TimeOnly 实例
                    var timeOnly = CreateTimeOnly(dateTimeOffsetValue.DateTime);
                    if (timeOnly != null)
                    {
                        return timeOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 TimeSpan 作为替代
                    return dateTimeOffsetValue.DateTime.TimeOfDay;
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
                    else if (IsDateOnlyType(targetType))
                    {
                        // 尝试创建 DateOnly 实例
                        var dateOnly = CreateDateOnly(dateTime);
                        if (dateOnly != null)
                        {
                            return dateOnly;
                        }
                        // 为了兼容 .NET Standard 2.1，使用 DateTime 作为替代
                        return dateTime.Date;
                    }
                    else if (IsTimeOnlyType(targetType))
                    {
                        // 尝试创建 TimeOnly 实例
                        var timeOnly = CreateTimeOnly(dateTime);
                        if (timeOnly != null)
                        {
                            return timeOnly;
                        }
                        // 为了兼容 .NET Standard 2.1，使用 TimeSpan 作为替代
                        return dateTime.TimeOfDay;
                    }
                }
            }
            else if (value is long longValue)
            {
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(longValue).ToLocalTime();
                if (targetType == typeof(DateTime))
                {
                    return dateTime;
                }
                else if (targetType == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(dateTime);
                }
                else if (IsDateOnlyType(targetType))
                {
                    // 尝试创建 DateOnly 实例
                    var dateOnly = CreateDateOnly(dateTime);
                    if (dateOnly != null)
                    {
                        return dateOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 DateTime 作为替代
                    return dateTime.Date;
                }
                else if (IsTimeOnlyType(targetType))
                {
                    // 尝试创建 TimeOnly 实例
                    var timeOnly = CreateTimeOnly(dateTime);
                    if (timeOnly != null)
                    {
                        return timeOnly;
                    }
                    // 为了兼容 .NET Standard 2.1，使用 TimeSpan 作为替代
                    return dateTime.TimeOfDay;
                }
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 尝试创建 DateOnly 实例
        /// </summary>
        /// <param name="dateTime">DateTime 值</param>
        /// <returns>DateOnly 实例或 null</returns>
        private object CreateDateOnly(DateTime dateTime)
        {
            try
            {
                var dateOnlyType = Type.GetType("System.DateOnly");
                if (dateOnlyType != null)
                {
                    // 尝试调用 DateOnly.FromDateTime 方法
                    var fromDateTimeMethod = dateOnlyType.GetMethod("FromDateTime", new[] { typeof(DateTime) });
                    if (fromDateTimeMethod != null)
                    {
                        return fromDateTimeMethod.Invoke(null, new object[] { dateTime });
                    }
                }
            }
            catch
            {
                // 忽略异常，返回 null
            }
            return null;
        }

        /// <summary>
        /// 尝试创建 TimeOnly 实例
        /// </summary>
        /// <param name="dateTime">DateTime 值</param>
        /// <returns>TimeOnly 实例或 null</returns>
        private object CreateTimeOnly(DateTime dateTime)
        {
            try
            {
                var timeOnlyType = Type.GetType("System.TimeOnly");
                if (timeOnlyType != null)
                {
                    // 尝试调用 TimeOnly.FromDateTime 方法
                    var fromDateTimeMethod = timeOnlyType.GetMethod("FromDateTime", new[] { typeof(DateTime) });
                    if (fromDateTimeMethod != null)
                    {
                        return fromDateTimeMethod.Invoke(null, new object[] { dateTime });
                    }
                }
            }
            catch
            {
                // 忽略异常，返回 null
            }
            return null;
        }

        /// <summary>
        /// 检查是否为 DateOnly 类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为 DateOnly 类型</returns>
        private bool IsDateOnlyType(Type type)
        {
            return type.Name == "DateOnly";
        }

        /// <summary>
        /// 检查是否为 TimeOnly 类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否为 TimeOnly 类型</returns>
        private bool IsTimeOnlyType(Type type)
        {
            return type.Name == "TimeOnly";
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
            var targetTypes = new List<Type> { typeof(DateTime), typeof(DateTimeOffset) };
            
            // 尝试添加 DateOnly 和 TimeOnly 类型，如果它们存在
            var dateOnlyType = Type.GetType("System.DateOnly");
            if (dateOnlyType != null)
            {
                targetTypes.Add(dateOnlyType);
            }
            
            var timeOnlyType = Type.GetType("System.TimeOnly");
            if (timeOnlyType != null)
            {
                targetTypes.Add(timeOnlyType);
            }
            
            return targetTypes.ToArray();
        }
    }
}