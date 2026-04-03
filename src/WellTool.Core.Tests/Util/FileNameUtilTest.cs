using WellTool.Core.IO;
using Xunit;

namespace WellTool.Core.Tests;

public class FileNameUtilTest
{
    [Fact]
    public void GetNameTest()
    {
        Assert.Equal("test.txt", FileNameUtil.GetName("C:\\path\\to\\test.txt"));
        Assert.Equal("test.txt", FileNameUtil.GetName("/path/to/test.txt"));
    }

    [Fact]
    public void GetExtensionTest()
    {
        Assert.Equal(".txt", FileNameUtil.GetExtension("test.txt"));
        Assert.Equal(".log", FileNameUtil.GetExtension("test.log.bak"));
        Assert.Equal("", FileNameUtil.GetExtension("test"));
    }

    [Fact]
    public void MainNameTest()
    {
        Assert.Equal("test", FileNameUtil.MainName("test.txt"));
        Assert.Equal("test.log", FileNameUtil.MainName("test.log.bak"));
    }

    [Fact]
    public void CleanInvalidTest()
    {
        var invalid = "test<file>.txt";
        var cleaned = FileNameUtil.CleanInvalid(invalid);
        Assert.DoesNotContain("<", cleaned);
        Assert.DoesNotContain(">", cleaned);
    }

    [Fact]
    public void PrefixTest()
    {
        Assert.Equal("test", FileNameUtil.Prefix("test.txt"));
    }

    [Fact]
    public void SuffixTest()
    {
        Assert.Equal(".txt", FileNameUtil.Suffix("test.txt"));
    }

    [Fact]
    public void IsValidNameTest()
    {
        Assert.True(FileNameUtil.IsValidName("test.txt"));
        Assert.False(FileNameUtil.IsValidName("test<file>.txt"));
    }

    [Fact]
    public void NormalizeTest()
    {
        var name = FileNameUtil.Normalize("test.TXT");
        Assert.Equal("test.txt", name);
    }
}
