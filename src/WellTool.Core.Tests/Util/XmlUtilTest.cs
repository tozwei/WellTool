
using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class XmlUtilTest
{
    [Fact]
    public void EscapeTest()
    {
        var result = XmlUtil.Escape("<tag>value & 'test'</tag>");
        Assert.Equal("&lt;tag&gt;value &amp; &apos;test&apos;&lt;/tag&gt;", result);
    }

    [Fact]
    public void UnescapeTest()
    {
        var result = XmlUtil.Unescape("&lt;tag&gt;value &amp; &apos;test&apos;&lt;/tag&gt;");
        Assert.Equal("<tag>value & 'test'</tag>", result);
    }

    [Fact]
    public void IsXmlSafeTest()
    {
        Assert.True(XmlUtil.IsXmlSafe('a'));
        Assert.True(XmlUtil.IsXmlSafe('\n'));
        Assert.False(XmlUtil.IsXmlSafe('\x01'));
    }

    [Fact]
    public void EscapeInvalidTest()
    {
        var input = "hello\x01world";
        var result = XmlUtil.EscapeInvalid(input);
        Assert.Equal("helloworld", result);
    }
}
