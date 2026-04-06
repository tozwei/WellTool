
using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class XmlUtilTest
{
    [Fact]
    public void ParseTest()
    {
        var xml = "<root><item>value</item></root>";
        var doc = XmlUtil.Parse(xml);
        Assert.NotNull(doc);
    }

    [Fact]
    public void CreateDocumentTest()
    {
        var doc = XmlUtil.CreateDocument();
        Assert.NotNull(doc);
    }

    [Fact]
    public void ToStrTest()
    {
        var xml = "<root><item>value</item></root>";
        var doc = XmlUtil.Parse(xml);
        var str = XmlUtil.ToStr(doc);
        Assert.Contains("root", str);
    }

    [Fact]
    public void SelectNodesTest()
    {
        var xml = "<root><item>value1</item><item>value2</item></root>";
        var nodes = XmlUtil.SelectNodes(xml, "//item");
        Assert.Equal(2, nodes.Count);
    }

    [Fact]
    public void GetElementTextTest()
    {
        var xml = "<root><item>value</item></root>";
        var text = XmlUtil.GetElementText(xml, "//item");
        Assert.Equal("value", text);
    }

    [Fact]
    public void RemoveElementTest()
    {
        var xml = "<root><item>value</item></root>";
        var result = XmlUtil.RemoveElement(xml, "//item");
        Assert.DoesNotContain("value", result);
    }
}
