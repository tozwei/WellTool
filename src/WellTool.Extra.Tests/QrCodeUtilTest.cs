namespace WellTool.Extra.Tests;

using WellTool.Extra;

public class QrCodeUtilTest
{
    [Fact]
    public void GenerateTest()
    {
        var qrCode = QrCodeUtil.Instance.Generate("Hello World");
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void GenerateWithSizeTest()
    {
        var qrCode = QrCodeUtil.Instance.Generate("Hello", 200, 200);
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void GenerateToByteArrayTest()
    {
        var bytes = QrCodeUtil.Instance.GenerateToByteArray("Test");
        Assert.NotNull(bytes);
        Assert.True(bytes.Length > 0);
    }

    [Fact]
    public void GenerateToFileTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            QrCodeUtil.Instance.GenerateToFile("Test", tempFile);
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
        var qrCode = QrCodeUtil.Instance.Generate("WithLogo");
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void DecodeTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            QrCodeUtil.Instance.GenerateToFile("DecodeTest", tempFile);
            var decoded = QrCodeUtil.Instance.Decode(tempFile);
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
        Assert.Throws<ArgumentNullException>(() => QrCodeUtil.Instance.Generate(null));
    }

    [Fact]
    public void GenerateEmptyTest()
    {
        Assert.Throws<ArgumentException>(() => QrCodeUtil.Instance.Generate(""));
    }
}
