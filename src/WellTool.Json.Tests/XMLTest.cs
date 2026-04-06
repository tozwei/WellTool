using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// XML 与 JSON 互转测试
    /// </summary>
    public class XMLTest
    {
        [Fact]
        public void TestXmlToJson()
        {
            var xmlStr = "<root><name>test</name><value>123</value></root>";
            var json = XMLToJSON(xmlStr);
            Assert.NotNull(json);
            Assert.Contains("test", json.ToString());
        }

        [Fact]
        public void TestXmlToJsonWithAttr()
        {
            var xmlStr = "<root id=\"1\"><name>test</name></root>";
            var json = XMLToJSON(xmlStr);
            Assert.NotNull(json);
        }

        [Fact]
        public void TestJsonToXml()
        {
            var jsonStr = "{\"name\":\"test\",\"value\":123}";
            var xml = JSONToXML(jsonStr);
            Assert.NotNull(xml);
            Assert.Contains("<name>test</name>", xml);
        }

        [Fact]
        public void TestXmlToJsonArray()
        {
            var xmlStr = "<root><item>1</item><item>2</item></root>";
            var json = XMLToJSON(xmlStr);
            Assert.NotNull(json);
        }

        private static JSONObject XMLToJSON(string xmlStr)
        {
            var xml = new System.Xml.XmlDocument();
            xml.LoadXml(xmlStr);
            var json = JSONUtil.ParseJson(Newtonsoft.Json.JsonConvert.SerializeXmlNode(xml.DocumentElement));
            return (JSONObject)json;
        }

        private static string JSONToXML(string jsonStr)
        {
            var json = JSONUtil.ParseObj(jsonStr);
            return Newtonsoft.Json.JsonConvert.SerializeXmlNode(ToXmlDocument(json), Newtonsoft.Json.Formatting.Indented);
        }

        private static System.Xml.XmlDocument ToXmlDocument(JSONObject json)
        {
            var doc = new System.Xml.XmlDocument();
            var root = doc.CreateElement("root");
            doc.AppendChild(root);
            foreach (var key in json.Keys)
            {
                var elem = doc.CreateElement(key.ToString());
                elem.InnerText = json[key]?.ToString() ?? "";
                root.AppendChild(elem);
            }
            return doc;
        }
    }
}
