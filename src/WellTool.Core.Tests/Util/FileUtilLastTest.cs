using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileUtilLastTest
{
    [Fact]
    public void FileExistsTest()
    {
        Assert.True(FileUtil.FileExists("README.md"));
        Assert.False(FileUtil.FileExists("not_exist_file.txt"));
    }

    [Fact]
    public void DirectoryExistsTest()
    {
        Assert.True(FileUtil.DirectoryExists("."));
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
    }

    [Fact]
    public void GetExtensionTest()
    {
        Assert.Equal(".txt", FileUtil.GetExtension("test.txt"));
    }

    [Fact]
    public void MainNameTest()
    {
        Assert.Equal("test", FileUtil.MainName("test.txt"));
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

    [Fact]
    public void CopyFileTest()
    {
        var tempFile1 = Path.Combine(Path.GetTempPath(), "test_src_" + Guid.NewGuid().ToString("N") + ".txt");
        var tempFile2 = Path.Combine(Path.GetTempPath(), "test_dst_" + Guid.NewGuid().ToString("N") + ".txt");
        
        try
        {
            File.WriteAllText(tempFile1, "Hello");
            FileUtil.CopyFile(tempFile1, tempFile2);
            Assert.True(File.Exists(tempFile2));
            Assert.Equal("Hello", File.ReadAllText(tempFile2));
        }
        finally
        {
            if (File.Exists(tempFile1)) File.Delete(tempFile1);
            if (File.Exists(tempFile2)) File.Delete(tempFile2);
        }
    }

    [Fact]
    public void MoveFileTest()
    {
        var tempFile1 = Path.Combine(Path.GetTempPath(), "test_src_" + Guid.NewGuid().ToString("N") + ".txt");
        var tempFile2 = Path.Combine(Path.GetTempPath(), "test_dst_" + Guid.NewGuid().ToString("N") + ".txt");
        
        try
        {
            File.WriteAllText(tempFile1, "Hello");
            FileUtil.MoveFile(tempFile1, tempFile2);
            Assert.False(File.Exists(tempFile1));
            Assert.True(File.Exists(tempFile2));
        }
        finally
        {
            if (File.Exists(tempFile1)) File.Delete(tempFile1);
            if (File.Exists(tempFile2)) File.Delete(tempFile2);
        }
    }

    [Fact]
    public void CleanDirectoryTest()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "welltool_clean_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        File.WriteAllText(Path.Combine(tempDir, "test.txt"), "test");
        
        FileUtil.CleanDirectory(tempDir);
        Assert.Empty(Directory.GetFiles(tempDir));
        
        Directory.Delete(tempDir);
    }
}
