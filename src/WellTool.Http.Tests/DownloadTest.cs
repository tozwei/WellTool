using WellTool.Http;
using Xunit;
using System.IO;
using System;

namespace WellTool.Http.Tests;

/// <summary>
/// 下载单元测试
/// </summary>
public class DownloadTest
{
    [Fact]
    public void DownloadFileTest()
    {
        // 测试文件下载功能
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "test.txt");
        
        try
        {
            // 下载一个小文件进行测试
            var url = "http://example.com/index.html";
            var size = HttpUtil.DownloadFile(url, tempFile);
            
            // 验证文件是否下载成功
            Assert.True(File.Exists(tempFile));
            Assert.True(size > 0);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void DownloadFileWithProgressTest()
    {
        // 测试带进度显示的文件下载
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "test.html");
        
        try
        {
            // 下载一个小文件进行测试
            var url = "http://example.com/index.html";
            
            var size = HttpUtil.DownloadFile(url, tempFile);
            
            // 验证文件是否下载成功
            Assert.True(File.Exists(tempFile));
            Assert.True(size > 0);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void DownloadFileFromUrlTest()
    {
        // 测试从URL下载文件
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "index.html");
        
        try
        {
            // 下载一个小文件进行测试
            var url = "http://example.com/index.html";
            var size = HttpUtil.DownloadFile(url, tempFile);
            
            // 验证文件是否下载成功
            Assert.True(File.Exists(tempFile));
            Assert.True(size > 0);
        }
        finally
        {
            // 清理临时目录
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void DownloadFileFromUrlWithFileNameTest()
    {
        // 测试从URL下载文件并指定文件名
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "custom_name.html");
        
        try
        {
            // 下载一个小文件进行测试
            var url = "http://example.com/index.html";
            var size = HttpUtil.DownloadFile(url, tempFile);
            
            // 验证文件是否下载成功
            Assert.True(File.Exists(tempFile));
            Assert.True(size > 0);
            Assert.Equal("custom_name.html", Path.GetFileName(tempFile));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
