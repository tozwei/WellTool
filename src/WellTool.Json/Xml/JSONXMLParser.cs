using System.Xml;

namespace WellTool.Json.Xml
{
    public class JSONXMLParser
    {
        public JSONObject Parse(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return ParseElement(doc.DocumentElement);
        }
        
        private JSONObject ParseElement(XmlElement element)
        {
            var json = new JSONObject();
            
            // 处理属性
            if (element.Attributes != null)
            {
                foreach (XmlAttribute attr in element.Attributes)
                {
                    json[attr.Name] = attr.Value;
                }
            }
            
            // 处理子元素
            if (element.HasChildNodes)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlElement childElement)
                    {
                        var childJson = ParseElement(childElement);
                        json[childElement.Name] = childJson;
                    }
                    else if (node is XmlText textNode && !string.IsNullOrWhiteSpace(textNode.Value))
                    {
                        json["#text"] = textNode.Value.Trim();
                    }
                }
            }
            
            return json;
        }
    }
}