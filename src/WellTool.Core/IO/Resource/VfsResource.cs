namespace WellTool.Core.IO.Resource;

using System;
using System.IO;
using System.Text;

/// <summary>
/// 虚拟文件系统资源
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
    /// 获取资源名
    /// </summary>
    public string GetName()
    {
        return _name ?? string.Empty;
    }

    /// <summary>
    /// 获取Uri
    /// </summary>
    public Uri GetUri()
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// 检查资源是否变更
    /// </summary>
    public bool IsModified()
    {
        return false;
    }

    /// <summary>
    /// 将资源内容写出到流
    /// </summary>
    public void WriteTo(Stream output)
    {
        using (var input = GetStream())
        {
            input.CopyTo(output);
        }
    }

    /// <summary>
    /// 获得StreamReader
    /// </summary>
    public StreamReader GetReader(Encoding encoding)
    {
        return new StreamReader(GetStream(), encoding ?? Encoding.UTF8);
    }

    /// <summary>
    /// 读取资源内容
    /// </summary>
    public string ReadStr(Encoding encoding)
    {
        using (var reader = GetReader(encoding))
        {
            return reader.ReadToEnd();
        }
    }

    /// <summary>
    /// 读取资源内容（UTF-8编码）
    /// </summary>
    public string ReadUtf8Str()
    {
        return ReadStr(Encoding.UTF8);
    }

    /// <summary>
    /// 读取资源内容
    /// </summary>
    public byte[] ReadBytes()
    {
        using (var stream = GetStream())
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

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

    public long LastModified => 0;
}
