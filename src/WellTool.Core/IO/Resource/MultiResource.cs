namespace WellTool.Core.IO.Resource;

using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 多个资源的组合
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
    /// 获取资源名
    /// </summary>
    public string GetName()
    {
        throw new NotSupportedException("MultiResource does not support GetName");
    }

    /// <summary>
    /// 获取Uri
    /// </summary>
    public Uri GetUri()
    {
        throw new NotSupportedException("MultiResource does not support GetUri");
    }

    /// <summary>
    /// 检查资源是否变更
    /// </summary>
    public bool IsModified()
    {
        throw new NotSupportedException("MultiResource does not support IsModified");
    }

    /// <summary>
    /// 将资源内容写出到流
    /// </summary>
    public void WriteTo(Stream output)
    {
        throw new NotSupportedException("MultiResource does not support WriteTo");
    }

    /// <summary>
    /// 获得StreamReader
    /// </summary>
    public StreamReader GetReader(System.Text.Encoding encoding)
    {
        throw new NotSupportedException("MultiResource does not support GetReader");
    }

    /// <summary>
    /// 读取资源内容
    /// </summary>
    public string ReadStr(System.Text.Encoding encoding)
    {
        throw new NotSupportedException("MultiResource does not support ReadStr");
    }

    /// <summary>
    /// 读取资源内容（UTF-8编码）
    /// </summary>
    public string ReadUtf8Str()
    {
        throw new NotSupportedException("MultiResource does not support ReadUtf8Str");
    }

    /// <summary>
    /// 读取资源内容
    /// </summary>
    public byte[] ReadBytes()
    {
        throw new NotSupportedException("MultiResource does not support ReadBytes");
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

    public string FullPath => throw new NotSupportedException();
    public bool IsAbsent => false;

    public IResource CreateRelative(string relativePath)
    {
        throw new NotSupportedException();
    }

    public long ContentLength => throw new NotSupportedException();

    public bool IsOpen => false;

    public long LastModified => throw new NotSupportedException();
}
