using WellTool.Core.Util;
using Xunit;
using System.IO;
using System;

namespace WellTool.Core.Tests;

public class FileUtilLastTest
{
    [Fact]
    public void FileExistsTest()
    {
        Assert.True(FileUtil.Exists("README.md"));
        Assert.False(FileUtil.Exists("not_exist_file.txt"));
    }

    [Fact]
    public void DirectoryExistsTest()
    {
        Assert.True(Directory.Exists("."));
    }

    [Fact]
    public void GetAbsolutePathTest()
    {
        var path = Path.GetFullPath(".");
        Assert.NotNull(path);
        Assert.True(Path.IsPathRooted(path));
    }

    [Fact]
    public void GetNameTest()
    {
        Assert.Equal("test.txt", FileUtil.Name("C:\\path\\to\\test.txt"));
    }

    [Fact]
    public void GetExtensionTest()
    {
        Assert.Equal(".txt", FileUtil.Ext("test.txt"));
    }

    [Fact]
    public void MainNameTest()
    {
        Assert.Equal("test", FileUtil.NameWithoutExt("test.txt"));
    }

    [Fact]
    public void IsDirectoryTest()
    {
        Assert.True(Directory.Exists("."));
        Assert.False(File.Exists("README.md"));
    }

    [Fact]
    public void MkdirsTest()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), "welltool_test_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempPath);
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

        FileUtil.Delete(tempFile);
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
            FileUtil.Copy(tempFile1, tempFile2);
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
            FileUtil.Move(tempFile1, tempFile2);
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
        
        foreach (var file in Directory.GetFiles(tempDir))
        {
            File.Delete(file);
        }
        Assert.Empty(Directory.GetFiles(tempDir));
        
        Directory.Delete(tempDir);
    }

    [Fact]
    public void DirTest()
    {
        var path = "C:\\path\\to\\test.txt";
        var dir = FileUtil.Dir(path);
        Assert.Equal("C:\\path\\to", dir);
    }
}
