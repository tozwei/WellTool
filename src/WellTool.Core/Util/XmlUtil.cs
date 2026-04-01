using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WellTool.Core.Util
{
    /// <summary>
    /// XML 工具类
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 将对象序列化为 XML 字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>XML 字符串</returns>
        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var writer = new StringWriter();
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }

        /// <summary>
        /// 将 XML 字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">XML 字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(xml);
            return (T)serializer.Deserialize(reader);
        }

        /// <summary>
        /// 从文件加载 XML 并反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <returns>对象</returns>
        public static T DeserializeFromFile<T>(string filePath)
        {
            var xml = File.ReadAllText(filePath);
            return Deserialize<T>(xml);
        }

        /// <summary>
        /// 将对象序列化为 XML 并保存到文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="filePath">文件路径</param>
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            var xml = Serialize(obj);
            File.WriteAllText(filePath, xml);
        }

        /// <summary>
        /// 解析 XML 字符串为 XmlDocument
        /// </summary>
        /// <param name="xml">XML 字符串</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument ParseXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        /// <summary>
        /// 从文件加载 XML 为 XmlDocument
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument LoadXmlFromFile(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);
            return doc;
        }

        /// <summary>
        /// 获取 XML 节点的值
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="xpath">XPath 表达式</param>
        /// <returns>节点值</returns>
        public static string GetNodeValue(XmlDocument doc, string xpath)
        {
            var node = doc.SelectSingleNode(xpath);
            return node?.InnerText ?? string.Empty;
        }

        /// <summary>
        /// 设置 XML 节点的值
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="xpath">XPath 表达式</param>
        /// <param name="value">节点值</param>
        public static void SetNodeValue(XmlDocument doc, string xpath, string value)
        {
            var node = doc.SelectSingleNode(xpath);
            if (node != null)
            {
                node.InnerText = value;
            }
        }

        /// <summary>
        /// 创建 XML 节点
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="name">节点名称</param>
        /// <returns>XmlNode</returns>
        public static XmlNode CreateNode(XmlDocument doc, string name)
        {
            return doc.CreateElement(name);
        }

        /// <summary>
        /// 创建 XML 节点并设置值
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="name">节点名称</param>
        /// <param name="value">节点值</param>
        /// <returns>XmlNode</returns>
        public static XmlNode CreateNode(XmlDocument doc, string name, string value)
        {
            var node = doc.CreateElement(name);
            node.InnerText = value;
            return node;
        }

        /// <summary>
        /// 将 XmlDocument 保存到文件
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveXml(XmlDocument doc, string filePath)
        {
            doc.Save(filePath);
        }

        /// <summary>
        /// 将 XmlDocument 转换为字符串
        /// </summary>
        /// <param name="doc">XmlDocument</param>
        /// <returns>XML 字符串</returns>
        public static string ToString(XmlDocument doc)
        {
            using var writer = new StringWriter();
            doc.Save(writer);
            return writer.ToString();
        }
    }
}