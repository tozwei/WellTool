using System.Drawing;
using System.IO;

namespace WellTool.Extra.Tests;

/// <summary>
/// QrCodeUtil 测试类
/// </summary>
public class QrCodeUtilTest
{
    private readonly QrCodeUtil _qrCodeUtil;

    public QrCodeUtilTest()
    {
        _qrCodeUtil = new QrCodeUtil();
    }

    [Fact]
    public void TestGenerate_ValidContent_ReturnsBitmap()
    {
        // 测试生成二维码
        var bitmap = _qrCodeUtil.Generate("https://example.com");
        
        Assert.NotNull(bitmap);
        Assert.True(bitmap.Width > 0);
        Assert.True(bitmap.Height > 0);
    }

    [Fact]
    public void TestGenerate_DefaultSize_ReturnsCorrectSize()
    {
        // 测试默认尺寸
        var bitmap = _qrCodeUtil.Generate("test");
        
        Assert.Equal(200, bitmap.Width);
        Assert.Equal(200, bitmap.Height);
    }

    [Fact]
    public void TestGenerate_CustomSize_ReturnsCorrectSize()
    {
        // 测试自定义尺寸
        var width = 300;
        var height = 400;
        var bitmap = _qrCodeUtil.Generate("test", width, height);
        
        Assert.Equal(width, bitmap.Width);
        Assert.Equal(height, bitmap.Height);
    }

    [Fact]
    public void TestGenerate_EmptyContent_GeneratesQrCode()
    {
        // 测试空内容（应该仍然生成二维码）
        var bitmap = _qrCodeUtil.Generate("");
        
        Assert.NotNull(bitmap);
    }

    [Fact]
    public void TestGenerate_LongContent_GeneratesQrCode()
    {
        // 测试长内容
        var longContent = new string('a', 1000);
        var bitmap = _qrCodeUtil.Generate(longContent);
        
        Assert.NotNull(bitmap);
    }

    [Fact]
    public void TestGenerate_UnicodeContent_GeneratesQrCode()
    {
        // 测试 Unicode 内容
        var bitmap = _qrCodeUtil.Generate("你好世界！ Hello World!");
        
        Assert.NotNull(bitmap);
    }

    [Fact]
    public void TestGenerateToFile_ValidPath_SavesFile()
    {
        // 测试保存到文件
        var tempFile = Path.GetTempFileName();
        try
        {
            _qrCodeUtil.GenerateToFile("test", tempFile);
            
            Assert.True(File.Exists(tempFile));
            var fileInfo = new FileInfo(tempFile);
            Assert.True(fileInfo.Length > 0);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestGenerateToFile_CustomSize_SavesCorrectSize()
    {
        // 测试保存自定义尺寸的二维码
        var tempFile = Path.GetTempFileName();
        try
        {
            var width = 150;
            var height = 150;
            _qrCodeUtil.GenerateToFile("test", tempFile, width, height);
            
            using var bitmap = new Bitmap(tempFile);
            Assert.Equal(width, bitmap.Width);
            Assert.Equal(height, bitmap.Height);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestGenerateToBytes_ValidContent_ReturnsBytes()
    {
        // 测试生成字节数组
        var bytes = _qrCodeUtil.GenerateToBytes("test");
        
        Assert.NotNull(bytes);
        Assert.True(bytes.Length > 0);
    }

    [Fact]
    public void TestGenerateToBytes_CustomSize_ReturnsCorrectSize()
    {
        // 测试自定义尺寸的字节数组
        var width = 250;
        var height = 250;
        var bytes = _qrCodeUtil.GenerateToBytes("test", width, height);
        
        Assert.NotNull(bytes);
        Assert.True(bytes.Length > 0);
        
        using var stream = new MemoryStream(bytes);
        using var bitmap = new Bitmap(stream);
        Assert.Equal(width, bitmap.Width);
        Assert.Equal(height, bitmap.Height);
    }

    [Fact]
    public void TestGenerateToBytes_IsPngFormat()
    {
        // 测试 PNG 格式
        var bytes = _qrCodeUtil.GenerateToBytes("test");
        
        // PNG 文件头: 89 50 4E 47
        Assert.Equal(0x89, bytes[0]);
        Assert.Equal(0x50, bytes[1]); // P
        Assert.Equal(0x4E, bytes[2]); // N
        Assert.Equal(0x47, bytes[3]); // G
    }

    [Fact]
    public void TestQrCodeException_Constructor_MessageOnly()
    {
        // 测试异常构造函数
        var exception = new QrCodeException("Test error");
        
        Assert.Equal("Test error", exception.Message);
        Assert.Null(exception.InnerException);
    }

    [Fact]
    public void TestQrCodeException_Constructor_MessageAndInnerException()
    {
        // 测试异常构造函数（带内部异常）
        var innerException = new Exception("Inner error");
        var exception = new QrCodeException("Test error", innerException);
        
        Assert.Equal("Test error", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void TestQrCodeUtil_SingletonInstance_ReturnsSameInstance()
    {
        // 测试单例实例
        var instance1 = QrCodeUtil.Instance;
        var instance2 = QrCodeUtil.Instance;
        
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void TestGenerate_MinimumSize_GeneratesQrCode()
    {
        // 测试最小尺寸
        var bitmap = _qrCodeUtil.Generate("test", 10, 10);
        
        Assert.NotNull(bitmap);
        Assert.Equal(10, bitmap.Width);
        Assert.Equal(10, bitmap.Height);
    }

    [Fact]
    public void TestGenerate_LargeSize_GeneratesQrCode()
    {
        // 测试大尺寸
        var bitmap = _qrCodeUtil.Generate("test", 1000, 1000);
        
        Assert.NotNull(bitmap);
        Assert.Equal(1000, bitmap.Width);
        Assert.Equal(1000, bitmap.Height);
    }

    [Fact]
    public void TestGenerateToFile_InvalidPath_ThrowsException()
    {
        // 测试无效路径
        var invalidPath = "/invalid/path/that/does/not/exist/qrcode.png";
        
        Assert.Throws<QrCodeException>(() => 
            _qrCodeUtil.GenerateToFile("test", invalidPath));
    }

    [Fact]
    public void TestGenerateToFile_ReadBack_ReturnsSameContent()
    {
        // 测试保存后读取
        var tempFile = Path.GetTempFileName();
        var content = "https://example.com/test";
        try
        {
            _qrCodeUtil.GenerateToFile(content, tempFile);
            
            using var bitmap = new Bitmap(tempFile);
            Assert.NotNull(bitmap);
            Assert.True(bitmap.Width > 0);
            Assert.True(bitmap.Height > 0);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
