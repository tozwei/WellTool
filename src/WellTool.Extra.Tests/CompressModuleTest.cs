using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using WellTool.Extra.Compress;
using WellTool.Extra.Compress.Archiver;
using WellTool.Extra.Compress.Extractor;

namespace WellTool.Extra.Tests;

/// <summary>
/// CompressUtil 压缩工具测试类
/// </summary>
public class CompressUtilExpandedTest
{
    /// <summary>
    /// 测试单例实例
    /// </summary>
    [Fact]
    public void TestInstance_NotNull()
    {
        Assert.NotNull(CompressUtil.Instance);
    }

    /// <summary>
    /// 测试压缩单个文件
    /// </summary>
    [Fact]
    public void TestZipFile()
    {
        // 创建临时文件
        var tempFile = Path.GetTempFileName();
        var tempZip = Path.GetTempFileName();
        
        try
        {
            File.WriteAllText(tempFile, "Test content for compression");
            
            CompressUtil.Instance.ZipFile(tempFile, tempZip);
            
            Assert.True(File.Exists(tempZip));
            Assert.True(new FileInfo(tempZip).Length > 0);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(tempZip)) File.Delete(tempZip);
        }
    }

    /// <summary>
    /// 测试压缩目录
    /// </summary>
    [Fact]
    public void TestZipDirectory()
    {
        // 创建临时目录和文件
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempZip = Path.GetTempFileName();
        
        try
        {
            Directory.CreateDirectory(tempDir);
            File.WriteAllText(Path.Combine(tempDir, "file1.txt"), "Content 1");
            File.WriteAllText(Path.Combine(tempDir, "file2.txt"), "Content 2");
            
            CompressUtil.Instance.ZipDirectory(tempDir, tempZip);
            
            Assert.True(File.Exists(tempZip));
            Assert.True(new FileInfo(tempZip).Length > 0);
        }
        finally
        {
            if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            if (File.Exists(tempZip)) File.Delete(tempZip);
        }
    }

    /// <summary>
    /// 测试解压文件
    /// </summary>
    [Fact]
    public void TestUnzip()
    {
        // 创建测试ZIP文件
        var tempFile = Path.GetTempFileName();
        var tempZip = Path.GetTempFileName();
        var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        
        try
        {
            File.WriteAllText(tempFile, "Content to extract");
            CompressUtil.Instance.ZipFile(tempFile, tempZip);
            
            Directory.CreateDirectory(extractDir);
            CompressUtil.Instance.Unzip(tempZip, extractDir);
            
            var extractedFiles = Directory.GetFiles(extractDir);
            Assert.NotEmpty(extractedFiles);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(tempZip)) File.Delete(tempZip);
            if (Directory.Exists(extractDir)) Directory.Delete(extractDir, true);
        }
    }

    /// <summary>
    /// 测试Gzip压缩
    /// </summary>
    [Fact]
    public void TestGzip()
    {
        var data = Encoding.UTF8.GetBytes("Test data for GZIP compression");
        
        var compressed = CompressUtil.Instance.Gzip(data);
        
        Assert.NotNull(compressed);
        Assert.True(compressed.Length > 0);
    }

    /// <summary>
    /// 测试Gzip解压
    /// </summary>
    [Fact]
    public void TestGunzip()
    {
        var originalData = Encoding.UTF8.GetBytes("Test data for GZIP compression");
        var compressed = CompressUtil.Instance.Gzip(originalData);
        
        var decompressed = CompressUtil.Instance.Gunzip(compressed);
        
        Assert.Equal(originalData, decompressed);
    }

    /// <summary>
    /// 测试Gzip压缩解压往返
    /// </summary>
    [Fact]
    public void TestGzipRoundTrip()
    {
        var originalString = "Hello, this is a test string with special chars: 你好世界! @#$%";
        var originalData = Encoding.UTF8.GetBytes(originalString);
        
        var compressed = CompressUtil.Instance.Gzip(originalData);
        var decompressed = CompressUtil.Instance.Gunzip(compressed);
        var resultString = Encoding.UTF8.GetString(decompressed);
        
        Assert.Equal(originalString, resultString);
    }

    /// <summary>
    /// 测试空数据Gzip压缩
    /// </summary>
    [Fact]
    public void TestGzip_EmptyData()
    {
        var data = Array.Empty<byte>();
        
        var compressed = CompressUtil.Instance.Gzip(data);
        
        Assert.NotNull(compressed);
        // Gzip压缩头会有最小长度
        Assert.True(compressed.Length >= 20);
    }

    /// <summary>
    /// 测试大数据Gzip压缩
    /// </summary>
    [Fact]
    public void TestGzip_LargeData()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 10000; i++)
        {
            sb.AppendLine($"Line {i}: This is test data for compression");
        }
        var data = Encoding.UTF8.GetBytes(sb.ToString());
        
        var compressed = CompressUtil.Instance.Gzip(data);
        var decompressed = CompressUtil.Instance.Gunzip(compressed);
        
        Assert.Equal(data, decompressed);
    }
}

