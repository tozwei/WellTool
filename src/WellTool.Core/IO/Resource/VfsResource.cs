namespace WellTool.Core.IO.Resource;

using System;
using System.IO;
using System.Text;

/// <summary>
/// 虚拟文件系统资源
/// 
/// @author looly
/// </summary>
public class VfsResource : IResource
{
    private readonly object _vfsObject;
    private readonly string? _name;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="vfsObject">VFS对象</param>
    public VfsResource(object vfsObject) : this(vfsObject, null)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="vfsObject">VFS对象</param>
    /// <param name="name">名称</param>
    public VfsResource(object vfsObject, string? name)
    {
        _vfsObject = vfsObject ?? throw new ArgumentNullException(nameof(vfsObject));
        _name = name;
    }

    /// <summary>
    /// 获取VFS对象
    /// </summary>
    public object GetVfsObject() => _vfsObject;

    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        if (_vfsObject is Stream stream)
        {
            return stream;
        }
        if (_vfsObject is byte[] bytes)
        {
            return new MemoryStream(bytes);
        }
        if (_vfsObject is string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }
        throw new NotSupportedException($"VFS object type {_vfsObject.GetType()} is not supported");
    }

    /// <summary>
    /// 获取读取器
    /// </summary>
    public StreamReader GetReader(Encoding? encoding = null)
    {
        return new StreamReader(GetStream(), encoding ?? Encoding.UTF8);
    }

    public string Name => _name ?? string.Empty;
    public string FullPath => string.Empty;
    public bool IsAbsent => _vfsObject == null;

    public IResource CreateRelative(string relativePath)
    {
        throw new NotSupportedException();
    }

    public long ContentLength
    {
        get
        {
            if (_vfsObject is byte[] bytes)
                return bytes.Length;
            if (_vfsObject is string str)
                return Encoding.UTF8.GetByteCount(str);
            return 0;
        }
    }

    public bool IsOpen
    {
        get
        {
            return _vfsObject is Stream;
        }
    }

    public Uri Uri => throw new NotSupportedException();

    public long LastModified => 0;
}
