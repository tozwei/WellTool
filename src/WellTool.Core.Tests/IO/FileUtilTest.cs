using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileUtilTest
{
    [Fact]
    public void ExistsTest()
    {
        // 创建临时文件进行测试
        var tempFile = Path.Combine(Path.GetTempPath(), "test_exists.txt");
        File.WriteAllText(tempFile, "test");
        try
        {
            Assert.True(FileUtil.Exists(tempFile));
            Assert.False(FileUtil.Exists("not_exist_file.txt"));
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void GetAbsolutePathTest()
    {
        var path = FileUtil.GetAbsolutePath(".");
        Assert.NotNull(path);
        Assert.True(Path.IsPathRooted(path));
    }

    [Fact]
    public void GetNameTest()
    {
        Assert.Equal("test.txt", FileUtil.GetName("C:\\path\\to\\test.txt"));
        Assert.Equal("test.txt", FileUtil.GetName("/path/to/test.txt"));
    }

    [Fact]
    public void GetExtensionTest()
    {
        Assert.Equal(".txt", FileUtil.GetExtension("test.txt"));
        Assert.Equal(".bak", FileUtil.GetExtension("test.log.bak"));
        Assert.Equal("", FileUtil.GetExtension("test"));
    }

    [Fact]
    public void MainNameTest()
    {
        Assert.Equal("test", FileUtil.MainName("test.txt"));
        Assert.Equal("test", FileUtil.MainName("/path/to/test.txt"));
    }

    [Fact]
    public void DirectoryExistsTest()
    {
        Assert.True(FileUtil.DirectoryExists("."));
        Assert.False(FileUtil.DirectoryExists("not_exist_dir"));
    }

    [Fact]
    public void CreateDirectoryTest()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), "welltool_test_" + Guid.NewGuid().ToString("N"));
        try
        {
            FileUtil.CreateDirectory(tempPath);
            Assert.True(Directory.Exists(tempPath));
        }
        finally
        {
            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);
        }
    }

    [Fact]
    public void DeleteTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "welltool_test_" + Guid.NewGuid().ToString("N") + ".txt");
        File.WriteAllText(tempFile, "test");
        Assert.True(File.Exists(tempFile));

        FileUtil.Delete(tempFile);
        Assert.False(File.Exists(tempFile));
    }
}
