using System;
using System.IO;

namespace WellTool.Core.IO.File;

/// <summary>
/// 文件包装类
/// </summary>
public class FileWrapper
{
	private readonly FileInfo _file;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="file">文件</param>
	public FileWrapper(FileInfo file)
	{
		_file = file;
	}

	/// <summary>
	/// 获取文件信息
	/// </summary>
	public FileInfo File => _file;

	/// <summary>
	/// 获取文件名
	/// </summary>
	public string Name => _file.Name;

	/// <summary>
	/// 获取不带扩展名的文件名
	/// </summary>
	public string NameWithoutExtension => Path.GetFileNameWithoutExtension(_file.Name);

	/// <summary>
	/// 获取文件扩展名
	/// </summary>
	public string Extension => _file.Extension;

	/// <summary>
	/// 获取文件大小
	/// </summary>
	public long Size => _file.Length;

	/// <summary>
	/// 获取文件是否存在
	/// </summary>
	public bool IsFile => _file.Exists;

	/// <summary>
	/// 获取是否是目录
	/// </summary>
	public bool IsDirectory => _file.Attributes.HasFlag(FileAttributes.Directory);

	/// <summary>
	/// 获取最后修改时间
	/// </summary>
	public DateTime LastWriteTime => _file.LastWriteTime;

	/// <summary>
	/// 获取创建时间
	/// </summary>
	public DateTime CreationTime => _file.CreationTime;

	/// <summary>
	/// 获取最后访问时间
	/// </summary>
	public DateTime LastAccessTime => _file.LastAccessTime;
}
