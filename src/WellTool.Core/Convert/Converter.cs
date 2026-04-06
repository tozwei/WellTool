using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellTool.Core.Convert;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public static class Converter
    {


        /// <summary>
        /// 转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的值</returns>
        public static T To<T>(object value)
        {
            if (value is int && typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }
            if (value is long && typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }
            var result = To(value, typeof(T));
            if (result is bool boolValue && typeof(T) == typeof(int))
            {
                return (T)(object)(boolValue ? 1 : 0);
            }
            if (result is bool boolValueLong && typeof(T) == typeof(long))
            {
                return (T)(object)(boolValueLong ? 1L : 0L);
            }
            if (result is bool boolValueStr && typeof(T) == typeof(string))
            {
                return (T)(object)boolValueStr.ToString();
            }
            return (T)result;
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

            // 处理集合类型转换
            if (targetType.IsGenericType)
            {
                var genericTypeDefinition = targetType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(HashSet<>) || 
                    genericTypeDefinition == typeof(List<>) || 
                    genericTypeDefinition == typeof(ICollection<>) ||
                    genericTypeDefinition == typeof(IList<>))
                {
                    var elementType = targetType.GetGenericArguments()[0];
                    var list = ToList<object>(value);
                    var result = Activator.CreateInstance(targetType);
                    var addMethod = targetType.GetMethod("Add");
                    foreach (var item in list)
                    {
                        var convertedItem = To(item, elementType);
                        addMethod.Invoke(result, new object[] { convertedItem });
                    }
                    return result;
                }
            }

            var converter = ConverterRegistry.GetConverter(sourceType, targetType);
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
        public static string ToStr(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string str)
            {
                return str;
            }

            // 处理数组类型
            if (value.GetType().IsArray)
            {
                var array = (Array)value;
                var sb = new StringBuilder("[");
                for (int i = 0; i < array.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(ToStr(array.GetValue(i)));
                }
                sb.Append("]");
                return sb.ToString();
            }

            // 处理集合类型
            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                var sb = new StringBuilder("[");
                bool first = true;
                foreach (var item in enumerable)
                {
                    if (!first)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(ToStr(item));
                    first = false;
                }
                sb.Append("]");
                return sb.ToString();
            }

            return value.ToString();
        }

        /// <summary>
        /// 转换对象为Int32
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Int32</returns>
        public static int? ToInt(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is string str)
                {
                    str = str.Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    // 处理带有小数的情况
                    if (str.Contains("."))
                    {
                        if (decimal.TryParse(str, out decimal d))
                        {
                            return (int)d;
                        }
                    }
                    else if (int.TryParse(str, out int i))
                    {
                        return i;
                    }
                }
                else if (value is bool b)
                {
                    return b ? 1 : 0;
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToInt32(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为Int64
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Int64</returns>
        public static long? ToLong(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is string str)
                {
                    str = str.Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    // 处理带有小数的情况
                    if (str.Contains("."))
                    {
                        if (decimal.TryParse(str, out decimal d))
                        {
                            return (long)d;
                        }
                    }
                    else if (long.TryParse(str, out long l))
                    {
                        return l;
                    }
                }
                else if (value is bool b)
                {
                    return b ? 1 : 0;
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToInt64(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为Char
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Char</returns>
        public static char? ToChar(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is string str)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    return str[0];
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToChar(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为Decimal
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Decimal</returns>
        public static decimal? ToDecimal(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is string str)
                {
                    str = str.Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    if (decimal.TryParse(str, out decimal d))
                    {
                        return d;
                    }
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToDecimal(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为Float
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Float</returns>
        public static float? ToFloat(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is byte[] bytes)
                {
                    if (bytes.Length == 4)
                    {
                        return BitConverter.ToSingle(bytes, 0);
                    }
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToSingle(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为Double
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的Double</returns>
        public static double? ToDouble(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is IConvertible convertible)
                {
                    return convertible.ToDouble(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换对象为指定类型（静默转换）
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的值，转换失败则返回默认值</returns>
        public static T? ConvertQuietly<T>(object value, T? defaultValue = null) where T : struct
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
        /// <param name="targetType">目标类型</param>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的值</returns>
        public static object ConvertObject(Type targetType, object value)
        {
            return To(value, targetType);
        }

        /// <summary>
        /// int 转 byte
        /// </summary>
        /// <param name="value">int值</param>
        /// <returns>byte值</returns>
        public static byte IntToByte(int value)
        {
            return (byte)value;
        }

        /// <summary>
        /// byte 转 unsigned int
        /// </summary>
        /// <param name="value">byte值</param>
        /// <returns>unsigned int值</returns>
        public static int ByteToUnsignedInt(byte value)
        {
            return value & 0xFF;
        }

        /// <summary>
        /// int 转 byte 数组
        /// </summary>
        /// <param name="value">int值</param>
        /// <returns>byte数组</returns>
        public static byte[] IntToBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// byte 数组转 int
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>int值</returns>
        public static int BytesToInt(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// long 转 byte 数组
        /// </summary>
        /// <param name="value">long值</param>
        /// <returns>byte数组</returns>
        public static byte[] LongToBytes(long value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// byte 数组转 long
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>long值</returns>
        public static long BytesToLong(byte[] bytes)
        {
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// short 转 byte 数组
        /// </summary>
        /// <param name="value">short值</param>
        /// <returns>byte数组</returns>
        public static byte[] ShortToBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// byte 数组转 short
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns>short值</returns>
        public static short BytesToShort(byte[] bytes)
        {
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的列表</returns>
        public static List<T> ToList<T>(object value)
        {
            var result = new List<T>();

            if (value == null)
            {
                return result;
            }

            if (value is string str)
            {
                // 处理字符串形式的列表
                str = str.Trim('[', ']');
                if (string.IsNullOrEmpty(str))
                {
                    return result;
                }

                var items = str.Split(',');
                foreach (var item in items)
                {
                    var trimmedItem = item.Trim();
                    if (!string.IsNullOrEmpty(trimmedItem))
                    {
                        try
                        {
                            result.Add(To<T>(trimmedItem));
                        }
                        catch
                        {
                            // 忽略转换失败的项
                        }
                    }
                }
            }
            else if (value is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    try
                    {
                        result.Add(To<T>(item));
                    }
                    catch
                    {
                        // 忽略转换失败的项
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的日期</returns>
        public static DateTime? ToDate(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                if (value is string str)
                {
                    str = str.Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        return null;
                    }
                    // 检查日期字符串格式是否完整（至少包含年月日）
                    if (str.Contains("-") && str.Split('-').Length < 3)
                    {
                        return null;
                    }
                    if (DateTime.TryParse(str, out DateTime date))
                    {
                        return date;
                    }
                }
                else if (value is IConvertible convertible)
                {
                    return convertible.ToDateTime(null);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为半角字符
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的半角字符</returns>
        public static string ToDBC(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var sb = new StringBuilder(value.Length);
            foreach (char c in value)
            {
                if (c == 12288) // 全角空格
                {
                    sb.Append(' ');
                }
                else if (c >= 65281 && c <= 65374) // 全角字符
                {
                    sb.Append((char)(c - 65248));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转换为全角字符
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的全角字符</returns>
        public static string ToSBC(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var sb = new StringBuilder(value.Length);
            foreach (char c in value)
            {
                if (c == ' ') // 半角空格
                {
                    sb.Append((char)12288);
                }
                else if (c >= 33 && c <= 126) // 半角字符
                {
                    sb.Append((char)(c + 65248));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="value">数字</param>
        /// <returns>转换后的中文</returns>
        public static string DigitToChinese(decimal? value)
        {
            if (value == null)
            {
                return "零元整";
            }

            return NumberChineseFormatter.Format((double)value, false, true);
        }

        /// <summary>
        /// 数字转中文（繁体）
        /// </summary>
        /// <param name="value">数字</param>
        /// <returns>转换后的中文</returns>
        public static string DigitToChineseTraditional(decimal? value)
        {
            if (value == null)
            {
                return "零元整";
            }

            return NumberChineseFormatter.Format((double)value, true, true);
        }

        /// <summary>
        /// 中文金额转数字
        /// </summary>
        /// <param name="chineseMoney">中文金额</param>
        /// <returns>转换后的数字</returns>
        public static decimal? ChineseMoneyToNumber(string chineseMoney)
        {
            return NumberChineseFormatter.ChineseMoneyToNumber(chineseMoney);
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