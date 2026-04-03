using System;
using System.IO;

namespace WellTool.Core.IO.Resource;

/// <summary>
/// Web应用程序资源
/// </summary>
public class WebAppResource : IResource
{
	private readonly FileResource _fileResource;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="path">资源路径</param>
	public WebAppResource(string path)
	{
		var baseDir = AppDomain.CurrentDomain.BaseDirectory;
		var fullPath = Path.Combine(baseDir, path);
		_fileResource = new FileResource(new FileInfo(fullPath));
	}

	/// <summary>
	/// 获取资源流
	/// </summary>
	public Stream GetStream() => _fileResource.GetStream();

	/// <summary>
	/// 获取资源内容
	/// </summary>
	public string GetString() => _fileResource.GetString();

	/// <summary>
	/// 获取资源字节
	/// </summary>
	public byte[] GetBytes() => _fileResource.GetBytes();

	/// <summary>
	/// 获取资源名称
	/// </summary>
	public string Name => _fileResource.Name;

	/// <summary>
	/// 资源是否存在
	/// </summary>
	public bool IsExist => _fileResource.IsExist;
}
