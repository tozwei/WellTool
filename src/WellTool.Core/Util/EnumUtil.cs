using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Util;

/// <summary>
/// 枚举工具类
/// </summary>
public static class EnumUtil
{
    /// <summary>
    /// 获取枚举的所有值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <returns>枚举的所有值</returns>
    public static T[] GetValues<T>() where T : struct, Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }

    /// <summary>
    /// 获取枚举的所有名称
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <returns>枚举的所有名称</returns>
    public static string[] GetNames<T>() where T : struct, Enum
    {
        return Enum.GetNames(typeof(T));
    }

    /// <summary>
    /// 将字符串转换为枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">字符串值</param>
    /// <returns>枚举值</returns>
    public static T Parse<T>(string value) where T : struct, Enum
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary>
    /// 将字符串转换为枚举，失败时返回默认值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">字符串值</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>枚举值</returns>
    public static T Parse<T>(string value, T defaultValue) where T : struct, Enum
    {
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        if (Enum.TryParse(value, out T result))
        {
            return result;
        }

        return defaultValue;
    }

    /// <summary>
    /// 检查枚举是否包含指定的值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">要检查的值</param>
    /// <returns>是否包含</returns>
    public static bool IsDefined<T>(T value) where T : struct, Enum
    {
        return Enum.IsDefined(typeof(T), value);
    }

    /// <summary>
    /// 检查字符串是否是有效的枚举值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">字符串值</param>
    /// <returns>是否有效</returns>
    public static bool IsDefined<T>(string value) where T : struct, Enum
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return Enum.TryParse<T>(value, out _);
    }
}
