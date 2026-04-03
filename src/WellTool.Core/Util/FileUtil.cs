using System;
using System.IO;
using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// FileUtil文件工具类
/// </summary>
public static class FileUtil
{
	/// <summary>
	/// 读取文件内容
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <returns>文件内容</returns>
	public static string ReadString(string path) => File.ReadAllText(path);

	/// <summary>
	/// 读取文件内容
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="encoding">编码</param>
	/// <returns>文件内容</returns>
	public static string ReadString(string path, Encoding encoding) => File.ReadAllText(path, encoding);

	/// <summary>
	/// 读取文件行
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <returns>文件行</returns>
	public static string[] ReadLines(string path) => File.ReadAllLines(path);

	/// <summary>
	/// 读取文件行
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="encoding">编码</param>
	/// <returns>文件行</returns>
	public static string[] ReadLines(string path, Encoding encoding) => File.ReadAllLines(path, encoding);

	/// <summary>
	/// 读取文件字节
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <returns>文件字节</returns>
	public static byte[] ReadBytes(string path) => File.ReadAllBytes(path);

	/// <summary>
	/// 写入文件
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="content">内容</param>
	public static void WriteString(string path, string content) => File.WriteAllText(path, content);

	/// <summary>
	/// 写入文件
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="content">内容</param>
	/// <param name="encoding">编码</param>
	public static void WriteString(string path, string content, Encoding encoding) => File.WriteAllText(path, content, encoding);

	/// <summary>
	/// 写入文件
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="content">内容</param>
	public static void WriteBytes(string path, byte[] content) => File.WriteAllBytes(path, content);

	/// <summary>
	/// 追加文件
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="content">内容</param>
	public static void AppendString(string path, string content) => File.AppendAllText(path, content);

	/// <summary>
	/// 追加文件
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <param name="content">内容</param>
	/// <param name="encoding">编码</param>
	public static void AppendString(string path, string content, Encoding encoding) => File.AppendAllText(path, content, encoding);

	/// <summary>
	/// 文件是否存在
	/// </summary>
	public static bool Exists(string path) => File.Exists(path);

	/// <summary>
	/// 删除文件
	/// </summary>
	public static bool Delete(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
			return true;
		}
		return false;
	}

	/// <summary>
	/// 复制文件
	/// </summary>
	public static void Copy(string src, string dest) => File.Copy(src, dest);

	/// <summary>
	/// 移动文件
	/// </summary>
	public static void Move(string src, string dest) => File.Move(src, dest);

	/// <summary>
	/// 获取扩展名
	/// </summary>
	public static string Ext(string path) => Path.GetExtension(path);

	/// <summary>
	/// 获取文件名
	/// </summary>
	public static string Name(string path) => Path.GetFileName(path);

	/// <summary>
	/// 获取文件名（不含扩展名）
	/// </summary>
	public static string NameWithoutExt(string path) => Path.GetFileNameWithoutExtension(path);

	/// <summary>
	/// 获取目录名
	/// </summary>
	public static string Dir(string path) => Path.GetDirectoryName(path);
}
