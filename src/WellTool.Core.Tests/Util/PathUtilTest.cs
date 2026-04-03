using WellTool.Core.IO;
using Xunit;

namespace WellTool.Core.Tests;

public class PathUtilTest
{
    [Fact]
    public void NormalizeTest()
    {
        var path = PathUtil.Normalize("C:\\path\\to\\file");
        Assert.NotNull(path);
    }

    [Fact]
    public void CleanPathTest()
    {
        var path = PathUtil.CleanPath("C:\\path\\..\\file");
        Assert.Contains("file", path);
    }

    [Fact]
    public void GetFullPathTest()
    {
        var path = PathUtil.GetFullPath("test.txt");
        Assert.NotNull(path);
    }

    [Fact]
    public void GetParentTest()
    {
        var path = PathUtil.GetParent("C:\\path\\to\\file.txt");
        Assert.Contains("path", path);
    }

    [Fact]
    public void IsAbsoluteTest()
    {
        Assert.True(PathUtil.IsAbsolute("C:\\path"));
        Assert.True(PathUtil.IsAbsolute("/path"));
        Assert.False(PathUtil.IsAbsolute("path"));
    }

    [Fact]
    public void IsPathCharTest()
    {
        Assert.True(PathUtil.IsPathChar('/'));
        Assert.True(PathUtil.IsPathChar('\\'));
    }

    [Fact]
    public void UnixNormalizeTest()
    {
        var path = PathUtil.UnixNormalize("/path/../file");
        Assert.Contains("file", path);
    }

    [Fact]
    public void SeparatorsToSystemTest()
    {
        var path = PathUtil.SeparatorsToSystem("/path/to/file");
        Assert.NotNull(path);
    }
}
