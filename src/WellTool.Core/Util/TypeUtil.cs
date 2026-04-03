using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// 类型工具类
/// </summary>
public static class TypeUtil
{
	/// <summary>
	/// 获取类型的名称
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>类型名称</returns>
	public static string GetName(Type type)
	{
		return type?.Name;
	}

	/// <summary>
	/// 获取类型的全名
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>类型全名</returns>
	public static string GetFullName(Type type)
	{
		return type?.FullName;
	}

	/// <summary>
	/// 判断类型是否为基本类型
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为基本类型</returns>
	public static bool IsBasicType(Type type)
	{
		if (type == null) return false;
		return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
	}

	/// <summary>
	/// 判断类型是否为值类型
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为值类型</returns>
	public static bool IsValueType(Type type)
	{
		return type?.IsValueType ?? false;
	}

	/// <summary>
	/// 判断类型是否为枚举
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为枚举</returns>
	public static bool IsEnum(Type type)
	{
		return type?.IsEnum ?? false;
	}

	/// <summary>
	/// 判断类型是否为数组
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为数组</returns>
	public static bool IsArray(Type type)
	{
		return type?.IsArray ?? false;
	}

	/// <summary>
	/// 获取数组元素的类型
	/// </summary>
	/// <param name="type">数组类型</param>
	/// <returns>元素类型</returns>
	public static Type GetElementType(Type type)
	{
		if (type?.IsArray == true)
		{
			return type.GetElementType();
		}
		return null;
	}

	/// <summary>
	/// 获取泛型类型参数
	/// </summary>
	/// <param name="type">泛型类型</param>
	/// <returns>类型参数数组</returns>
	public static Type[] GetGenericArguments(Type type)
	{
		return type?.GetGenericArguments();
	}

	/// <summary>
	/// 获取泛型的原始定义类型
	/// </summary>
	/// <param name="type">泛型类型</param>
	/// <returns>原始定义类型</returns>
	public static Type GetGenericTypeDefinition(Type type)
	{
		return type?.GetGenericTypeDefinition();
	}

	/// <summary>
	/// 获取类型的父类型
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>父类型</returns>
	public static Type GetBaseType(Type type)
	{
		return type?.BaseType;
	}

	/// <summary>
	/// 判断类型是否实现接口
	/// </summary>
	/// <param name="type">类型</param>
	/// <param name="interfaceType">接口类型</param>
	/// <returns>是否实现接口</returns>
	public static bool IsAssignableFrom(Type type, Type interfaceType)
	{
		if (type == null || interfaceType == null) return false;
		return interfaceType.IsAssignableFrom(type);
	}

	/// <summary>
	/// 获取类型的命名空间
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>命名空间</returns>
	public static string GetNamespace(Type type)
	{
		return type?.Namespace;
	}

	/// <summary>
	/// 获取类型的程序集
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>程序集名称</returns>
	public static string GetAssemblyName(Type type)
	{
		return type?.Assembly?.GetName()?.Name;
	}
}
