using System;
using System.IO;

namespace WellTool.Core.Util;

/// <summary>
/// DirUtil目录工具类
/// </summary>
public static class DirUtil
{
	/// <summary>
	/// 目录是否存在
	/// </summary>
	public static bool Exists(string path) => Directory.Exists(path);

	/// <summary>
	/// 创建目录
	/// </summary>
	public static DirectoryInfo Create(string path) => Directory.CreateDirectory(path);

	/// <summary>
	/// 删除目录
	/// </summary>
	public static void Delete(string path) => Directory.Delete(path);

	/// <summary>
	/// 删除目录（递归）
	/// </summary>
	public static void Delete(string path, bool recursive) => Directory.Delete(path, recursive);

	/// <summary>
	/// 获取目录下的文件
	/// </summary>
	public static string[] GetFiles(string path) => Directory.GetFiles(path);

	/// <summary>
	/// 获取目录下的文件
	/// </summary>
	public static string[] GetFiles(string path, string searchPattern) => Directory.GetFiles(path, searchPattern);

	/// <summary>
	/// 获取目录下的子目录
	/// </summary>
	public static string[] GetDirectories(string path) => Directory.GetDirectories(path);

	/// <summary>
	/// 获取目录下的子目录
	/// </summary>
	public static string[] GetDirectories(string path, string searchPattern) => Directory.GetDirectories(path, searchPattern);

	/// <summary>
	/// 获取当前工作目录
	/// </summary>
	public static string CurrentDir() => Directory.GetCurrentDirectory();

	/// <summary>
	/// 设置当前工作目录
	/// </summary>
	public static void SetCurrentDir(string path) => Directory.SetCurrentDirectory(path);

	/// <summary>
	/// 获取临时目录
	/// </summary>
	public static string TempDir() => Path.GetTempPath();
}
