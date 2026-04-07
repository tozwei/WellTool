
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class XmlUtilLastTest
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
}
