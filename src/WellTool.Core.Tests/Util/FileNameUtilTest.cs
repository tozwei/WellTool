using WellTool.Core.IO.File;
using Xunit;
using Assert = Xunit.Assert;

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
        Assert.Equal("txt", FileNameUtil.GetSuffix("test.txt"));
        Assert.Equal("bak", FileNameUtil.GetSuffix("test.log.bak"));
        Assert.Equal("", FileNameUtil.GetSuffix("test"));
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
        Assert.Equal("test", FileNameUtil.GetPrefix("test.txt"));
    }

    [Fact]
    public void SuffixTest()
    {
        Assert.Equal("txt", FileNameUtil.GetSuffix("test.txt"));
    }

    [Fact]
    public void ContainsInvalidTest()
    {
        Assert.False(FileNameUtil.ContainsInvalid("test.txt"));
        Assert.True(FileNameUtil.ContainsInvalid("test<file>.txt"));
    }
}
