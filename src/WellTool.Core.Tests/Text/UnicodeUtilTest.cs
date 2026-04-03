using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class UnicodeUtilTest
{
    [Fact]
    public void ConvertTest()
    {
        var s = UnicodeUtil.ToUnicode("aaa123中文", true);
        Assert.Equal("aaa123\\u4e2d\\u6587", s);

        var s1 = UnicodeUtil.ToString(s);
        Assert.Equal("aaa123中文", s1);
    }

    [Fact]
    public void ConvertTest2()
    {
        var str = "aaaa\\u0026bbbb\\u0026cccc";
        var unicode = UnicodeUtil.ToString(str);
        Assert.Equal("aaaa&bbbb&cccc", unicode);
    }

    [Fact]
    public void ConvertTest3()
    {
        var str = "aaa\\u111";
        var res = UnicodeUtil.ToString(str);
        Assert.Equal("aaa\\u111", res);
    }

    [Fact]
    public void ConvertTest4()
    {
        var str = "aaa\\U4e2d\\u6587\\u111\\urtyu\\u0026";
        var res = UnicodeUtil.ToString(str);
        Assert.Equal("aaa中文\\u111\\urtyu&", res);
    }

    [Fact]
    public void IssueI50MI6Test()
    {
        var s = UnicodeUtil.ToUnicode("烟", true);
        Assert.Equal("\\u70df", s);
    }
}
