using System;
using System.Drawing;
using System.Text;
using WellTool.Extra.Qrcode;

namespace WellTool.Extra.Tests;

/// <summary>
/// QrConfig 二维码配置测试类
/// </summary>
public class QrConfigTest
{
    /// <summary>
    /// 测试创建默认配置
    /// </summary>
    [Fact]
    public void TestCreate()
    {
        var config = QrConfig.Create();
        Assert.NotNull(config);
    }

    /// <summary>
    /// 测试无参构造 - 默认尺寸
    /// </summary>
    [Fact]
    public void TestConstructor_Default()
    {
        var config = new QrConfig();
        Assert.Equal(300, config.GetWidth());
        Assert.Equal(300, config.GetHeight());
    }

    /// <summary>
    /// 测试指定尺寸构造
    /// </summary>
    [Fact]
    public void TestConstructor_WithSize()
    {
        var config = new QrConfig(200, 200);
        Assert.Equal(200, config.GetWidth());
        Assert.Equal(200, config.GetHeight());
    }

    /// <summary>
    /// 测试设置宽度
    /// </summary>
    [Fact]
    public void TestSetWidth()
    {
        var config = new QrConfig();
        var result = config.SetWidth(250);
        
        Assert.Same(config, result);
        Assert.Equal(250, config.GetWidth());
    }

    /// <summary>
    /// 测试设置高度
    /// </summary>
    [Fact]
    public void TestSetHeight()
    {
        var config = new QrConfig();
        var result = config.SetHeight(350);
        
        Assert.Same(config, result);
        Assert.Equal(350, config.GetHeight());
    }

    /// <summary>
    /// 测试设置前景色
    /// </summary>
    [Fact]
    public void TestSetForeColor()
    {
        var config = new QrConfig();
        var result = config.SetForeColor(Color.Blue);
        
        Assert.Same(config, result);
        Assert.NotNull(config.GetForeColor());
    }

    /// <summary>
    /// 测试设置背景色
    /// </summary>
    [Fact]
    public void TestSetBackColor()
    {
        var config = new QrConfig();
        var result = config.SetBackColor(Color.White);
        
        Assert.Same(config, result);
        Assert.NotNull(config.GetBackColor());
    }

    /// <summary>
    /// 测试设置透明背景
    /// </summary>
    [Fact]
    public void TestSetBackColor_Null()
    {
        var config = new QrConfig();
        var result = config.SetBackColor(null);
        
        Assert.Same(config, result);
        Assert.Null(config.GetBackColor());
    }

    /// <summary>
    /// 测试设置边距
    /// </summary>
    [Fact]
    public void TestSetMargin()
    {
        var config = new QrConfig();
        var result = config.SetMargin(4);
        
        Assert.Same(config, result);
        Assert.Equal(4, config.GetMargin());
    }

    /// <summary>
    /// 测试设置二维码版本
    /// </summary>
    [Fact]
    public void TestSetQrVersion()
    {
        var config = new QrConfig();
        var result = config.SetQrVersion(10);
        
        Assert.Same(config, result);
        Assert.Equal(10, config.GetQrVersion());
    }

    /// <summary>
    /// 测试设置二维码版本为null
    /// </summary>
    [Fact]
    public void TestSetQrVersion_Null()
    {
        var config = new QrConfig();
        config.SetQrVersion(null);
        
        Assert.Null(config.GetQrVersion());
    }

    /// <summary>
    /// 测试设置纠错级别
    /// </summary>
    [Theory]
    [InlineData(ErrorCorrectionLevel.L)]
    [InlineData(ErrorCorrectionLevel.M)]
    [InlineData(ErrorCorrectionLevel.Q)]
    [InlineData(ErrorCorrectionLevel.H)]
    public void TestSetErrorCorrection(ErrorCorrectionLevel level)
    {
        var config = new QrConfig();
        var result = config.SetErrorCorrection(level);
        
        Assert.Same(config, result);
        Assert.Equal(level, config.GetErrorCorrection());
    }

    /// <summary>
    /// 测试默认纠错级别
    /// </summary>
    [Fact]
    public void TestDefaultErrorCorrection()
    {
        var config = new QrConfig();
        Assert.Equal(ErrorCorrectionLevel.M, config.GetErrorCorrection());
    }

    /// <summary>
    /// 测试设置编码
    /// </summary>
    [Fact]
    public void TestSetCharset()
    {
        var config = new QrConfig();
        var result = config.SetCharset(Encoding.UTF8);
        
        Assert.Same(config, result);
        Assert.Equal(Encoding.UTF8, config.GetCharset());
    }

