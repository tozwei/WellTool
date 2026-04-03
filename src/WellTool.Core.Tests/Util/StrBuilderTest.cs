using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrBuilderTest
{
    [Fact]
    public void AppendTest()
    {
        var builder = new StrBuilder();
        builder.Append("Hello");
        builder.Append(" World");
        Assert.Equal("Hello World", builder.ToString());
    }

    [Fact]
    public void AppendLineTest()
    {
        var builder = new StrBuilder();
        builder.Append("Hello");
        builder.AppendLine();
        builder.Append("World");
        Assert.Equal("Hello\r\nWorld", builder.ToString());
    }

    [Fact]
    public void AppendFormatTest()
    {
        var builder = new StrBuilder();
        builder.AppendFormat("Hello {0}", "World");
        Assert.Equal("Hello World", builder.ToString());
    }

    [Fact]
    public void InsertTest()
    {
        var builder = new StrBuilder("Hello");
        builder.Insert(5, " World");
        Assert.Equal("Hello World", builder.ToString());
    }

    [Fact]
    public void DeleteTest()
    {
        var builder = new StrBuilder("Hello World");
        builder.Delete(5, 11);
        Assert.Equal("Hello", builder.ToString());
    }

    [Fact]
    public void ReplaceTest()
    {
        var builder = new StrBuilder("Hello World");
        builder.Replace("World", "Universe");
        Assert.Equal("Hello Universe", builder.ToString());
    }

    [Fact]
    public void ReverseTest()
    {
        var builder = new StrBuilder("Hello");
        builder.Reverse();
        Assert.Equal("olleH", builder.ToString());
    }

    [Fact]
    public void ClearTest()
    {
        var builder = new StrBuilder("Hello");
        builder.Clear();
        Assert.Equal("", builder.ToString());
    }

    [Fact]
    public void LengthTest()
    {
        var builder = new StrBuilder("Hello");
        Assert.Equal(5, builder.Length);
    }

    [Fact]
    public void IsEmptyTest()
    {
        var builder = new StrBuilder();
        Assert.True(builder.IsEmpty());

        builder.Append("Hello");
        Assert.False(builder.IsEmpty());
    }
}
