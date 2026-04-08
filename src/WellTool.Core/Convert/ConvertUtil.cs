namespace WellTool.Core.Convert;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WellTool.Core.Lang;
using WellTool.Core.Util;
using WellTool.Core.Converter;

/// <summary>
/// 类型转换器
/// </summary>
public class ConvertUtil
{
	/// <summary>
	/// 转换为字符串
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static string ToStr(object value, string defaultValue)
	{
		if (value == null)
		{
			return defaultValue;
		}
		if (value is bool b)
		{
			return b ? "true" : "false";
		}
		return value.ToString();
	}

	/// <summary>
	/// 转换为字符串
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static string ToStr(object value)
	{
		return ToStr(value, "");
	}

	/// <summary>
	/// 转换为int
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static int ToInt(object value, int defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is int i) return i;
		if (int.TryParse(value.ToString(), out int result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为int
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static int ToInt(object value)
	{
		return ToInt(value, 0);
	}

	/// <summary>
	/// 转换为long
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static long ToLong(object value, long defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is long l) return l;
		if (long.TryParse(value.ToString(), out long result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为long
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static long ToLong(object value)
	{
		return ToLong(value, 0);
	}

	/// <summary>
	/// 转换为double
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static double ToDouble(object value, double defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is double d) return d;
		if (double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为double
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static double ToDouble(object value)
	{
		return ToDouble(value, 0);
	}

	/// <summary>
	/// 转换为float
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static float ToFloat(object value, float defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is float f) return f;
		if (float.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out float result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为float
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static float ToFloat(object value)
	{
		return ToFloat(value, 0);
	}

	/// <summary>
	/// 转换为decimal
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static decimal ToDecimal(object value, decimal defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is decimal dec) return dec;
		if (decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为decimal
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static decimal ToDecimal(object value)
	{
		return ToDecimal(value, 0);
	}

	/// <summary>
	/// 转换为bool
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static bool ToBool(object value, bool defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is bool b) return b;
		if (value is string s)
		{
			var lower = s.ToLower();
			if (lower == "true" || lower == "yes" || lower == "ok" || lower == "1") return true;
			if (lower == "false" || lower == "no" || lower == "0") return false;
		}
		if (value is int i) return i != 0;
		return defaultValue;
	}

	/// <summary>
	/// 转换为bool
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static bool ToBool(object value)
	{
		return ToBool(value, false);
	}

	/// <summary>
	/// 转换为DateTime
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static DateTime? ToDateTime(object value, DateTime? defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is DateTime dt) return dt;
		if (DateTime.TryParse(value.ToString(), out DateTime result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为DateTime
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static DateTime? ToDateTime(object value)
	{
		return ToDateTime(value, null);
	}

	/// <summary>
	/// 转换为Enum对象
	/// </summary>
	/// <typeparam name="T">枚举类型</typeparam>
	/// <param name="value">值</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>Enum</returns>
	public static T? ToEnum<T>(object value, T? defaultValue) where T : struct, Enum
	{
		if (value == null) return defaultValue;
		if (value is T t) return t;
		if (value is string s && Enum.TryParse<T>(s, true, out T result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为Enum对象
	/// </summary>
	/// <typeparam name="T">枚举类型</typeparam>
	/// <param name="value">值</param>
	/// <returns>Enum</returns>
	public static T? ToEnum<T>(object value) where T : struct, Enum
	{
		return ToEnum<T>(value, null);
	}

	/// <summary>
	/// 转换为byte
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static byte ToByte(object value, byte defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is byte b) return b;
		if (byte.TryParse(value.ToString(), out byte result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为byte
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static byte ToByte(object value)
	{
		return ToByte(value, 0);
	}

	/// <summary>
	/// 转换为char
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static char ToChar(object value, char defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is char c) return c;
		if (value is string s && s.Length > 0) return s[0];
		if (char.TryParse(value.ToString(), out char result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为char
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static char ToChar(object value)
	{
		return ToChar(value, '\0');
	}

	/// <summary>
	/// 转换为short
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="defaultValue">转换错误时的默认值</param>
	/// <returns>结果</returns>
	public static short ToShort(object value, short defaultValue)
	{
		if (value == null) return defaultValue;
		if (value is short s) return s;
		if (short.TryParse(value.ToString(), out short result)) return result;
		return defaultValue;
	}

	/// <summary>
	/// 转换为short
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <returns>结果</returns>
	public static short ToShort(object value)
	{
		return ToShort(value, 0);
	}

	/// <summary>
	/// 字符串转换成十六进制字符串，结果为小写
	/// </summary>
	/// <param name="str">待转换的字符串</param>
	/// <param name="encoding">编码</param>
	/// <returns>16进制字符串</returns>
	public static string ToHex(string str, Encoding encoding)
	{
		if (string.IsNullOrEmpty(str)) return str;
		return WellTool.Core.Util.HexUtil.Encode(encoding.GetBytes(str));
	}

	/// <summary>
	/// byte数组转16进制串
	/// </summary>
	/// <param name="bytes">被转换的byte数组</param>
	/// <returns>转换后的值</returns>
	public static string ToHex(byte[] bytes)
	{
		if (bytes == null || bytes.Length == 0) return string.Empty;
		return WellTool.Core.Util.HexUtil.Encode(bytes);
	}

	/// <summary>
	/// Hex字符串转换为Byte值
	/// </summary>
	/// <param name="src">Byte字符串</param>
	/// <returns>byte[]</returns>
	public static byte[] HexToBytes(string src)
	{
		if (string.IsNullOrEmpty(src)) return Array.Empty<byte>();
		return WellTool.Core.Util.HexUtil.Decode(src);
	}

	/// <summary>
	/// 转换时间为毫秒
	/// </summary>
	/// <param name="sourceDuration">时长</param>
	/// <param name="sourceUnit">源单位</param>
	/// <returns>毫秒数</returns>
	public static long ToMillis(long sourceDuration, TimeUnit sourceUnit)
	{
		return sourceUnit.ToMillis(sourceDuration);
	}

	/// <summary>
	/// 原始类转为包装类，非原始类返回原类
	/// </summary>
	/// <param name="clazz">原始类</param>
	/// <returns>包装类</returns>
	public static Type Wrap(Type clazz)
	{
		return BasicTypeUtil.Wrap(clazz);
	}

	/// <summary>
	/// 包装类转为原始类，非包装类返回原类
	/// </summary>
	/// <param name="clazz">包装类</param>
	/// <returns>原始类</returns>
	public static Type Unwrap(Type clazz)
	{
		return BasicTypeUtil.Unwrap(clazz);
	}

	/// <summary>
	/// 转换为指定类型
	/// </summary>
	/// <typeparam name="T">目标类型</typeparam>
	/// <param name="value">被转换的值</param>
	/// <returns>转换后的值</returns>
	public static T To<T>(object value)
	{
		return (T)System.Convert.ChangeType(value, typeof(T));
	}

	/// <summary>
	/// 转换为指定类型
	/// </summary>
	/// <typeparam name="T">目标类型</typeparam>
	/// <param name="value">被转换的值</param>
	/// <returns>转换后的值</returns>
	public static T ConvertTo<T>(object value)
	{
		return To<T>(value);
	}

	/// <summary>
	/// 将字符串数组转换为int数组
	/// </summary>
	/// <param name="strArray">字符串数组</param>
	/// <returns>int数组</returns>
	public static int[] ToIntArray(string[] strArray)
	{
		if (strArray == null)
		{
			return Array.Empty<int>();
		}
		var result = new int[strArray.Length];
		for (int i = 0; i < strArray.Length; i++)
		{
			result[i] = ToInt(strArray[i]);
		}
		return result;
	}

	/// <summary>
	/// 将字节数组转换为Base64字符串
	/// </summary>
	/// <param name="bytes">字节数组</param>
	/// <returns>Base64字符串</returns>
	public static string ToBase64String(byte[] bytes)
	{
		return ConvertUtil.ToBase64String(bytes);
	}

	/// <summary>
	/// 将对象转换为指定类型
	/// </summary>
	/// <param name="value">被转换的值</param>
	/// <param name="targetType">目标类型</param>
	/// <returns>转换后的值</returns>
	public static object ChangeType(object value, Type targetType)
	{
		return System.Convert.ChangeType(value, targetType);
	}
}

/// <summary>
/// 时间单位
/// </summary>
public enum TimeUnit
{
	Milliseconds,
	Seconds,
	Minutes,
	Hours,
	Days
}

public static class TimeUnitExtensions
{
	public static long ToMillis(this TimeUnit unit, long duration)
	{
		switch (unit)
		{
			case TimeUnit.Milliseconds: return duration;
			case TimeUnit.Seconds: return duration * 1000;
			case TimeUnit.Minutes: return duration * 60 * 1000;
			case TimeUnit.Hours: return duration * 60 * 60 * 1000;
			case TimeUnit.Days: return duration * 24 * 60 * 60 * 1000;
			default: return duration;
		}
	}
}
