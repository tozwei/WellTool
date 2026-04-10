using Xunit;
using System.IO;
using WellTool.Core.IO;

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
        // 测试常见文件类型的MIME类型
        Assert.Equal("image/jpeg", FileUtil.GetMimeType("test.jpg"));
        Assert.Equal("image/jpeg", FileUtil.GetMimeType("test.jpeg"));
        Assert.Equal("image/png", FileUtil.GetMimeType("test.png"));
        Assert.Equal("image/gif", FileUtil.GetMimeType("test.gif"));
        Assert.Equal("text/html", FileUtil.GetMimeType("test.html"));
        Assert.Equal("text/css", FileUtil.GetMimeType("test.css"));
        Assert.Equal("text/javascript", FileUtil.GetMimeType("test.js"));
        Assert.Equal("application/msword", FileUtil.GetMimeType("test.doc"));
        Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", FileUtil.GetMimeType("test.docx"));
        Assert.Equal("application/vnd.ms-excel", FileUtil.GetMimeType("test.xls"));
        Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileUtil.GetMimeType("test.xlsx"));
        Assert.Equal("application/pdf", FileUtil.GetMimeType("test.pdf"));
        Assert.Equal("application/zip", FileUtil.GetMimeType("test.zip"));
        
        // 测试未知文件类型
        Assert.Equal("application/octet-stream", FileUtil.GetMimeType("test.unknown"));
    }
}
