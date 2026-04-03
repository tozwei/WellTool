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
	/// 获取资源内容
	/// </summary>
	public string GetString()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetString();
		}
		return null;
	}

	/// <summary>
	/// 获取资源字节
	/// </summary>
	public byte[] GetBytes()
	{
		if (_resources.Count > 0)
		{
			return _resources[0].GetBytes();
		}
		return null;
	}

	/// <summary>
	/// 获取资源名称
	/// </summary>
	public string Name
	{
		get
		{
			if (_resources.Count > 0)
			{
				return _resources[0].Name;
			}
			return null;
		}
	}

	/// <summary>
	/// 资源是否存在
	/// </summary>
	public bool IsExist
	{
		get
		{
			foreach (var resource in _resources)
			{
				if (resource.IsExist)
				{
					return true;
				}
			}
			return false;
		}
	}
}
