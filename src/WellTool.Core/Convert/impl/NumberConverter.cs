using System;
using System.Collections.Generic;

namespace WellTool.Core.Convert.impl
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

            if (targetType == typeof(int))
            {
                return int.Parse(value.ToString());
            }
            else if (targetType == typeof(long))
            {
                return long.Parse(value.ToString());
            }
            else if (targetType == typeof(float))
            {
                return float.Parse(value.ToString());
            }
            else if (targetType == typeof(double))
            {
                return double.Parse(value.ToString());
            }
            else if (targetType == typeof(decimal))
            {
                return decimal.Parse(value.ToString());
            }
            else if (targetType == typeof(byte))
            {
                return byte.Parse(value.ToString());
            }
            else if (targetType == typeof(sbyte))
            {
                return sbyte.Parse(value.ToString());
            }
            else if (targetType == typeof(short))
            {
                return short.Parse(value.ToString());
            }
            else if (targetType == typeof(ushort))
            {
                return ushort.Parse(value.ToString());
            }
            else if (targetType == typeof(uint))
            {
                return uint.Parse(value.ToString());
            }
            else if (targetType == typeof(ulong))
            {
                return ulong.Parse(value.ToString());
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
            return new[] { typeof(string), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(uint), typeof(ulong) };
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