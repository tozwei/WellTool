using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharSequenceUtilLastTest
{
    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(CharSequenceUtil.IsEmpty(null));
        Assert.False(CharSequenceUtil.IsEmpty("abc"));
    }

    [Fact]
    public void TrimTest()
    {
        Assert.Equal("abc", CharSequenceUtil.Trim(" abc "));
    }

    [Fact]
    public void SubTest()
    {
        Assert.Equal("bc", CharSequenceUtil.Sub("abcde", 1, 3));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(CharSequenceUtil.Contains("abc", "b"));
    }
}
