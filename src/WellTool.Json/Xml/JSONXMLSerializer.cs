using System.Xml;

namespace WellTool.Json.Xml
{
    public class JSONXMLSerializer
    {
        public string Serialize(JSONObject json, string rootName = "root")
        {
            var doc = new XmlDocument();
            var root = doc.CreateElement(rootName);
            doc.AppendChild(root);
            
            SerializeObject(json, root, doc);
            return doc.OuterXml;
        }
        
        private void SerializeObject(JSONObject json, XmlElement parent, XmlDocument doc)
        {
            foreach (var key in json.Keys)
            {
                var value = json[key];
                
                if (key == "#text")
                {
                    parent.InnerText = value.ToString();
                }
                else if (value is JSONObject childObject)
                {
                    var element = doc.CreateElement(key);
                    parent.AppendChild(element);
                    SerializeObject(childObject, element, doc);
                }
                else if (value is JSONArray childArray)
                {
                    foreach (var item in childArray)
                    {
                        var element = doc.CreateElement(key);
                        parent.AppendChild(element);
                        if (item is JSONObject itemObject)
                        {
                            SerializeObject(itemObject, element, doc);
                        }
                        else
                        {
                            element.InnerText = item.ToString();
                        }
                    }
                }
                else
                {
                    parent.SetAttribute(key, value.ToString());
                }
            }
        }
    }
}