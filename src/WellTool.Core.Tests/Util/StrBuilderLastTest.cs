using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrBuilderLastTest
{
    [Fact]
    public void BuilderTest()
    {
        var builder = new StrBuilder();
        builder.Append("Hello").Append(" ").Append("World");
        Assert.Equal("Hello World", builder.ToString());
    }

    [Fact]
    public void ClearTest()
    {
        var builder = new StrBuilder("Hello");
        builder.Clear();
        Assert.Equal("", builder.ToString());
    }

    [Fact]
    public void InsertTest()
    {
        var builder = new StrBuilder("HelloWorld");
        builder.Insert(5, " ");
        Assert.Equal("Hello World", builder.ToString());
    }

    [Fact]
    public void DeleteTest()
    {
        var builder = new StrBuilder("HelloWorld");
        builder.Delete(5, 10);
        Assert.Equal("Hello", builder.ToString());
    }

    [Fact]
    public void ReverseTest()
    {
        var builder = new StrBuilder("Hello");
        builder.Reverse();
        Assert.Equal("olleH", builder.ToString());
    }

    [Fact]
    public void ReplaceTest()
    {
        var builder = new StrBuilder("HelloWorld");
        builder.Replace("World", "!");
        Assert.Equal("Hello!", builder.ToString());
    }

    [Fact]
    public void LengthTest()
    {
        var builder = new StrBuilder("Hello");
        Assert.Equal(5, builder.Length);
    }
}
