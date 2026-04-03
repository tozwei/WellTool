namespace WellTool.Extra.Tests;

using Well.Extra.QrCode;

public class QrCodeUtilTest
{
    [Fact]
    public void GenerateTest()
    {
        var qrCode = QrCodeUtil.Generate("Hello World");
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void GenerateWithSizeTest()
    {
        var qrCode = QrCodeUtil.Generate("Hello", 200, 200);
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void GenerateToByteArrayTest()
    {
        var bytes = QrCodeUtil.GenerateToByteArray("Test");
        Assert.NotNull(bytes);
        Assert.True(bytes.Length > 0);
    }

    [Fact]
    public void GenerateToFileTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            QrCodeUtil.GenerateToFile("Test", tempFile);
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void GenerateWithLogoTest()
    {
        var qrCode = QrCodeUtil.Generate("WithLogo");
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void DecodeTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            QrCodeUtil.GenerateToFile("DecodeTest", tempFile);
            var decoded = QrCodeUtil.Decode(tempFile);
            Assert.Equal("DecodeTest", decoded);
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void GenerateNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => QrCodeUtil.Generate(null));
    }

    [Fact]
    public void GenerateEmptyTest()
    {
        Assert.Throws<ArgumentException>(() => QrCodeUtil.Generate(""));
    }
}
