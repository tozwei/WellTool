using System;
using WellTool.Core.Convert;
using System.Globalization;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 数字转换�?    /// </summary>
    public class NumberConverter : IConverter
    {
        private readonly Type _targetType;

        /// <summary>
        /// 构造函�?        /// </summary>
        /// <param name="targetType">目标数字类型</param>
        public NumberConverter(Type targetType)
        {
            _targetType = targetType;
        }

        /// <summary>
        /// 转换�?        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return GetDefaultValue(_targetType);
            }

            // 处理已经是目标类�?            if (_targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            var str = value.ToString().Trim();

            // 处理空字符串
            if (string.IsNullOrEmpty(str))
            {
                return GetDefaultValue(_targetType);
            }

            // 处理科学计数�?            if (str.Contains("e") || str.Contains("E"))
            {
                if (decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal d))
                {
                    return ChangeType(d, _targetType);
                }
            }

            // 先尝试转换为 decimal 作为中间类型
            if (!decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                // 尝试去除货币符号和其他非数字字符
                var cleanedStr = CleanNumberString(str);
                if (!decimal.TryParse(cleanedStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue))
                {
                    return GetDefaultValue(_targetType);
                }
            }

            return ChangeType(decimalValue, _targetType);
        }

        /// <summary>
        /// 清理数字字符�?        /// </summary>
        private static string CleanNumberString(string str)
        {
            var result = new System.Text.StringBuilder();
            bool hasDecimal = false;

            foreach (char c in str)
            {
                if (char.IsDigit(c) || c == '-' || c == '+')
                {
                    result.Append(c);
                }
                else if (c == '.' && !hasDecimal)
                {
                    result.Append(c);
                    hasDecimal = true;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 转换为目标数字类�?        /// </summary>
        private static object ChangeType(decimal value, Type targetType)
        {
            var typeCode = Type.GetTypeCode(targetType);

            switch (typeCode)
            {
                case TypeCode.Byte:
                    return (byte)value;
                case TypeCode.SByte:
                    return (sbyte)value;
                case TypeCode.Int16:
                    return (short)value;
                case TypeCode.UInt16:
                    return (ushort)value;
                case TypeCode.Int32:
                    return (int)value;
                case TypeCode.UInt32:
                    return (uint)value;
                case TypeCode.Int64:
                    return (long)value;
                case TypeCode.UInt64:
                    return (ulong)value;
                case TypeCode.Single:
                    return (float)value;
                case TypeCode.Double:
                    return (double)value;
                case TypeCode.Decimal:
                    return value;
                default:
                    return Convert.ChangeType(value, targetType);
            }
        }

        /// <summary>
        /// 获取默认�?        /// </summary>
        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                               typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float),
                               typeof(double), typeof(decimal) };
        }

        /// <summary>
        /// 获取支持的目标类�?        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                               typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float),
                               typeof(double), typeof(decimal) };
        }
    }
}