    /// <summary>
    /// 测试设置Logo
    /// </summary>
    [Fact]
    public void TestSetImg()
    {
        var config = new QrConfig();
        using var bitmap = new Bitmap(50, 50);
        var result = config.SetImg(bitmap);
        
        Assert.Same(config, result);
        Assert.NotNull(config.GetImg());
    }

    /// <summary>
    /// 测试设置Logo缩放比例
    /// </summary>
    [Fact]
    public void TestSetRatio()
    {
        var config = new QrConfig();
        var result = config.SetRatio(5);
        
        Assert.Same(config, result);
        Assert.Equal(5, config.GetRatio());
    }

    /// <summary>
    /// 测试设置Logo圆角弧度
    /// </summary>
    [Fact]
    public void TestSetRound()
    {
        var config = new QrConfig();
        var result = config.SetRound(0.5);
        
        Assert.Same(config, result);
        Assert.Equal(0.5, config.GetRound());
    }

    /// <summary>
    /// 测试默认圆角弧度
    /// </summary>
    [Fact]
    public void TestDefaultRound()
    {
        var config = new QrConfig();
        Assert.Equal(0.3, config.GetRound());
    }

    /// <summary>
    /// 测试Fluent API链式调用
    /// </summary>
    [Fact]
    public void TestFluentApi()
    {
        var config = new QrConfig(200, 200)
            .SetWidth(250)
            .SetHeight(300)
            .SetForeColor(Color.Black)
            .SetBackColor(Color.White)
            .SetMargin(3)
            .SetQrVersion(5)
            .SetErrorCorrection(ErrorCorrectionLevel.H)
            .SetCharset(Encoding.UTF8)
            .SetRatio(8)
            .SetRound(0.4);
        
        Assert.Equal(250, config.GetWidth());
        Assert.Equal(300, config.GetHeight());
        Assert.Equal(3, config.GetMargin());
        Assert.Equal(5, config.GetQrVersion());
        Assert.Equal(ErrorCorrectionLevel.H, config.GetErrorCorrection());
        Assert.Equal(8, config.GetRatio());
        Assert.Equal(0.4, config.GetRound());
    }
}

/// <summary>
/// ErrorCorrectionLevel 枚举测试
/// </summary>
public class ErrorCorrectionLevelTest
{
    /// <summary>
    /// 测试纠错级别枚举值
    /// </summary>
    [Fact]
    public void TestEnumValues()
    {
        Assert.Equal(0, (int)ErrorCorrectionLevel.L);
        Assert.Equal(1, (int)ErrorCorrectionLevel.M);
        Assert.Equal(2, (int)ErrorCorrectionLevel.Q);
        Assert.Equal(3, (int)ErrorCorrectionLevel.H);
    }

    /// <summary>
    /// 测试每个纠错级别
    /// </summary>
    [Theory]
    [InlineData(ErrorCorrectionLevel.L)]
    [InlineData(ErrorCorrectionLevel.M)]
    [InlineData(ErrorCorrectionLevel.Q)]
    [InlineData(ErrorCorrectionLevel.H)]
    public void TestEachLevel(ErrorCorrectionLevel level)
    {
        var config = new QrConfig();
        config.SetErrorCorrection(level);
        Assert.Equal(level, config.GetErrorCorrection());
    }
}

/// <summary>
/// QrCodeException 二维码异常测试
/// </summary>
public class QrCodeExceptionTest
{
    /// <summary>
    /// 测试构造异常 - 消息
    /// </summary>
    [Fact]
    public void TestConstructor_Message()
    {
        var ex = new QrCodeException("QR Code generation failed");
        Assert.Equal("QR Code generation failed", ex.Message);
    }

    /// <summary>
    /// 测试构造异常 - 消息和内部异常
    /// </summary>
    [Fact]
    public void TestConstructor_MessageAndInner()
    {
        var innerEx = new InvalidOperationException("Invalid data");
        var ex = new QrCodeException("QR Code error", innerEx);
        
        Assert.Equal("QR Code error", ex.Message);
        Assert.Same(innerEx, ex.InnerException);
    }

    /// <summary>
    /// 测试异常可抛出和捕获
    /// </summary>
    [Fact]
    public void TestCanThrowAndCatch()
    {
        Assert.Throws<QrCodeException>(() =>
        {
            throw new QrCodeException("QR error");
        });
    }
}
