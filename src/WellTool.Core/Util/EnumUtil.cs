using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// 获取枚举的所有值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举值数组</returns>
        public static T[] GetValues<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        /// <summary>
        /// 获取枚举的所有名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举名称数组</returns>
        public static string[] GetNames<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// 将字符串转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串值</param>
        /// <returns>枚举值</returns>
        public static T ToEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 将字符串转换为枚举（忽略大小写）
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串值</param>
        /// <returns>枚举值</returns>
        public static T ToEnumIgnoreCase<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// 将整数转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">整数值</param>
        /// <returns>枚举值</returns>
        public static T ToEnum<T>(int value) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        /// <summary>
        /// 检查字符串是否是有效的枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">字符串值</param>
        /// <returns>是否有效</returns>
        public static bool IsValidEnum<T>(string value) where T : Enum
        {
            return Enum.TryParse(typeof(T), value, out _);
        }

        /// <summary>
        /// 检查整数是否是有效的枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">整数值</param>
        /// <returns>是否有效</returns>
        public static bool IsValidEnum<T>(int value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns>描述</returns>
        public static string GetDescription<T>(T enumValue) where T : Enum
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = fieldInfo?.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description;
            }
            return enumValue.ToString();
        }

        /// <summary>
        /// 获取枚举的所有值和描述
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举值和描述的字典</returns>
        public static Dictionary<T, string> GetEnumMap<T>() where T : Enum
        {
            var map = new Dictionary<T, string>();
            var values = GetValues<T>();
            foreach (var value in values)
            {
                map[value] = GetDescription(value);
            }
            return map;
        }

        /// <summary>
        /// 获取枚举的名称和值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举名称和值的字典</returns>
        public static Dictionary<string, T> GetNameValueMap<T>() where T : Enum
        {
            var map = new Dictionary<string, T>();
            var values = GetValues<T>();
            foreach (var value in values)
            {
                map[value.ToString()] = value;
            }
            return map;
        }

        /// <summary>
        /// 获取枚举的整数值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns>整数值</returns>
        public static int GetIntValue<T>(T enumValue) where T : Enum
        {
            return Convert.ToInt32(enumValue);
        }

        /// <summary>
        /// 获取枚举的长整数值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumValue">枚举值</param>
        /// <returns>长整数值</returns>
        public static long GetLongValue<T>(T enumValue) where T : Enum
        {
            return Convert.ToInt64(enumValue);
        }
    }
}