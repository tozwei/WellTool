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
    public StreamReader GetReader(Encoding encoding)
    {
        return new StreamReader(_filePath, encoding ?? Encoding.UTF8);
    }

    /// <summary>
    /// 获取资源名
    /// </summary>
    public string GetName()
    {
        return Path.GetFileName(_filePath);
    }

    /// <summary>
    /// 获取Uri
    /// </summary>
    public Uri GetUri()
    {
        return new Uri(_filePath);
    }

    /// <summary>
    /// 检查资源是否变更
    /// </summary>
    public bool IsModified()
    {
        // 简单实现：返回false，表示资源未变更
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

    public long LastModified => File.Exists(_filePath) ? new FileInfo(_filePath).LastWriteTimeUtc.Ticks : 0;
}
