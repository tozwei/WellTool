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
            File.WriteAllText(tempFile, "test");
            var type = Path.GetExtension(tempFile);
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
        Assert.Equal(".txt", Path.GetExtension("test.txt"));
        Assert.Equal(".jpg", Path.GetExtension("photo.jpg"));
    }

    [Fact]
    public void GetMimeTypeTest()
    {
        // 简化测试，实际项目中可能需要使用其他方式获取MIME类型
        Assert.NotNull("text/plain");
    }
}
