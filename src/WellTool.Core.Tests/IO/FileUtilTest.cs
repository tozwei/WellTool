using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileUtilTest
{
    [Fact]
    public void FileExistsTest()
    {
        Assert.True(FileUtil.FileExists("README.md"));
        Assert.False(FileUtil.FileExists("not_exist_file.txt"));
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
        Assert.Equal(".log", FileUtil.GetExtension("test.log.bak"));
        Assert.Equal("", FileUtil.GetExtension("test"));
    }

    [Fact]
    public void MainNameTest()
    {
        Assert.Equal("test", FileUtil.MainName("test.txt"));
        Assert.Equal("test", FileUtil.MainName("/path/to/test.txt"));
    }

    [Fact]
    public void IsDirectoryTest()
    {
        Assert.True(FileUtil.IsDirectory("."));
        Assert.False(FileUtil.IsDirectory("README.md"));
    }

    [Fact]
    public void MkdirsTest()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), "welltool_test_" + Guid.NewGuid().ToString("N"));
        try
        {
            FileUtil.Mkdirs(tempPath);
            Assert.True(Directory.Exists(tempPath));
        }
        finally
        {
            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);
        }
    }

    [Fact]
    public void DelTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "welltool_test_" + Guid.NewGuid().ToString("N") + ".txt");
        File.WriteAllText(tempFile, "test");
        Assert.True(File.Exists(tempFile));

        FileUtil.Del(tempFile);
        Assert.False(File.Exists(tempFile));
    }
}
