using Xunit;
using WellTool.Core;
using System.Xml.Linq;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Xml工具单元测试
    /// </summary>
    public class XmlUtilTest
    {
        [Fact]
        public void CreateXmlTest()
        {
            var doc = XmlUtil.CreateXml("root");
            Assert.NotNull(doc);
        }

        [Fact]
        public void CreateDocumentTest()
        {
            var doc = XmlUtil.CreateDocument("root");
            Assert.NotNull(doc);
        }

        [Fact]
        public void ParseTest()
        {
            var xml = "<root><item>value</item></root>";
            var doc = XmlUtil.Parse(xml);
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
        public void GetElementTest()
        {
            var xml = "<root><item>value</item></root>";
            var doc = XmlUtil.Parse(xml);
            var element = XmlUtil.GetElement(doc, "item");
            Assert.NotNull(element);
            Assert.Equal("value", element.Value);
        }

        [Fact]
        public void GetElementsTest()
        {
            var xml = "<root><item>1</item><item>2</item></root>";
            var doc = XmlUtil.Parse(xml);
            var elements = XmlUtil.GetElements(doc, "item");
            Assert.Equal(2, elements.Count);
        }

        [Fact]
        public void AddElementTest()
        {
            var xml = "<root><item>1</item></root>";
            var doc = XmlUtil.Parse(xml);
            XmlUtil.AddElement(doc.Root!, "newItem", "value");
            var result = XmlUtil.ToStr(doc);
            Assert.Contains("newItem", result);
        }

        [Fact]
        public void RemoveElementTest()
        {
            var xml = "<root><item>1</item></root>";
            var doc = XmlUtil.Parse(xml);
            var element = XmlUtil.GetElement(doc, "item");
            XmlUtil.RemoveElement(element!);
            var result = XmlUtil.ToStr(doc);
            Assert.DoesNotContain("item", result);
        }

        [Fact]
        public void TransTest()
        {
            var xml = "<root><item>1</item></root>";
            var doc = XmlUtil.Parse(xml);
            var str = XmlUtil.Trans(doc);
            Assert.Contains("item", str);
        }

        [Fact]
        public void EscapeTest()
        {
            var escaped = XmlUtil.Escape("<test>&");
            Assert.Contains("&lt;", escaped);
            Assert.Contains("&amp;", escaped);
        }

        [Fact]
        public void UnescapeTest()
        {
            var unescaped = XmlUtil.Unescape("&lt;test&gt;&amp;");
            Assert.Contains("<test>", unescaped);
            Assert.Contains("&", unescaped);
        }

        [Fact]
        public void XmlToMapTest()
        {
            var xml = "<root><key>value</key></root>";
            var map = XmlUtil.XmlToMap(xml);
            Assert.NotEmpty(map);
        }

        [Fact]
        public void MapToXmlTest()
        {
            var map = new System.Collections.Generic.Dictionary<string, object> { { "key", "value" } };
            var xml = XmlUtil.MapToXml(map);
            Assert.Contains("key", xml);
            Assert.Contains("value", xml);
        }
    }
}
