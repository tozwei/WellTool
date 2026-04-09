using System.IO;
using WellTool.Core.IO;
using WellTool.Core.IO.File;
using Xunit;

namespace WellTool.Core.Tests;

public class FileUtilTest
{
    /// <summary>
    /// 测试路径规范化的基本逻辑 (使用 [Fact])
    /// </summary>
    [Fact]
    public void Normalize_PathWithParentDirectory_ShouldResolveCorrectly()
    {
        // 1. Arrange (准备数据)
        string input = "/foo/../bar";
        string expected = "/bar";

        // 2. Act (执行动作)
        string actual = FileUtil.Normalize(input);

        // 3. Assert (断言结果)
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// 测试多种路径场景 (使用 [Theory] 和 [InlineData])
    /// 这是测试路径工具更推荐的方式
    /// </summary>
    [Theory]
    [InlineData("/foo//", "/foo/")]                // 去除多余斜杠
    [InlineData("/foo/./", "/foo/")]               // 处理当前目录点
    [InlineData("/foo/../bar", "/bar")]            // 处理上级目录
    [InlineData("/../", "/")]                      // 根目录无法再向上
    [InlineData("foo/bar/..", "foo")]              // 相对路径处理
    [InlineData("C:\\foo\\..\\bar", "C:/bar")]     // Windows 盘符和反斜杠转换
    [InlineData(null, null)]                       // 空值处理
    public void Normalize_VariousPaths_ShouldReturnExpected(string input, string expected)
    {
        // Act
        var result = FileUtil.Normalize(input);
        // Assert
        Assert.Equal(expected, result);
    }


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
