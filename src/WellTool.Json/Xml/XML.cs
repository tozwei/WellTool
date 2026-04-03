using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WellTool.Json.Xml
{
    /// <summary>
    /// XML 工具类，提供 XML 和 JSON 之间的转换
    /// </summary>
    public static class XML
    {
        private static readonly char AMP = '&';
        private static readonly char LT = '<';
        private static readonly char GT = '>';
        private static readonly char QUOT = '"';

        /// <summary>
        /// 转换 XML 为 JSONObject
        /// </summary>
        /// <param name="xmlString">XML 字符串</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ToJSONObject(string xmlString)
        {
            return ToJSONObject(xmlString, false);
        }

        /// <summary>
        /// 转换 XML 为 JSONObject
        /// </summary>
        /// <param name="xmlString">XML 字符串</param>
        /// <param name="keepStrings">如果为 true，则值保持 String 类型</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ToJSONObject(string xmlString, bool keepStrings)
        {
            var result = new JSONObject();
            ToJSONObject(result, xmlString, keepStrings);
            return result;
        }

        /// <summary>
        /// 转换 XML 为 JSONObject
        /// </summary>
        /// <param name="jo">JSONObject</param>
        /// <param name="xmlString">XML 字符串</param>
        /// <param name="keepStrings">如果为 true，则值保持 String 类型</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ToJSONObject(JSONObject jo, string xmlString, bool keepStrings)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return jo;
            }

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlString);

                if (doc.DocumentElement != null)
                {
                    ParseXmlNode(jo, doc.DocumentElement, keepStrings);
                }
            }
            catch (System.Exception ex)
            {
                throw new JSONException("Error parsing XML", ex);
            }

            return jo;
        }

        /// <summary>
        /// 解析 XML 节点
        /// </summary>
        private static void ParseXmlNode(JSONObject parent, XmlNode node, bool keepStrings)
        {
            if (node.NodeType == XmlNodeType.Text)
            {
                var value = node.Value;
                if (!keepStrings)
                {
                    var parsedValue = ParseValue(value);
                    parent["#text"] = parsedValue;
                }
                else
                {
                    parent["#text"] = value;
                }
                return;
            }

            var childElements = new Dictionary<string, List<object>>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                {
                    var name = child.Name;

                    // 处理属性
                    foreach (XmlAttribute attr in child.Attributes)
                    {
                        var attrName = $"@{name}.@{attr.Name}";
                        object attrValue = keepStrings ? attr.Value : ParseValue(attr.Value);
                        AddToObject(parent, attrName, attrValue);
                    }

                    // 递归处理子元素
                    var childObj = new JSONObject();
                    ParseXmlNode(childObj, child, keepStrings);

                    // 移除 @type 属性，因为它用于标识类型
                    childObj.Remove("@type");

                    if (childObj.Count > 0)
                    {
                        AddToObject(parent, name, childObj.Count == 1 && childObj.ContainsKey("#text")
                            ? childObj["#text"]
                            : childObj);
                    }
                }
                else if (child.NodeType == XmlNodeType.Text)
                {
                    var value = child.Value?.Trim();
                    if (!string.IsNullOrEmpty(value))
                    {
                        object finalValue = value;
                        if (!keepStrings)
                        {
                            finalValue = ParseValue(value);
                        }
                        if (parent.ContainsKey("#text"))
                        {
                            // 如果已经存在 #text，转换为数组
                            var existing = parent["#text"];
                            if (existing is List<object> list)
                            {
                                list.Add(finalValue);
                            }
                            else
                            {
                                parent["#text"] = new List<object> { existing, finalValue };
                            }
                        }
                        else
                        {
                            parent["#text"] = finalValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加对象到父对象
        /// </summary>
        private static void AddToObject(JSONObject parent, string name, object value)
        {
            if (parent.ContainsKey(name))
            {
                var existing = parent[name];
                if (existing is List<object> list)
                {
                    list.Add(value);
                }
                else
                {
                    parent[name] = new List<object> { existing, value };
                }
            }
            else
            {
                parent[name] = value;
            }
        }

        /// <summary>
        /// 解析值
        /// </summary>
        private static object ParseValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            // 尝试解析为数字
            if (long.TryParse(value, out var longVal))
            {
                if (longVal >= int.MinValue && longVal <= int.MaxValue)
                {
                    return (int)longVal;
                }
                return longVal;
            }

            if (double.TryParse(value, out var doubleVal))
            {
                return doubleVal;
            }

            // 尝试解析为布尔值
            if (bool.TryParse(value, out var boolVal))
            {
                return boolVal;
            }

            return value;
        }

        /// <summary>
        /// 转换 JSONObject 为 XML
        /// </summary>
        /// <param name="obj">JSON 对象或数组</param>
        /// <returns>XML 字符串</returns>
        public static string ToXml(object obj)
        {
            return ToXml(obj, null);
        }

        /// <summary>
        /// 转换 JSONObject 为 XML
        /// </summary>
        /// <param name="obj">JSON 对象或数组</param>
        /// <param name="tagName">可选标签名称</param>
        /// <returns>XML 字符串</returns>
        public static string ToXml(object obj, string tagName)
        {
            return ToXml(obj, tagName, "content");
        }

        /// <summary>
        /// 转换 JSONObject 为 XML
        /// </summary>
        /// <param name="obj">JSON 对象或数组</param>
        /// <param name="tagName">可选标签名称</param>
        /// <param name="contentKeys">标识为内容的 key</param>
        /// <returns>XML 字符串</returns>
        public static string ToXml(object obj, string tagName, params string[] contentKeys)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(tagName))
            {
                sb.Append('<').Append(tagName).Append('>');
            }

            if (obj is JSONObject jsonObj)
            {
                AppendObjectToXml(sb, jsonObj, contentKeys);
            }
            else if (obj is JSONArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    if (item is JSONObject itemObj)
                    {
                        sb.Append('<').Append("item").Append('>');
                        AppendObjectToXml(sb, itemObj, contentKeys);
                        sb.Append("</item>");
                    }
                    else
                    {
                        sb.Append(EscapeXml(item?.ToString()));
                    }
                }
            }
            else
            {
                sb.Append(EscapeXml(obj.ToString()));
            }

            if (!string.IsNullOrEmpty(tagName))
            {
                sb.Append("</").Append(tagName).Append('>');
            }

            return sb.ToString();
        }

        /// <summary>
        /// 追加对象到 XML
        /// </summary>
        private static void AppendObjectToXml(StringBuilder sb, JSONObject obj, string[] contentKeys)
        {
            foreach (var kvp in obj)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                // 处理属性（以 @ 开头的 key）
                if (key.StartsWith("@"))
                {
                    // 属性处理，这里简化处理
                    continue;
                }

                // 处理内容键
                var isContentKey = false;
                foreach (var contentKey in contentKeys)
                {
                    if (key.Equals(contentKey, StringComparison.OrdinalIgnoreCase))
                    {
                        isContentKey = true;
                        break;
                    }
                }

                if (isContentKey)
                {
                    sb.Append(EscapeXml(value?.ToString()));
                }
                else
                {
                    sb.Append('<').Append(key).Append('>');

                    if (value is JSONObject nestedObj)
                    {
                        AppendObjectToXml(sb, nestedObj, contentKeys);
                    }
                    else if (value is JSONArray arr)
                    {
                        foreach (var item in arr)
                        {
                            if (item is JSONObject itemObj)
                            {
                                sb.Append("<item>");
                                AppendObjectToXml(sb, itemObj, contentKeys);
                                sb.Append("</item>");
                            }
                            else
                            {
                                sb.Append(EscapeXml(item?.ToString()));
                            }
                        }
                    }
                    else if (value != null)
                    {
                        sb.Append(EscapeXml(value.ToString()));
                    }

                    sb.Append("</").Append(key).Append('>');
                }
            }
        }

        /// <summary>
        /// 转义 XML 特殊字符
        /// </summary>
        private static string EscapeXml(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }
    }
}
