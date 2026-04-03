using System;
using System.Reflection;

namespace WellTool.Core.Util;

/// <summary>
/// 修饰符工具类
/// </summary>
public static class ModifierUtil
{
	/// <summary>
	/// 修饰符枚举
	/// </summary>
	[Flags]
	public enum Modifier
	{
		/// <summary>
		/// 公共
		/// </summary>
		Public = 0x0001,
		/// <summary>
		/// 私有
		/// </summary>
		Private = 0x0002,
		/// <summary>
		/// 保护
		/// </summary>
		Protected = 0x0004,
		/// <summary>
		/// 内部
		/// </summary>
		Internal = 0x0008,
		/// <summary>
		/// 静态
		/// </summary>
		Static = 0x0010,
		/// <summary>
		/// 最终
		/// </summary>
		Final = 0x0020,
		/// <summary>
		/// 抽象
		/// </summary>
		Abstract = 0x0040,
		/// <summary>
		/// 虚拟
		/// </summary>
		Virtual = 0x0080,
		/// <summary>
		/// 覆盖
		/// </summary>
		Override = 0x0100,
		/// <summary>
		/// 只读
		/// </summary>
		ReadOnly = 0x0200,
		/// <summary>
		/// 密封
		/// </summary>
		Sealed = 0x0400
	}

	/// <summary>
	/// 判断类型是否为公共的
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为公共的</returns>
	public static bool IsPublic(Type type)
	{
		return type?.IsPublic ?? false;
	}

	/// <summary>
	/// 判断类型是否为私有的
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为私有的</returns>
	public static bool IsPrivate(Type type)
	{
		return type?.IsNotPublic ?? false;
	}

	/// <summary>
	/// 判断类型是否为内部的
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为内部的</returns>
	public static bool IsInternal(Type type)
	{
		if (type == null) return false;
		return !type.IsPublic && !type.IsPrivate && !type.IsNested;
	}

	/// <summary>
	/// 判断类型是否为静态的
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为静态的</returns>
	public static bool IsStatic(Type type)
	{
		if (type == null) return false;
		return type.IsAbstract && type.IsSealed;
	}

	/// <summary>
	/// 判断类型是否为抽象的
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>是否为抽象的</returns>
	public static bool IsAbstract(Type type)
	{
		return type?.IsAbstract ?? false;
	}

	/// <summary>
	/// 判断方法是否为抽象的
	/// </summary>
	/// <param name="method">方法</param>
	/// <returns>是否为抽象的</returns>
	public static bool IsAbstract(MethodBase method)
	{
		return method?.IsAbstract ?? false;
	}

	/// <summary>
	/// 判断方法是否为虚方法
	/// </summary>
	/// <param name="method">方法</param>
	/// <returns>是否为虚方法</returns>
	public static bool IsVirtual(MethodBase method)
	{
		return method?.IsVirtual ?? false;
	}

	/// <summary>
	/// 判断方法是否为最终方法
	/// </summary>
	/// <param name="method">方法</param>
	/// <returns>是否为最终方法</returns>
	public static bool IsFinal(MethodBase method)
	{
		return method?.IsFinal ?? false;
	}

	/// <summary>
	/// 判断字段是否为只读的
	/// </summary>
	/// <param name="field">字段</param>
	/// <returns>是否为只读的</returns>
	public static bool IsReadOnly(FieldInfo field)
	{
		return field?.IsInitOnly ?? false;
	}

	/// <summary>
	/// 判断字段是否为常量
	/// </summary>
	/// <param name="field">字段</param>
	/// <returns>是否为常量</returns>
	public static bool IsConstant(FieldInfo field)
	{
		if (field == null) return false;
		return field.IsLiteral && !field.IsInitOnly;
	}

	/// <summary>
	/// 获取修饰符字符串
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>修饰符字符串</returns>
	public static string ToString(Type type)
	{
		if (type == null) return string.Empty;
		var modifiers = string.Empty;
		if (IsPublic(type)) modifiers += "public ";
		if (IsPrivate(type)) modifiers += "private ";
		if (IsInternal(type)) modifiers += "internal ";
		if (IsAbstract(type)) modifiers += "abstract ";
		if (IsStatic(type)) modifiers += "static ";
		return modifiers.TrimEnd();
	}
}
