using WellTool.Http;
using Xunit;
using System.IO;
using System;
using System.Collections.Generic;

namespace WellTool.Http.Tests;

/// <summary>
/// 文件上传单元测试
/// </summary>
public class UploadTest
{
    [Fact]
    public void UploadFileTest()
    {
        // 创建一个临时测试文件
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "test.txt");
        File.WriteAllText(tempFile, "Test file content");
        
        try
        {
            // 测试单文件上传
            var paramMap = new Dictionary<string, object?>
            {
                { "city", "北京" },
                { "file", new FileInfo(tempFile) }
            };
            
            // 这里只是测试上传请求的构建，不实际发送请求
            var request = HttpRequest.Post("http://example.com/upload")
                .Form(paramMap);
            
            // 验证请求方法
            Assert.Equal(Method.POST, request.GetMethod());
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
    public void UploadFilesTest()
    {
        // 创建临时测试文件
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile1 = Path.Combine(tempDir, "test1.txt");
        var tempFile2 = Path.Combine(tempDir, "test2.txt");
        File.WriteAllText(tempFile1, "Test file 1 content");
        File.WriteAllText(tempFile2, "Test file 2 content");
        
        try
        {
            // 测试多文件上传
            var paramMap = new Dictionary<string, object?>
            {
                { "file1", new FileInfo(tempFile1) },
                { "file2", new FileInfo(tempFile2) },
                { "fileType", "text" }
            };
            var request = HttpRequest.Post("http://example.com/upload")
                .Form(paramMap);
            
            // 验证请求方法
            Assert.Equal(Method.POST, request.GetMethod());
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile1))
            {
                File.Delete(tempFile1);
            }
            if (File.Exists(tempFile2))
            {
                File.Delete(tempFile2);
            }
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void UploadWithHeadersTest()
    {
        // 创建一个临时测试文件
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "test.txt");
        File.WriteAllText(tempFile, "Test file content");
        
        try
        {
            // 测试带自定义头的文件上传
            var headers = new Dictionary<string, string>
            {
                { "md5", "aaaaaaaa" }
            };
            
            var paramsMap = new Dictionary<string, object?>
            {
                { "fileName", Path.GetFileName(tempFile) },
                { "file", new FileInfo(tempFile) }
            };
            
            var request = HttpRequest.Post("http://example.com/upload")
                .SetChunkedStreamingMode(1024 * 1024);
            
            // 添加自定义头
            foreach (var header in headers)
            {
                request.SetHeader(header.Key, header.Value);
            }
            
            request.Form(paramsMap);
            
            // 验证请求方法和头信息
            Assert.Equal(Method.POST, request.GetMethod());
            var md5Header = request.GetHeader("md5");
            Assert.Equal("aaaaaaaa", md5Header);
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
    public void UploadWithAuthTest()
    {
        // 创建一个临时测试文件
        var tempDir = Path.Combine(Path.GetTempPath(), "WellToolTest");
        Directory.CreateDirectory(tempDir);
        var tempFile = Path.Combine(tempDir, "test.txt");
        File.WriteAllText(tempFile, "Test file content");
        
        try
        {
            // 测试带认证的文件上传
            var token = "test";
            var request = HttpRequest.Post("https://example.com/upload")
                .SetHeader("User-Agent", "PostmanRuntime/7.28.4")
                .Auth(token)
                .Form("smfile", new FileInfo(tempFile));
            
            // 验证请求方法和头信息
            Assert.Equal(Method.POST, request.GetMethod());
            var userAgent = request.GetHeader("User-Agent");
            Assert.Equal("PostmanRuntime/7.28.4", userAgent);
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
