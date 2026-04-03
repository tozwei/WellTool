using System;

namespace WellTool.Core.Util;

/// <summary>
/// JDK工具类
/// </summary>
public static class JdkUtil
{
	/// <summary>
	/// 获取JDK版本
	/// </summary>
	/// <returns>JDK版本</returns>
	public static string GetVersion()
	{
		return Environment.Version.ToString();
	}

	/// <summary>
	/// 获取JVM版本
	/// </summary>
	/// <returns>JVM版本</returns>
	public static string GetJdkVersion()
	{
		return GetVersion();
	}

	/// <summary>
	/// 检查是否为JDK
	/// </summary>
	/// <returns>是否为JDK</returns>
	public static bool IsJdk()
	{
		return true;
	}

	/// <summary>
	/// 获取Java版本信息
	/// </summary>
	/// <returns>版本信息</returns>
	public static string GetJavaVersion()
	{
		return GetVersion();
	}

	/// <summary>
	/// 检查是否为Java
	/// </summary>
	/// <returns>是否为Java</returns>
	public static bool IsJava()
	{
		return false;
	}
}
