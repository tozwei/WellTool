using System.IO.Compression;
using System.Text;

namespace WellTool.Extra.Compress;

/// <summary>
/// 压缩工具类
/// 基于.NET内置压缩功能
/// </summary>
public static class CompressUtil
{
    /// <summary>
    /// 根据压缩文件名后缀获取压缩格式
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns>压缩格式</returns>
    public static string? GetCompressorName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return null;

        var lowerName = fileName.ToLower();
        if (lowerName.EndsWith(".gz") || lowerName.EndsWith(".gzip"))
            return "gzip";
        if (lowerName.EndsWith(".deflate"))
            return "deflate";
        if (lowerName.EndsWith(".br"))
            return "brotli";
        
        return null;
    }

    /// <summary>
    /// 根据归档文件名后缀获取归档格式
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns>归档格式</returns>
    public static string? GetArchiverName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return null;

        var lowerName = fileName.ToLower();
        if (lowerName.EndsWith(".zip"))
            return "zip";
        if (lowerName.EndsWith(".tar"))
            return "tar";
        
        return null;
    }

    /// <summary>
    /// 创建GZip压缩输出流
    /// </summary>
    /// <param name="outputStream">输出流</param>
    /// <returns>GZip压缩流</returns>
    public static Stream GetGZipOutputStream(Stream outputStream)
    {
        return new GZipOutputStream(outputStream);
    }

    /// <summary>
    /// 创建GZip解压输入流
    /// </summary>
    /// <param name="inputStream">输入流</param>
    /// <returns>GZip解压流</returns>
    public static Stream GetGZipInputStream(Stream inputStream)
    {
        return new GZipStream(inputStream, CompressionMode.Decompress);
    }

    /// <summary>
    /// 压缩数据为GZip格式
    /// </summary>
    /// <param name="data">原始数据</param>
    /// <returns>压缩后数据</returns>
    public static byte[] GZip(byte[] data)
    {
        using var output = new MemoryStream();
        using (var gzip = new GZipStream(output, CompressionMode.Compress, true))
        {
            gzip.Write(data, 0, data.Length);
        }
        return output.ToArray();
    }

    /// <summary>
    /// 解压GZip数据
    /// </summary>
    /// <param name="data">压缩数据</param>
    /// <returns>解压后数据</returns>
    public static byte[] UnGZip(byte[] data)
    {
        using var input = new MemoryStream(data);
        using var gzip = new GZipStream(input, CompressionMode.Decompress);
        using var output = new MemoryStream();
        gzip.CopyTo(output);
        return output.ToArray();
    }

    /// <summary>
    /// 归档文件为Zip
    /// </summary>
    /// <param name="sourceDir">源目录</param>
    /// <param name="targetFile">目标文件</param>
    /// <param name="charset">编码</param>
    public static void Archive(string sourceDir, string targetFile, Encoding? charset = null)
    {
        charset ??= Encoding.UTF8;
        
        if (File.Exists(targetFile))
            File.Delete(targetFile);
            
        ZipFile.CreateFromDirectory(sourceDir, targetFile);
    }

    /// <summary>
    /// 解归档Zip文件
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="targetDir">目标目录</param>
    /// <param name="charset">编码</param>
    public static void Extract(string archiveFile, string targetDir, Encoding? charset = null)
    {
        charset ??= Encoding.UTF8;
        
        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);
            
        ZipFile.ExtractToDirectory(archiveFile, targetDir);
    }

    /// <summary>
    /// 解压Zip到内存流
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <returns>内存流字典</returns>
    public static Dictionary<string, MemoryStream> ExtractToMemory(string archiveFile)
    {
        var result = new Dictionary<string, MemoryStream>();
        
        using var zipStream = new FileStream(archiveFile, FileMode.Open, FileAccess.Read);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        
        foreach (var entry in archive.Entries)
        {
            var stream = new MemoryStream();
            entry.Open().CopyTo(stream);
            stream.Position = 0;
            result[entry.FullName] = stream;
        }
        
        return result;
    }
}

/// <summary>
/// GZip输出流封装
/// </summary>
public class GZipOutputStream : Stream
{
    private readonly GZipStream _innerStream;

    public GZipOutputStream(Stream outputStream)
    {
        _innerStream = new GZipStream(outputStream, CompressionMode.Compress, true);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _innerStream.Write(buffer, offset, count);
    }

    public override bool CanRead => _innerStream.CanRead;
    public override bool CanSeek => _innerStream.CanSeek;
    public override bool CanWrite => _innerStream.CanWrite;
    public override long Length => _innerStream.Length;
    public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

    public override void Flush() => _innerStream.Flush();
    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);
    public override void SetLength(long value) => _innerStream.SetLength(value);
    public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _innerStream.Dispose();
        }
        base.Dispose(disposing);
    }
}
