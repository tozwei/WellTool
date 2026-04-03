using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// 内部JSON工具类，仅用于JSON内部使用
    /// </summary>
    internal static class InternalJSONUtil
    {
        /// <summary>
        /// 如果对象是Number 且是 NaN or infinite，将抛出异常
        /// </summary>
        /// <param name="obj">被检查的对象</param>
        /// <returns>检测后的值</returns>
        internal static object TestValidity(object obj)
        {
            if (!IsValidNumber(obj))
            {
                throw new JSONException("JSON does not allow non-finite numbers.");
            }
            return obj;
        }

        /// <summary>
        /// 检查数值是否有效
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否有效</returns>
        private static bool IsValidNumber(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            if (obj is double d)
            {
                return !double.IsNaN(d) && !double.IsInfinity(d);
            }
            if (obj is float f)
            {
                return !float.IsNaN(f) && !float.IsInfinity(f);
            }
            if (obj is decimal dec)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 值转为String，用于JSON中
        /// </summary>
        /// <param name="value">需要转为字符串的对象</param>
        /// <returns>字符串</returns>
        internal static string ValueToString(object value)
        {
            if (value == null || value is JSONNull)
            {
                return JSONNull.NULL.ToString();
            }
            if (value is JSONString jsonString)
            {
                return jsonString.ToJSONString();
            }
            if (value is IConvertible convertible)
            {
                if (value is sbyte || value is byte || value is short || value is ushort ||
                    value is int || value is uint || value is long || value is ulong ||
                    value is float || value is double || value is decimal ||
                    value is BigInteger)
                {
                    return convertible.ToString(NumberFormatInfo.InvariantInfo);
                }
            }
            if (value is bool || value is JSONObject || value is JSONArray)
            {
                return value.ToString();
            }
            if (value is Dictionary<string, object> map)
            {
                return new JSONObject(map).ToString();
            }
            if (value is List<object> list)
            {
                return new JSONArray(list).ToString();
            }
            if (value is object[] array)
            {
                return new JSONArray(array).ToString();
            }
            return JSONUtil.Quote(value.ToString());
        }

        /// <summary>
        /// 尝试转换字符串为number, boolean, or null，无法转换返回String
        /// </summary>
        /// <param name="str">A String</param>
        /// <returns>A simple JSON value</returns>
        public static object StringToValue(string str)
        {
            // null处理
            if (string.IsNullOrEmpty(str) || "null".Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return JSONNull.NULL;
            }

            // boolean处理
            if ("true".Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if ("false".Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Number处理
            char b = str[0];
            if ((b >= '0' && b <= '9') || b == '-')
            {
                try
                {
                    if (str.Contains(".") || str.Contains("e") || str.Contains("E"))
                    {
                        // Double会出现小数精度丢失问题，此处使用decimal
                        return decimal.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        long myLong = long.Parse(str, CultureInfo.InvariantCulture);
                        if (str == myLong.ToString(CultureInfo.InvariantCulture))
                        {
                            if (myLong >= int.MinValue && myLong <= int.MaxValue)
                            {
                                return (int)myLong;
                            }
                            return myLong;
                        }
                    }
                }
                catch
                {
                    // 解析失败，返回原字符串
                }
            }

            // 其它情况返回原String值
            return str;
        }

        /// <summary>
        /// 将Property的键转化为JSON形式
        /// </summary>
        /// <param name="jsonObject">JSONObject</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>JSONObject</returns>
        internal static JSONObject PropertyPut(JSONObject jsonObject, string key, object value)
        {
            var path = key.Split('.');
            int last = path.Length - 1;
            JSONObject target = jsonObject;
            for (int i = 0; i < last; i++)
            {
                string segment = path[i];
                JSONObject nextTarget = target.GetJSONObject(segment);
                if (nextTarget == null)
                {
                    nextTarget = new JSONObject(target.Config);
                    target.Set(segment, nextTarget, target.Config.IsCheckDuplicate());
                }
                target = nextTarget;
            }
            target.Set(path[last], value, target.Config.IsCheckDuplicate());
            return jsonObject;
        }

        /// <summary>
        /// 默认情况下是否忽略null值的策略选择
        /// </summary>
        /// <param name="obj">需要检查的对象</param>
        /// <returns>是否忽略null值</returns>
        internal static bool DefaultIgnoreNullValue(object obj)
        {
            return !(obj is string) && !(obj is JSONTokener) && !(obj is Dictionary<string, object>);
        }
    }
}
