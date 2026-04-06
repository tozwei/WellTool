using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// Dictionary扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <returns>字符串值</returns>
        public static string GetStr(this Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value))
            {
                return value?.ToString();
            }
            return null;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <returns>布尔值</returns>
        public static bool? GetBool(this Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value))
            {
                if (value is bool boolValue)
                {
                    return boolValue;
                }
                else if (value is string strValue)
                {
                    bool.TryParse(strValue, out var result);
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取长整数值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <returns>长整数值</returns>
        public static long? GetLong(this Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value))
            {
                if (value is long longValue)
                {
                    return longValue;
                }
                else if (value is int intValue)
                {
                    return intValue;
                }
                else if (value is string strValue)
                {
                    long.TryParse(strValue, out var result);
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取双精度浮点数值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <returns>双精度浮点数值</returns>
        public static double? GetDouble(this Dictionary<string, object> dict, string key)
        {
            if (dict.TryGetValue(key, out var value))
            {
                if (value is double doubleValue)
                {
                    return doubleValue;
                }
                else if (value is float floatValue)
                {
                    return floatValue;
                }
                else if (value is int intValue)
                {
                    return intValue;
                }
                else if (value is long longValue)
                {
                    return longValue;
                }
                else if (value is string strValue)
                {
                    double.TryParse(strValue, out var result);
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set(this Dictionary<string, object> dict, string key, object value)
        {
            dict[key] = value;
        }
    }
}