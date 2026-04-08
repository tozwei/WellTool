namespace WellTool.Extra.Tests;

using System.IO;
using System.IO.Compression;
using WellTool.Extra.Compress;

public class ExtractorTest
{
    private readonly string _tempDir;
    
    public ExtractorTest()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"WellToolTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(_tempDir);
    }

    [Fact]
    public void ZipExtractTest()
    {
        // 创建测试zip文件
        var zipFile = Path.Combine(_tempDir, "test.zip");
        var extractDir = Path.Combine(_tempDir, "extract");
        
        using (var zip = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        {
            var entry = zip.CreateEntry("test.txt");
            using var writer = new StreamWriter(entry.Open());
            writer.Write("Hello World");
        }
        
        // 测试解压
        CompressUtil.Extract(zipFile, extractDir);
        
        var extractedFile = Path.Combine(extractDir, "test.txt");
        Assert.True(File.Exists(extractedFile));
        
        var content = File.ReadAllText(extractedFile);
        Assert.Equal("Hello World", content);
    }

    [Fact]
    public void ExtractToMemoryTest()
    {
        // 创建测试zip文件
        var zipFile = Path.Combine(_tempDir, "test2.zip");
        
        using (var zip = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        {
            var entry = zip.CreateEntry("data.txt");
            using var writer = new StreamWriter(entry.Open());
            writer.Write("Test Data");
        }
        
        // 测试解压到内存流
        var result = CompressUtil.ExtractToMemory(zipFile);
        
        Assert.True(result.ContainsKey("data.txt"));
        
        using var reader = new StreamReader(result["data.txt"]);
        var content = reader.ReadToEnd();
        Assert.Equal("Test Data", content);
    }

    [Fact]
    public void GetArchiverNameTest()
    {
        Assert.Equal("zip", CompressUtil.GetArchiverName("test.zip"));
        Assert.Equal("tar", CompressUtil.GetArchiverName("test.tar"));
        Assert.Null(CompressUtil.GetArchiverName("test.txt"));
    }

    [Fact]
    public void GZipTest()
    {
        var originalData = "Hello GZip";
        var bytes = System.Text.Encoding.UTF8.GetBytes(originalData);
        
        var compressed = CompressUtil.GZip(bytes);
        Assert.NotNull(compressed);
        
        var decompressed = CompressUtil.UnGZip(compressed);
        var result = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal(originalData, result);
    }

    [Fact]
    public void GetCompressorNameTest()
    {
        Assert.Equal("gzip", CompressUtil.GetCompressorName("test.gz"));
        Assert.Equal("gzip", CompressUtil.GetCompressorName("test.gzip"));
        Assert.Equal("deflate", CompressUtil.GetCompressorName("test.deflate"));
        Assert.Equal("brotli", CompressUtil.GetCompressorName("test.br"));
        Assert.Null(CompressUtil.GetCompressorName("test.txt"));
    }
}
