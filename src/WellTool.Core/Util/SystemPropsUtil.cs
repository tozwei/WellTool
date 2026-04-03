using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// 系统属性工具类
/// </summary>
public static class SystemPropsUtil
{
	/// <summary>
	/// 获取系统属性
	/// </summary>
	/// <param name="key">键</param>
	/// <returns>值</returns>
	public static string Get(string key)
	{
		return Environment.GetEnvironmentVariable(key);
	}

	/// <summary>
	/// 获取系统属性，带默认值
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>值</returns>
	public static string Get(string key, string defaultValue)
	{
		var value = Get(key);
		return string.IsNullOrEmpty(value) ? defaultValue : value;
	}

	/// <summary>
	/// 设置系统属性
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	public static void Set(string key, string value)
	{
		Environment.SetEnvironmentVariable(key, value);
	}

	/// <summary>
	/// 获取所有系统属性
	/// </summary>
	/// <returns>系统属性字典</returns>
	public static Dictionary<string, string> GetAll()
	{
		var result = new Dictionary<string, string>();
		var envVars = Environment.GetEnvironmentVariables();
		foreach (var key in envVars.Keys)
		{
			result[key?.ToString()] = envVars[key]?.ToString();
		}
		return result;
	}

	/// <summary>
	/// 获取操作系统名称
	/// </summary>
	public static string OsName => Environment.OSVersion.Platform.ToString();

	/// <summary>
	/// 获取操作系统版本
	/// </summary>
	public static string OsVersion => Environment.OSVersion.VersionString;

	/// <summary>
	/// 获取处理器数量
	/// </summary>
	public static int ProcessorCount => Environment.ProcessorCount;

	/// <summary>
	/// 获取当前工作目录
	/// </summary>
	public static string UserDir => Environment.CurrentDirectory;

	/// <summary>
	/// 获取用户名称
	/// </summary>
	public static string UserName => Environment.UserName;

	/// <summary>
	/// 获取行分隔符
	/// </summary>
	public static string LineSeparator => Environment.NewLine;

	/// <summary>
	/// 获取路径分隔符
	/// </summary>
	public static string PathSeparator => System.IO.Path.PathSeparator.ToString();

	/// <summary>
	/// 获取临时目录
	/// </summary>
	public static string TempDir => System.IO.Path.GetTempPath();

	/// <summary>
	/// 获取用户主目录
	/// </summary>
	public static string UserHome => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

	/// <summary>
	/// 获取Java版本
	/// </summary>
	public static string JavaVersion => Environment.Version.ToString();
}
