using System;
using System.IO;

namespace WellTool.Core.IO.File;

/// <summary>
/// 文件系统工具类
/// </summary>
public static class FileSystemUtil
{
	/// <summary>
	/// 获取文件系统的根目录
	/// </summary>
	/// <returns>根目录信息</returns>
	public static DirectoryInfo GetRoot()
	{
		return new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory));
	}

	/// <summary>
	/// 获取临时目录
	/// </summary>
	/// <returns>临时目录</returns>
	public static DirectoryInfo GetTempDir()
	{
		return new DirectoryInfo(Path.GetTempPath());
	}

	/// <summary>
	/// 获取当前工作目录
	/// </summary>
	/// <returns>当前工作目录</returns>
	public static DirectoryInfo GetCurrentDir()
	{
		return new DirectoryInfo(Directory.GetCurrentDirectory());
	}

	/// <summary>
	/// 获取程序根目录
	/// </summary>
	/// <returns>程序根目录</returns>
	public static DirectoryInfo GetAppRoot()
	{
		return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
	}
}
