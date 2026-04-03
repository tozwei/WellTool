namespace WellTool.Core.IO.Resource;

using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 多个资源的组合
/// 
/// @author looly
/// </summary>
public class MultiResource : IResource
{
    private readonly IResource[] _resources;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="resources">资源数组</param>
    public MultiResource(params IResource[] resources)
    {
        _resources = resources ?? Array.Empty<IResource>();
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="resources">资源列表</param>
    public MultiResource(IEnumerable<IResource> resources)
    {
        if (resources is IResource[] array)
        {
            _resources = array;
        }
        else
        {
            var list = new List<IResource>();
            foreach (var r in resources)
            {
                list.Add(r);
            }
            _resources = list.ToArray();
        }
    }

    /// <summary>
    /// 获取资源数量
    /// </summary>
    public int Count => _resources.Length;

    /// <summary>
    /// 获取资源流
    /// </summary>
    public Stream GetStream()
    {
        throw new NotSupportedException("MultiResource does not support GetStream");
    }

    /// <summary>
    /// 获取资源读取器
    /// </summary>
    public StreamReader GetReader(System.Text.Encoding? encoding = null)
    {
        throw new NotSupportedException("MultiResource does not support GetReader");
    }

    /// <summary>
    /// 获取所有资源
    /// </summary>
    public IResource[] GetResources()
    {
        return _resources;
    }

    /// <summary>
    /// 获取指定索引的资源
    /// </summary>
    public IResource GetResource(int index)
    {
        return _resources[index];
    }

    public string Name => throw new NotSupportedException();
    public string FullPath => throw new NotSupportedException();
    public bool IsAbsent => false;

    public IResource CreateRelative(string relativePath)
    {
        throw new NotSupportedException();
    }

    public long ContentLength => throw new NotSupportedException();

    public bool IsOpen => false;

    public Uri Uri => throw new NotSupportedException();

    public long LastModified => throw new NotSupportedException();
}
