using System.IO;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3314Test
{
    [Fact]
    public void HttpRequestCreateTest()
    {
        // 测试创建 HttpRequest 对象
        var request = HttpRequest.Get("https://hutool.cn/test/getList");
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestMethodsTest()
    {
        // 测试 HttpRequest 的各种方法
        var request = HttpRequest.Get("https://hutool.cn/test/getList");
        
        // 测试设置超时
        request.Timeout(5000);
        
        // 测试设置 Content-Type
        request.SetContentType(ContentType.JSON);
        
        // 测试设置 Header
        request.SetHeader("Authorization", "Bearer token123");
        
        // 测试设置 Form 数据
        request.Form("key", "value");
        
        Assert.NotNull(request);
    }

    [Fact]
    public void FileReadWriteTest()
    {
        // 测试文件读写操作
        // 创建一个临时文件
        var tempFile = Path.GetTempFileName();
        var testContent = "Test content";
        
        try
        {
            // 写入文件内容
            File.WriteAllText(tempFile, testContent, Encoding.UTF8);
            
            // 验证文件是否存在
            Assert.True(File.Exists(tempFile));
            
            // 读取文件内容
            var content = File.ReadAllText(tempFile, Encoding.UTF8);
            Assert.Equal(testContent, content);
            
            // 读取文件字节
            var bytes = File.ReadAllBytes(tempFile);
            Assert.NotNull(bytes);
            Assert.NotEmpty(bytes);
            
            // 验证字节内容
            var byteContent = Encoding.UTF8.GetString(bytes);
            // 移除可能的 BOM 标记
            if (byteContent.StartsWith("\ufeff"))
            {
                byteContent = byteContent.Substring(1);
            }
            Assert.Equal(testContent, byteContent);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void PathMethodsTest()
    {
        // 测试路径相关方法
        // 测试获取文件扩展名
        Assert.Equal(".txt", Path.GetExtension("file.txt"));
        Assert.Equal(".json", Path.GetExtension("file.json"));
        Assert.Equal(".xlsx", Path.GetExtension("file.xlsx"));
        
        // 测试获取文件名
        Assert.Equal("file.txt", Path.GetFileName("path/to/file.txt"));
        Assert.Equal("file.json", Path.GetFileName("path/to/file.json"));
        
        // 测试获取目录
        Assert.Equal("path\\to", Path.GetDirectoryName("path/to/file.txt"));
        Assert.Equal("path", Path.GetDirectoryName("path/file.txt"));
    }

    [Fact]
    public void FileExistsTest()
    {
        // 测试文件存在性
        // 创建一个临时文件
        var tempFile = Path.GetTempFileName();
        
        try
        {
            // 验证文件存在
            Assert.True(File.Exists(tempFile));
            
            // 验证文件不存在
            Assert.False(File.Exists(Path.GetTempFileName() + ".non-existent"));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void FileSizeTest()
    {
        // 测试文件大小
        // 创建一个临时文件
        var tempFile = Path.GetTempFileName();
        var testContent = "Test content";
        File.WriteAllText(tempFile, testContent, Encoding.UTF8);
        
        try
        {
            // 验证文件大小
            var fileInfo = new FileInfo(tempFile);
            var size = fileInfo.Length;
            Assert.True(size > 0);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
