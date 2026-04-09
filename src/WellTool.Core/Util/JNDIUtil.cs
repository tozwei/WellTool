using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// JNDI工具类（.NET环境不支持JNDI，仅做接口定义）
/// </summary>
public static class JNDIUtil
{
	/// <summary>
	/// 查找JNDI对象
	/// </summary>
	/// <param name="name">名称</param>
	/// <returns>对象</returns>
	public static object Lookup(string name)
	{
       // .NET 环境不支持 JNDI；为兼容测试和调用方，返回 null 表示未找到
		return null;
	}

	/// <summary>
	/// 绑定对象到JNDI
	/// </summary>
	/// <param name="name">名称</param>
	/// <param name="obj">对象</param>
	public static void Bind(string name, object obj)
	{
		throw new NotSupportedException("JNDI is not supported in .NET");
	}

	/// <summary>
	/// 解绑对象
	/// </summary>
	/// <param name="name">名称</param>
	public static void Unbind(string name)
	{
		throw new NotSupportedException("JNDI is not supported in .NET");
	}

	/// <summary>
	/// 获取环境变量
	/// </summary>
	/// <param name="envName">环境变量名</param>
	/// <returns>环境变量值</returns>
	public static string GetEnvironment(string envName)
	{
		return Environment.GetEnvironmentVariable(envName);
	}

	/// <summary>
	/// 获取所有环境变量
	/// </summary>
	/// <returns>环境变量字典</returns>
	public static Dictionary<string, string> GetEnvironment()
	{
		var result = new Dictionary<string, string>();
		var envVars = Environment.GetEnvironmentVariables();
		foreach (var key in envVars.Keys)
		{
			result[key?.ToString()] = envVars[key]?.ToString();
		}
		return result;
	}
}
