using System;
using WellTool.Core.Convert;
using System.Globalization;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鏁板瓧杞崲锟?    /// </summary>
    public class NumberConverter : IConverter
    {
        private readonly Type _targetType;

        /// <summary>
        /// 鏋勯€犲嚱锟?        /// </summary>
        public NumberConverter() : this(typeof(double))
        {
        }

        /// <summary>
        /// 鏋勯€犲嚱锟?        /// </summary>
        /// <param name="targetType">鐩爣鏁板瓧绫诲瀷</param>
        public NumberConverter(Type targetType)
        {
            _targetType = targetType;
        }

        /// <summary>
        /// 杞崲锟?        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return GetDefaultValue(_targetType);
            }

            // 澶勭悊宸茬粡鏄洰鏍囩被锟?            if (_targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            var str = value.ToString().Trim();

            // 澶勭悊绌哄瓧绗︿覆
            if (string.IsNullOrEmpty(str))
            {
                return GetDefaultValue(_targetType);
            }

            // 澶勭悊绉戝璁℃暟锟?            if (str.Contains("e") || str.Contains("E"))
            {
                if (decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal d))
                {
                    return ChangeType(d, _targetType);
                }
            }

            // 鍏堝皾璇曡浆鎹负 decimal 浣滀负涓棿绫诲瀷
            if (!decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                // 灏濊瘯鍘婚櫎璐у竵绗﹀彿鍜屽叾浠栭潪鏁板瓧瀛楃
                var cleanedStr = CleanNumberString(str);
                if (!decimal.TryParse(cleanedStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue))
                {
                    return GetDefaultValue(_targetType);
                }
            }

            return ChangeType(decimalValue, _targetType);
        }

        /// <summary>
        /// 娓呯悊鏁板瓧瀛楃锟?        /// </summary>
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
        /// 杞崲涓虹洰鏍囨暟瀛楃被锟?        /// </summary>
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
                    return System.Convert.ChangeType(value, targetType);
            }
        }

        /// <summary>
        /// 鑾峰彇榛樿锟?        /// </summary>
        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                               typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float),
                               typeof(double), typeof(decimal) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被锟?        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                               typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float),
                               typeof(double), typeof(decimal) };
        }
    }
}

