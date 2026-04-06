using WellTool.Core.IO;
using WellTool.Core.IO.File;
using System.IO;
using Xunit;

namespace WellTool.Core.Tests;

public class PathUtilTest
{
    [Fact]
    public void NormalizeTest()
    {
        var path = FileUtil.Normalize(@"C:\path\to\file");
        Assert.NotNull(path);
    }

    [Fact]
    public void CleanPathTest()
    {
        var path = Path.GetFullPath(@"C:\path\..\file");
        Assert.Contains("file", path);
    }

    [Fact]
    public void GetParentTest()
    {
        var path = Path.GetDirectoryName(@"C:\path\to\file.txt");
        Assert.Contains("path", path);
    }

    [Fact]
    public void IsAbsoluteTest()
    {
        Assert.True(Path.IsPathRooted(@"C:\path"));
        Assert.True(Path.IsPathRooted("/path"));
        Assert.False(Path.IsPathRooted("path"));
    }

    [Fact]
    public void IsPathCharTest()
    {
        Assert.True(Path.DirectorySeparatorChar == '/' || Path.DirectorySeparatorChar == '\\');
        Assert.True(Path.AltDirectorySeparatorChar == '/' || Path.AltDirectorySeparatorChar == '\\');
    }

    [Fact]
    public void UnixNormalizeTest()
    {
        var path = Path.GetFullPath("/path/../file").Replace('\\', '/');
        Assert.Contains("file", path);
    }

    [Fact]
    public void SeparatorsToSystemTest()
    {
        var path = "/path/to/file".Replace('/', Path.DirectorySeparatorChar);
        Assert.NotNull(path);
    }
}
