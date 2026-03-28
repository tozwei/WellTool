using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 数字转换器
    /// </summary>
    public class NumberConverter : IConverter
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

            // 处理布尔值
            if (value is bool boolValue)
            {
                int boolIntValue = boolValue ? 1 : 0;
                if (targetType == typeof(int))
                {
                    return boolIntValue;
                }
                else if (targetType == typeof(long))
                {
                    return (long)boolIntValue;
                }
                else if (targetType == typeof(float))
                {
                    return (float)boolIntValue;
                }
                else if (targetType == typeof(double))
                {
                    return (double)boolIntValue;
                }
                else if (targetType == typeof(decimal))
                {
                    return (decimal)boolIntValue;
                }
                else if (targetType == typeof(byte))
                {
                    return (byte)boolIntValue;
                }
                else if (targetType == typeof(sbyte))
                {
                    return (sbyte)boolIntValue;
                }
                else if (targetType == typeof(short))
                {
                    return (short)boolIntValue;
                }
                else if (targetType == typeof(ushort))
                {
                    return (ushort)boolIntValue;
                }
                else if (targetType == typeof(uint))
                {
                    return (uint)boolIntValue;
                }
                else if (targetType == typeof(ulong))
                {
                    return (ulong)boolIntValue;
                }
            }

            string strValue = value.ToString().Trim();

            try
            {
                if (targetType == typeof(int))
                {
                    if (strValue.Contains("."))
                    {
                        return (int)decimal.Parse(strValue);
                    }
                    return int.Parse(strValue);
                }
                else if (targetType == typeof(long))
                {
                    if (strValue.Contains("."))
                    {
                        return (long)decimal.Parse(strValue);
                    }
                    return long.Parse(strValue);
                }
                else if (targetType == typeof(float))
                {
                    return float.Parse(strValue);
                }
                else if (targetType == typeof(double))
                {
                    return double.Parse(strValue);
                }
                else if (targetType == typeof(decimal))
                {
                    return decimal.Parse(strValue);
                }
                else if (targetType == typeof(byte))
                {
                    if (strValue.Contains("."))
                    {
                        return (byte)decimal.Parse(strValue);
                    }
                    return byte.Parse(strValue);
                }
                else if (targetType == typeof(sbyte))
                {
                    if (strValue.Contains("."))
                    {
                        return (sbyte)decimal.Parse(strValue);
                    }
                    return sbyte.Parse(strValue);
                }
                else if (targetType == typeof(short))
                {
                    if (strValue.Contains("."))
                    {
                        return (short)decimal.Parse(strValue);
                    }
                    return short.Parse(strValue);
                }
                else if (targetType == typeof(ushort))
                {
                    if (strValue.Contains("."))
                    {
                        return (ushort)decimal.Parse(strValue);
                    }
                    return ushort.Parse(strValue);
                }
                else if (targetType == typeof(uint))
                {
                    if (strValue.Contains("."))
                    {
                        return (uint)decimal.Parse(strValue);
                    }
                    return uint.Parse(strValue);
                }
                else if (targetType == typeof(ulong))
                {
                    if (strValue.Contains("."))
                    {
                        return (ulong)decimal.Parse(strValue);
                    }
                    return ulong.Parse(strValue);
                }
            }
            catch (Exception ex)
            {
                throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}: {ex.Message}", ex);
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>默认值</returns>
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(uint), typeof(ulong), typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(uint), typeof(ulong) };
        }
    }
}