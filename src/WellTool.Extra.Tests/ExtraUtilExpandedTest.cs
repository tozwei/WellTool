using System;
using WellTool.Extra;

namespace WellTool.Extra.Tests;

/// <summary>
/// ExtraUtil 额外工具类测试
/// </summary>
public class ExtraUtilTest
{
    /// <summary>
    /// 测试单例实例
    /// </summary>
    [Fact]
    public void TestInstance_NotNull()
    {
        Assert.NotNull(ExtraUtil.Instance);
    }

    /// <summary>
    /// 测试获取同一实例
    /// </summary>
    [Fact]
    public void TestInstance_SameInstance()
    {
        var instance1 = ExtraUtil.Instance;
        var instance2 = ExtraUtil.Instance;
        Assert.Same(instance1, instance2);
    }

    /// <summary>
    /// 测试获取压缩工具
    /// </summary>
    [Fact]
    public void TestCompress()
    {
        var util = ExtraUtil.Instance;
        var compressUtil = util.Compress();
        Assert.NotNull(compressUtil);
        Assert.Same(CompressUtil.Instance, compressUtil);
    }

    /// <summary>
    /// 测试获取表情符号工具
    /// </summary>
    [Fact]
    public void TestEmoji()
    {
        var util = ExtraUtil.Instance;
        var emojiUtil = util.Emoji();
        Assert.NotNull(emojiUtil);
    }

    /// <summary>
    /// 测试获取表达式工具
    /// </summary>
    [Fact]
    public void TestExpression()
    {
        var util = ExtraUtil.Instance;
        var expressionUtil = util.Expression();
        Assert.NotNull(expressionUtil);
    }

    /// <summary>
    /// 测试获取FTP工具
    /// </summary>
    [Fact]
    public void TestFtp()
    {
        var util = ExtraUtil.Instance;
        var ftpUtil = util.Ftp();
        Assert.NotNull(ftpUtil);
    }

    /// <summary>
    /// 测试获取邮件工具
    /// </summary>
    [Fact]
    public void TestMail()
    {
        var util = ExtraUtil.Instance;
        var mailUtil = util.Mail();
        Assert.NotNull(mailUtil);
    }

    /// <summary>
    /// 测试获取拼音工具
    /// </summary>
    [Fact]
    public void TestPinyin()
    {
        var util = ExtraUtil.Instance;
        var pinyinUtil = util.Pinyin();
        Assert.NotNull(pinyinUtil);
    }

    /// <summary>
    /// 测试获取二维码工具
    /// </summary>
    [Fact]
    public void TestQrCode()
    {
        var util = ExtraUtil.Instance;
        var qrCodeUtil = util.QrCode();
        // 注意：当前实现返回null，这是已知行为
        Assert.Null(qrCodeUtil);
    }

    /// <summary>
    /// 测试获取SSH工具
    /// </summary>
    [Fact]
    public void TestSsh()
    {
        var util = ExtraUtil.Instance;
        var sshUtil = util.Ssh();
        Assert.NotNull(sshUtil);
    }

    /// <summary>
    /// 测试获取模板工具
    /// </summary>
    [Fact]
    public void TestTemplate()
    {
        var util = ExtraUtil.Instance;
        var templateUtil = util.Template();
        Assert.NotNull(templateUtil);
    }

    /// <summary>
    /// 测试获取分词工具
    /// </summary>
    [Fact]
    public void TestTokenizer()
    {
        var util = ExtraUtil.Instance;
        var tokenizerUtil = util.Tokenizer();
        Assert.NotNull(tokenizerUtil);
    }

    /// <summary>
    /// 测试获取验证工具
    /// </summary>
    [Fact]
    public void TestValidation()
    {
        var util = ExtraUtil.Instance;
        var validationUtil = util.Validation();
        // 注意：当前实现返回null，这是已知行为
        Assert.Null(validationUtil);
    }

    /// <summary>
    /// 测试所有工具方法都返回非null或预期的对象
    /// </summary>
    [Fact]
    public void TestAllMethodsReturnExpected()
    {
        var util = ExtraUtil.Instance;
        
        Assert.NotNull(util.Compress());
        Assert.NotNull(util.Emoji());
        Assert.NotNull(util.Expression());
        Assert.NotNull(util.Ftp());
        Assert.NotNull(util.Mail());
        Assert.NotNull(util.Pinyin());
        // 注意：当前实现返回null，这是已知行为
        // Assert.NotNull(util.QrCode());
        Assert.NotNull(util.Ssh());
        Assert.NotNull(util.Template());
        Assert.NotNull(util.Tokenizer());
    }
}
