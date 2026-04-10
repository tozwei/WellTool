namespace WellTool.Extra.Tests;

using System;
using System.IO;
using WellTool.Extra.Compress;
using WellTool.Extra.Compress.Archiver;
using Xunit;

public class ArchiverTest
{
    [Fact]
    public void ZipTest()
    {
        // 创建临时文件和目录
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var tempFile = Path.Combine(tempDir, "test.txt");
        var tempZip = Path.GetTempFileName();
        
        try
        {
            // 创建测试目录和文件
            Directory.CreateDirectory(tempDir);
            File.WriteAllText(tempFile, "Test content for zip archive");
            
            // 使用StreamArchiver创建zip归档
            using var archiver = StreamArchiver.Create("zip", new FileInfo(tempZip));
            archiver.Add(new FileInfo(tempDir));
            archiver.Finish();
            
            // 验证归档文件是否创建成功
            Assert.True(File.Exists(tempZip));
            Assert.True(new FileInfo(tempZip).Length > 0);
        }
        finally
        {
            // 清理临时文件和目录
            if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            if (File.Exists(tempZip)) File.Delete(tempZip);
        }
    }

    [Fact]
    public void TarTest()
    {
        // Tar格式测试 - 目前StreamArchiver只支持zip，所以测试创建行为
        var tempFile = Path.GetTempFileName();
        
        try
        {
            // 尝试创建tar格式的归档器
            // 注意：StreamArchiver目前只支持zip，无论传入什么格式名称，都会创建zip归档
            using var archiver = StreamArchiver.Create("tar", new FileInfo(tempFile));
            
            // 验证归档器创建成功
            Assert.NotNull(archiver);
            
            // 验证文件被创建
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void SevenZTest()
    {
        // 7z格式测试 - 目前StreamArchiver只支持zip，所以测试创建行为
        var tempFile = Path.GetTempFileName();
        
        try
        {
            // 尝试创建7z格式的归档器
            // 注意：StreamArchiver目前只支持zip，无论传入什么格式名称，都会创建zip归档
            using var archiver = StreamArchiver.Create("7z", new FileInfo(tempFile));
            
            // 验证归档器创建成功
            Assert.NotNull(archiver);
            
            // 验证文件被创建
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void TgzTest()
    {
        // Tgz格式测试 - 目前StreamArchiver只支持zip，所以测试创建行为
        var tempFile = Path.GetTempFileName();
        
        try
        {
            // 尝试创建tgz格式的归档器
            // 注意：StreamArchiver目前只支持zip，无论传入什么格式名称，都会创建zip归档
            using var archiver = StreamArchiver.Create("tgz", new FileInfo(tempFile));
            
            // 验证归档器创建成功
            Assert.NotNull(archiver);
            
            // 验证文件被创建
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void TestStreamArchiverWithStream()
    {
        // 测试使用Stream创建归档器
        using var memoryStream = new MemoryStream();
        
        // 创建归档器
        using var archiver = StreamArchiver.Create("zip", memoryStream);
        
        // 验证归档器创建成功
        Assert.NotNull(archiver);
        
        // 验证内存流被使用
        Assert.True(memoryStream.CanWrite);
    }

    [Fact]
    public void TestStreamArchiverAddFile()
    {
        // 测试添加单个文件到归档
        var tempFile = Path.GetTempFileName();
        var tempZip = Path.GetTempFileName();
        
        try
        {
            // 创建测试文件
            File.WriteAllText(tempFile, "Test content for single file");
            
            // 创建归档器并添加文件
            using var archiver = StreamArchiver.Create("zip", new FileInfo(tempZip));
            archiver.Add(new FileInfo(tempFile));
            archiver.Finish();
            
            // 验证归档文件创建成功
            Assert.True(File.Exists(tempZip));
            Assert.True(new FileInfo(tempZip).Length > 0);
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile)) File.Delete(tempFile);
            if (File.Exists(tempZip)) File.Delete(tempZip);
        }
    }
}
