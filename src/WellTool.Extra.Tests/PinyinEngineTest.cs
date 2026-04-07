using System;
using System.Text;
using WellTool.Extra.Pinyin;

namespace WellTool.Extra.Tests;

/// <summary>
/// PinyinEngine 接口测试类
/// </summary>
public class PinyinEngineTest
{
    /// <summary>
    /// 测试获取单字符拼音 - 非汉字字符
    /// </summary>
    [Fact]
    public void TestGetPinyin_SingleChar_NonChinese()
    {
        var engine = PinyinFactory.Get();
        
        // 测试英文字母返回原字符
        Assert.Equal("A", engine.GetPinyin('A'));
        Assert.Equal("z", engine.GetPinyin('z'));
        
        // 测试数字返回原字符
        Assert.Equal("1", engine.GetPinyin('1'));
    }

    /// <summary>
    /// 测试获取单字符拼音 - 带声调标记
    /// </summary>
    [Fact]
    public void TestGetPinyin_SingleChar_WithTone()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetPinyin('中', true);
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试获取字符串拼音 - 带分隔符
    /// </summary>
    [Fact]
    public void TestGetPinyin_String_WithSeparator()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetPinyin("hello", "-");
        Assert.Equal("hello", result);
    }

    /// <summary>
    /// 测试获取字符串拼音 - 带声调
    /// </summary>
    [Fact]
    public void TestGetPinyin_String_WithTone()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetPinyin("hello", "-", true);
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试获取首字母 - 单字符
    /// </summary>
    [Fact]
    public void TestGetFirstLetter_SingleChar()
    {
        var engine = PinyinFactory.Get();
        
        // 测试英文字母返回小写形式
        Assert.Equal('a', engine.GetFirstLetter('A'));
        Assert.Equal('z', engine.GetFirstLetter('z'));
    }

    /// <summary>
    /// 测试获取首字母 - 字符串
    /// </summary>
    [Fact]
    public void TestGetFirstLetter_String()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetFirstLetter("hello", "-");
        Assert.Equal("hello", result);
    }

    /// <summary>
    /// 测试获取首字母 - 混合字符串
    /// </summary>
    [Fact]
    public void TestGetFirstLetter_MixedString()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetFirstLetter("Hello123", "-");
        Assert.NotNull(result);
    }
}

/// <summary>
/// PinyinFactory 工厂类测试
/// </summary>
public class PinyinFactoryTest
{
    /// <summary>
    /// 测试获取默认引擎实例
    /// </summary>
    [Fact]
    public void TestGet_DefaultInstance()
    {
        var engine1 = PinyinFactory.Get();
        var engine2 = PinyinFactory.Get();
        
        // 默认应该返回同一个实例
        Assert.NotNull(engine1);
        Assert.NotNull(engine2);
        Assert.Same(engine1, engine2);
    }

    /// <summary>
    /// 测试设置自定义引擎
    /// </summary>
    [Fact]
    public void TestSet_CustomEngine()
    {
        // 使用自定义引擎
        var customEngine = new CustomPinyinEngine();
        PinyinFactory.Set(customEngine);
        
        var engine = PinyinFactory.Get();
        Assert.Same(customEngine, engine);
        
        // 恢复默认引擎
        PinyinFactory.Set(null);
    }

    /// <summary>
    /// 测试自定义引擎的GetPinyin方法
    /// </summary>
    [Fact]
    public void TestCustomEngine_GetPinyin()
    {
        var customEngine = new CustomPinyinEngine();
        PinyinFactory.Set(customEngine);
        
        var result = customEngine.GetPinyin('A');
        Assert.Equal("CustomPinyin", result);
        
        // 恢复默认引擎
        PinyinFactory.Set(null);
    }

    /// <summary>
    /// 测试空字符串处理
    /// </summary>
    [Fact]
    public void TestGetPinyin_EmptyString()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetPinyin("", "-");
        Assert.Equal("", result);
    }

    /// <summary>
    /// 测试null字符串处理
    /// </summary>
    [Fact]
    public void TestGetPinyin_NullString()
    {
        var engine = PinyinFactory.Get();
        
        var result = engine.GetPinyin(null, "-");
        Assert.Null(result);
    }
}

/// <summary>
/// 自定义拼音引擎实现（用于测试）
/// </summary>
internal class CustomPinyinEngine : PinyinEngine
{
    public string GetPinyin(char c)
    {
        return "CustomPinyin";
    }

    public string GetPinyin(char c, bool tone)
    {
        return "CustomPinyin";
    }

    public string GetPinyin(string str, string separator)
    {
        return "CustomPinyin";
    }

    public string GetPinyin(string str, string separator, bool tone)
    {
        return "CustomPinyin";
    }

    public char GetFirstLetter(char c)
    {
        return 'C';
    }

    public string GetFirstLetter(string str, string separator)
    {
        return "C";
    }
}
