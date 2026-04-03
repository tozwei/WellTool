using System.Collections.Generic;
using System.IO;

namespace WellTool.Core.IO.Resource;

/// <summary>
/// 多文件资源
/// </summary>
public class MultiFileResource : IResource
{
	private readonly List<IResource> _resources;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="resources">资源列表</param>
	public MultiFileResource(params IResource[] resources)
	{
		_resources = new List<IResource>(resources);
	}

	/// <summary>
	/// 添加资源
	/// </summary>
	/// <param name="resource">资源</param>
	public void Add(IResource resource)
	{
		_resources.Add(resource);
	}

	/// <summary>
	/// 获取资源列表
	/// </summary>
	public IReadOnlyList<IResource> Resources => _resources.AsReadOnly();

	/// <summary>
	/// 获取资源流
	/// </summary>
	public Stream GetStream()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetStream();
		}
		return null;
	}

	/// <summary>
	/// 获取资源名
	/// </summary>
	public string GetName()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetName();
		}
		return null;
	}

	/// <summary>
	/// 获取Uri
	/// </summary>
	public System.Uri GetUri()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetUri();
		}
		return null;
	}

	/// <summary>
	/// 检查资源是否变更
	/// </summary>
	public bool IsModified()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].IsModified();
		}
		return false;
	}

	/// <summary>
	/// 将资源内容写出到流
	/// </summary>
	public void WriteTo(Stream output)
	{
		if (_resources.Count > 0)
		{
			_resources[0].WriteTo(output);
		}
	}

	/// <summary>
	/// 获得StreamReader
	/// </summary>
	public System.IO.StreamReader GetReader(System.Text.Encoding encoding)
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetReader(encoding);
		}
		return null;
	}

	/// <summary>
	/// 读取资源内容
	/// </summary>
	public string ReadStr(System.Text.Encoding encoding)
	{
		if (_resources.Count > 0)
		{
			return _resources[0].ReadStr(encoding);
		}
		return null;
	}

	/// <summary>
	/// 读取资源内容（UTF-8编码）
	/// </summary>
	public string ReadUtf8Str()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].ReadUtf8Str();
		}
		return null;
	}

	/// <summary>
	/// 读取资源内容
	/// </summary>
	public byte[] ReadBytes()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].ReadBytes();
		}
		return null;
	}
}
