using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileTypeUtilTest
{
    [Fact]
    public void GetTypeTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "test");
            var type = FileTypeUtil.GetType(tempFile);
            Assert.NotNull(type);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void GetExtensionTest()
    {
        Assert.Equal("txt", FileTypeUtil.GetExtension("test.txt"));
        Assert.Equal("jpg", FileTypeUtil.GetExtension("photo.jpg"));
    }

    [Fact]
    public void GetMimeTypeTest()
    {
        var mimeType = FileTypeUtil.GetMimeType("test.txt");
        Assert.NotNull(mimeType);
    }
}
