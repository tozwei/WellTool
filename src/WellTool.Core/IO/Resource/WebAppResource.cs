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
	/// 获取资源名
	/// </summary>
	public string GetName() => _fileResource.GetName();

	/// <summary>
	/// 获取Uri
	/// </summary>
	public Uri GetUri() => _fileResource.GetUri();

	/// <summary>
	/// 检查资源是否变更
	/// </summary>
	public bool IsModified() => _fileResource.IsModified();

	/// <summary>
	/// 将资源内容写出到流
	/// </summary>
	public void WriteTo(Stream output) => _fileResource.WriteTo(output);

	/// <summary>
	/// 获得StreamReader
	/// </summary>
	public StreamReader GetReader(System.Text.Encoding encoding) => _fileResource.GetReader(encoding);

	/// <summary>
	/// 读取资源内容
	/// </summary>
	public string ReadStr(System.Text.Encoding encoding) => _fileResource.ReadStr(encoding);

	/// <summary>
	/// 读取资源内容（UTF-8编码）
	/// </summary>
	public string ReadUtf8Str() => _fileResource.ReadUtf8Str();

	/// <summary>
	/// 读取资源内容
	/// </summary>
	public byte[] ReadBytes() => _fileResource.ReadBytes();
}
