namespace WellTool.Core.Util;

/// <summary>
/// 对象工具类
/// </summary>
public static class ObjUtil
{
	/// <summary>
	/// 判断对象是否为空
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>是否为空</returns>
	public static bool IsEmpty(object obj)
	{
		if (obj == null)
		{
			return true;
		}
		if (obj is string str)
		{
			return string.IsNullOrEmpty(str);
		}
		if (obj is System.Collections.ICollection collection)
		{
			return collection.Count == 0;
		}
		return false;
	}

	/// <summary>
	/// 判断对象是否不为空
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>是否不为空</returns>
	public static bool IsNotEmpty(object obj)
	{
		return !IsEmpty(obj);
	}

	/// <summary>
	/// 判断两个对象是否相等
	/// </summary>
	/// <param name="a">对象A</param>
	/// <param name="b">对象B</param>
	/// <returns>是否相等</returns>
	public static bool Equals(object a, object b)
	{
		return Equals(a, b, false);
	}

	/// <summary>
	/// 判断两个对象是否相等
	/// </summary>
	/// <param name="a">对象A</param>
	/// <param name="b">对象B</param>
	/// <param name="isNullSafe">是否空安全比较</param>
	/// <returns>是否相等</returns>
	public static bool Equals(object a, object b, bool isNullSafe)
	{
		if (isNullSafe)
		{
			if (a == null && b == null) return true;
			if (a == null || b == null) return false;
		}
		return a?.Equals(b) ?? (b == null);
	}

	/// <summary>
	/// 获取对象的字符串表示
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>字符串表示</returns>
	public static string ToString(object obj)
	{
		return obj?.ToString() ?? "null";
	}

	/// <summary>
	/// 获取对象的哈希码
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>哈希码</returns>
	public static int GetHashCode(object obj)
	{
		return obj?.GetHashCode() ?? 0;
	}

	/// <summary>
	/// 获取对象类型
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>类型</returns>
	public static System.Type GetType(object obj)
	{
		return obj?.GetType();
	}

	/// <summary>
	/// 判断对象是否为值类型
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>是否为值类型</returns>
	public static bool IsValueType(object obj)
	{
		return obj?.GetType().IsValueType ?? false;
	}

	/// <summary>
	/// 判断对象是否为字符串
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>是否为字符串</returns>
	public static bool IsString(object obj)
	{
		return obj is string;
	}

	/// <summary>
	/// 判断对象是否为数字
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>是否为数字</returns>
	public static bool IsNumber(object obj)
	{
		if (obj == null) return false;
		return obj is sbyte || obj is byte ||
			   obj is short || obj is ushort ||
			   obj is int || obj is uint ||
			   obj is long || obj is ulong ||
			   obj is float || obj is double || obj is decimal;
	}

	/// <summary>
	/// 克隆对象（浅克隆）
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <returns>克隆后的对象</returns>
	public static T Clone<T>(T obj) where T : class
	{
		if (obj == null) return null;
		var cloneMethod = obj.GetType().GetMethod("MemberwiseClone",
			System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		return cloneMethod?.Invoke(obj, null) as T;
	}
}
