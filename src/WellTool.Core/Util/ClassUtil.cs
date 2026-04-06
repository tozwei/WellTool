using System;
using System.Reflection;

namespace WellTool.Core.Util;

/// <summary>
/// ClassUtil类型工具类
/// </summary>
public static class ClassUtil
{
	/// <summary>
	/// 获取类型名称
	/// </summary>
	public static string GetSimpleName(Type type)
	{
		return type?.Name ?? "null";
	}

	/// <summary>
	/// 获取类型名称（包含泛型参数）
	/// </summary>
	public static string GetName(Type type)
	{
		if (type == null)
			return "null";
		return type.IsGenericType ? GetGenericName(type) : type.Name;
	}

	/// <summary>
	/// 获取泛型名称
	/// </summary>
	public static string GetGenericName(Type type)
	{
		if (!type.IsGenericType)
			return type.Name;

		var genericTypeName = type.Name.Split('`')[0];
		var typeParameters = Array.ConvertAll(type.GetGenericArguments(), GetName);
		return $"{genericTypeName}<{string.Join(", ", typeParameters)}>";
	}

	/// <summary>
	/// 获取包名
	/// </summary>
	public static string GetPackage(Type type)
	{
		if (type == null)
			return null;
		var ns = type.Namespace;
		return ns?.Split('.')[0];
	}

	/// <summary>
	/// 获取类加载器名称
	/// </summary>
	public static string GetClassLoaderName(Type type)
	{
		return type?.Assembly.GetName().Name;
	}

	/// <summary>
	/// 获取主类
	/// </summary>
	public static Type GetComponentType(Type type)
	{
		if (type == null)
			return null;
		return type.IsArray ? type.GetElementType() : null;
	}

    /// <summary>
    /// 是否为包装类型
    /// </summary>
    public static bool IsPrimitiveWrapper(Type type)
	{
		return type == typeof(Boolean) ||
			   type == typeof(Byte) ||
			   type == typeof(Char) ||
			   type == typeof(Int16) ||
			   type == typeof(Int32) ||
			   type == typeof(Int64) ||
			   type == typeof(Single) ||
			   type == typeof(Double);
	}

	/// <summary>
	/// 是否为原子类型
	/// </summary>
	public static bool IsPrimitive(Type type)
	{
		return type == typeof(bool) ||
			   type == typeof(byte) ||
			   type == typeof(char) ||
			   type == typeof(short) ||
			   type == typeof(int) ||
			   type == typeof(long) ||
			   type == typeof(float) ||
			   type == typeof(double) ||
			   type == typeof(string);
	}

	/// <summary>
	/// 加载类
	/// </summary>
	public static Type LoadClass(string className)
	{
		return LoadClass(className, true);
	}

	/// <summary>
	/// 加载类
	/// </summary>
	public static Type LoadClass(string className, bool initialize)
	{
		return Type.GetType(className, initialize);
	}

	/// <summary>
	/// 获取类名
	/// </summary>
	public static string GetClassName(Type type) => GetSimpleName(type);

	/// <summary>
	/// 获取类名
	/// </summary>
	public static string GetClassName(object obj) => obj == null ? null : GetSimpleName(obj.GetType());
}
