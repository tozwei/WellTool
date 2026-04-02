using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tests;

/// <summary>
/// TokenizerEngine 接口测试类
/// </summary>
public class TokenizerEngineTest
{
    /// <summary>
    /// 测试创建默认分词引擎
    /// </summary>
    [Fact]
    public void TestCreate_Default()
    {
        var engine = TokenizerFactory.Create();
        Assert.NotNull(engine);
    }

    /// <summary>
    /// 测试分词处理 - 简单文本
    /// </summary>
    [Fact]
    public void TestParse_SimpleText()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello world");
        
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试分词处理 - 空文本
    /// </summary>
    [Fact]
    public void TestParse_EmptyText()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("");
        
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试分词处理 - 多词文本
    /// </summary>
    [Fact]
    public void TestParse_MultipleWords()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("word1 word2 word3");
        
        Assert.NotNull(result);
    }
}

/// <summary>
/// TokenizerFactory 工厂类测试
/// </summary>
public class TokenizerFactoryTest
{
    /// <summary>
    /// 测试工厂创建引擎
    /// </summary>
    [Fact]
    public void TestCreate()
    {
        var engine = TokenizerFactory.Create();
        Assert.NotNull(engine);
    }
}

/// <summary>
/// Word 接口测试类
/// </summary>
public class WordTest
{
    /// <summary>
    /// 测试单词文本获取
    /// </summary>
    [Fact]
    public void TestGetText()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello");
        
        foreach (var word in result)
        {
            Assert.NotNull(word.GetText());
        }
    }

    /// <summary>
    /// 测试单词起始位置
    /// </summary>
    [Fact]
    public void TestGetStartOffset()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello");
        
        foreach (var word in result)
        {
            Assert.True(word.GetStartOffset() >= 0);
        }
    }

    /// <summary>
    /// 测试单词结束位置
    /// </summary>
    [Fact]
    public void TestGetEndOffset()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello");
        
        foreach (var word in result)
        {
            Assert.True(word.GetEndOffset() >= 0);
            Assert.True(word.GetEndOffset() >= word.GetStartOffset());
        }
    }

    /// <summary>
    /// 测试单词比较
    /// </summary>
    [Fact]
    public void TestCompareTo()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("apple banana");
        
        var words = result.ToList();
        if (words.Count >= 2)
        {
            // 比较两个单词
            var comparison = words[0].CompareTo(words[1]);
            Assert.True(comparison is int);
        }
    }
}

/// <summary>
/// Result 接口测试类
/// </summary>
public class ResultTest
{
    /// <summary>
    /// 测试结果可枚举
    /// </summary>
    [Fact]
    public void TestResult_IsEnumerable()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello world");
        
        Assert.NotNull(result);
        
        var count = 0;
        foreach (var word in result)
        {
            Assert.NotNull(word);
            count++;
        }
        Assert.True(count > 0);
    }

    /// <summary>
    /// 测试结果作为IEnumerable使用
    /// </summary>
    [Fact]
    public void TestResult_AsIEnumerable()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("a b c");
        
        var words = result.ToList();
        Assert.NotNull(words);
    }
}

/// <summary>
/// AbstractResult 抽象类测试
/// </summary>
public class AbstractResultTest
{
    /// <summary>
    /// 测试枚举器获取
    /// </summary>
    [Fact]
    public void TestGetEnumerator()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("test words");
        
        var enumerator = result.GetEnumerator();
        Assert.NotNull(enumerator);
    }

    /// <summary>
    /// 测试空文本的枚举
    /// </summary>
    [Fact]
    public void TestEmptyTextEnumeration()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("");
        
        var words = result.ToList();
        Assert.Empty(words);
    }

    /// <summary>
    /// 测试单字符文本的枚举
    /// </summary>
    [Fact]
    public void TestSingleCharEnumeration()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("a");
        
        var words = result.ToList();
        Assert.Single(words);
    }

    /// <summary>
    /// 测试带特殊字符的文本
    /// </summary>
    [Fact]
    public void TestSpecialCharacters()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello-world test_file");
        
        var words = result.ToList();
        Assert.NotEmpty(words);
    }

    /// <summary>
    /// 测试单词位置信息
    /// </summary>
    [Fact]
    public void TestWordPositionInfo()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("hello world");
        
        var words = result.ToList();
        Assert.Equal(2, words.Count);
        
        // 验证位置信息的合理性
        for (int i = 0; i < words.Count; i++)
        {
            Assert.True(words[i].GetStartOffset() >= 0);
            Assert.True(words[i].GetEndOffset() > words[i].GetStartOffset());
        }
    }

    /// <summary>
    /// 测试枚举器遍历
    /// </summary>
    [Fact]
    public void TestEnumeratorTraversal()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("word1 word2 word3");
        
        var words = new System.Collections.Generic.List<Word>();
        var enumerator = result.GetEnumerator();
        
        while (enumerator.MoveNext())
        {
            words.Add(enumerator.Current);
        }
        
        Assert.Equal(3, words.Count);
    }

    /// <summary>
    /// 测试重复枚举
    /// </summary>
    [Fact]
    public void TestRepeatedEnumeration()
    {
        var engine = TokenizerFactory.Create();
        var result = engine.Parse("test");
        
        // 第一次枚举
        var count1 = result.Count();
        // 第二次枚举
        var count2 = result.Count();
        
        Assert.Equal(count1, count2);
    }
}
