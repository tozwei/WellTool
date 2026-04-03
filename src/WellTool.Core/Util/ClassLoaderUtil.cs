using System;
using System.Reflection;

namespace WellTool.Core.Util;

/// <summary>
/// 类加载器工具类
/// </summary>
public static class ClassLoaderUtil
{
	/// <summary>
	/// 获取当前类加载器
	/// </summary>
	/// <returns>类加载器</returns>
	public static Type GetClassLoader()
	{
		return typeof(ClassLoaderUtil);
	}

	/// <summary>
	/// 获取系统类加载器
	/// </summary>
	/// <returns>类加载器</returns>
	public static Type GetSystemClassLoader()
	{
		return typeof(ClassLoaderUtil);
	}

	/// <summary>
	/// 获取调用者的类加载器
	/// </summary>
	/// <returns>类加载器</returns>
	public static Type GetCallerClassLoader()
	{
		var trace = new System.Diagnostics.StackTrace(true);
		var frames = trace.GetFrames();
		if (frames != null && frames.Length > 2)
		{
			for (int i = 2; i < frames.Length; i++)
			{
				var method = frames[i].GetMethod();
				if (method?.DeclaringType != null)
				{
					return method.DeclaringType.Assembly.GetType();
				}
			}
		}
		return null;
	}

	/// <summary>
	/// 获取程序集
	/// </summary>
	/// <param name="className">类名</param>
	/// <returns>程序集</returns>
	public static Assembly GetAssembly(string className)
	{
		var type = Type.GetType(className);
		return type?.Assembly;
	}

	/// <summary>
	/// 加载类
	/// </summary>
	/// <param name="className">类名</param>
	/// <returns>类型</returns>
	public static Type LoadClass(string className)
	{
		return Type.GetType(className);
	}

	/// <summary>
	/// 判断类是否存在
	/// </summary>
	/// <param name="className">类名</param>
	/// <returns>是否存在</returns>
	public static bool IsClassExist(string className)
	{
		return Type.GetType(className) != null;
	}
}
