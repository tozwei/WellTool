namespace WellTool.Core.IO.Resource;

using System;
using System.IO;
using System.Text;

/// <summary>
/// 文件对象资源
/// 
/// @author looly
/// </summary>
public class FileObjectResource : IResource
{
    private readonly string _filePath;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public FileObjectResource(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    /// <summary>
    /// 获取文件路径
    /// </summary>
    public string FilePath => _filePath;

    /// <summary>
    /// 获取流
    /// </summary>
    public Stream GetStream()
    {
        return new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    }

    /// <summary>
    /// 获取读取器
    /// </summary>
    public StreamReader GetReader(Encoding? encoding = null)
    {
        return new StreamReader(_filePath, encoding ?? Encoding.UTF8);
    }

    public string Name => Path.GetFileName(_filePath);
    public string FullPath => Path.GetFullPath(_filePath);
    public bool IsAbsent => !File.Exists(_filePath);

    public IResource CreateRelative(string relativePath)
    {
        string dir = Path.GetDirectoryName(_filePath) ?? string.Empty;
        string newPath = Path.Combine(dir, relativePath);
        return new FileObjectResource(newPath);
    }

    public long ContentLength => new FileInfo(_filePath).Length;

    public bool IsOpen => false;

    public Uri Uri => new Uri(_filePath);

    public long LastModified => File.Exists(_filePath) ? new FileInfo(_filePath).LastWriteTimeUtc.Ticks : 0;
}