/// <summary>
/// CompressException 异常测试类
/// </summary>
public class CompressExceptionTest
{
    /// <summary>
    /// 测试构造异常 - 消息
    /// </summary>
    [Fact]
    public void TestConstructor_Message()
    {
        var ex = new CompressException("Compression failed");
        Assert.Equal("Compression failed", ex.Message);
    }

    /// <summary>
    /// 测试构造异常 - 消息和内部异常
    /// </summary>
    [Fact]
    public void TestConstructor_MessageAndInner()
    {
        var innerEx = new IOException("IO error");
        var ex = new CompressException("Zip error", innerEx);
        
        Assert.Equal("Zip error", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }

    /// <summary>
    /// 测试异常可抛出和捕获
    /// </summary>
    [Fact]
    public async Task TestCanThrowAndCatch()
    {
        await Assert.ThrowsAsync<CompressException>(async () =>
        {
            throw new CompressException("Test error");
        });
    }
}



/// <summary>
/// StreamArchiver 测试类
/// </summary>
public class StreamArchiverTest
{
    /// <summary>
    /// 测试StreamArchiver类存在
    /// </summary>
    [Fact]
    public void TestStreamArchiverExists()
    {
        var type = typeof(StreamArchiver);
        Assert.NotNull(type);
    }

    /// <summary>
    /// 测试StreamArchiver实现Archiver接口
    /// </summary>
    [Fact]
    public void TestImplementsArchiver()
    {
        Assert.True(typeof(Archiver).IsAssignableFrom(typeof(StreamArchiver)));
    }
}

/// <summary>
/// SevenZArchiver 测试类
/// </summary>
public class SevenZArchiverTest
{
    /// <summary>
    /// 测试SevenZArchiver类存在
    /// </summary>
    [Fact]
    public void TestSevenZArchiverExists()
    {
        var type = typeof(SevenZArchiver);
        Assert.NotNull(type);
    }

    /// <summary>
    /// 测试SevenZArchiver实现Archiver接口
    /// </summary>
    [Fact]
    public void TestImplementsArchiver()
    {
        Assert.True(typeof(Archiver).IsAssignableFrom(typeof(SevenZArchiver)));
    }
}

/// <summary>
/// StreamExtractor 测试类
/// </summary>
public class StreamExtractorTest
{
    /// <summary>
    /// 测试StreamExtractor类存在
    /// </summary>
    [Fact]
    public void TestStreamExtractorExists()
    {
        var type = typeof(StreamExtractor);
        Assert.NotNull(type);
    }

    /// <summary>
    /// 测试StreamExtractor实现Extractor接口
    /// </summary>
    [Fact]
    public void TestImplementsExtractor()
    {
        Assert.True(typeof(Extractor).IsAssignableFrom(typeof(StreamExtractor)));
    }
}

/// <summary>
/// SevenZExtractor 测试类
/// </summary>
public class SevenZExtractorTest
{
    /// <summary>
    /// 测试SevenZExtractor类存在
    /// </summary>
    [Fact]
    public void TestSevenZExtractorExists()
    {
        var type = typeof(SevenZExtractor);
        Assert.NotNull(type);
    }

    /// <summary>
    /// 测试SevenZExtractor实现Extractor接口
    /// </summary>
    [Fact]
    public void TestImplementsExtractor()
    {
        Assert.True(typeof(Extractor).IsAssignableFrom(typeof(SevenZExtractor)));
    }
}
