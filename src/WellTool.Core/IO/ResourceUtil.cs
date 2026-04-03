namespace WellTool.Core.io;

using System;
using System.IO;

/// <summary>
/// 资源接口
/// </summary>
public interface IResource
{
	/// <summary>
	/// 获取输入流
	/// </summary>
	Stream GetStream();

	/// <summary>
	/// 获取描述
	/// </summary>
	string GetDescription();
}

/// <summary>
/// 文件资源
/// </summary>
public class FileResource : IResource
{
	private readonly string _filePath;

	public FileResource(string filePath)
	{
		_filePath = filePath;
	}

	public Stream GetStream()
	{
		return File.OpenRead(_filePath);
	}

	public string GetDescription()
	{
		return _filePath;
	}
}

/// <summary>
/// 字节数组资源
/// </summary>
public class ByteArrayResource : IResource
{
	private readonly byte[] _data;

	public ByteArrayResource(byte[] data)
	{
		_data = data;
	}

	public Stream GetStream()
	{
		return new MemoryStream(_data);
	}

	public string GetDescription()
	{
		return "ByteArrayResource";
	}
}

/// <summary>
/// 资源工具类
/// </summary>
public static class ResourceUtil
{
	/// <summary>
	/// 从路径获取资源
	/// </summary>
	/// <param name="path">路径</param>
	/// <returns>资源</returns>
	public static IResource GetResource(string path)
	{
		if (File.Exists(path))
			return new FileResource(path);
		
		throw new FileNotFoundException($"Resource not found: {path}");
	}

	/// <summary>
	/// 获取资源流
	/// </summary>
	/// <param name="path">路径</param>
	/// <returns>流</returns>
	public static Stream GetStream(string path)
	{
		return GetResource(path).GetStream();
	}
}
