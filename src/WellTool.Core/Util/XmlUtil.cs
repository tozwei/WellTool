using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;

namespace WellTool.Core.Util;

/// <summary>
/// XmlUtil工具类
/// </summary>
public static class XmlUtil
{
	/// <summary>
	/// XML编码
	/// </summary>
	public static string Escape(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return input.Replace("&", "&amp;")
				   .Replace("<", "&lt;")
				   .Replace(">", "&gt;")
				   .Replace("\"", "&quot;")
				   .Replace("'", "&apos;");
	}

	/// <summary>
	/// XML解码
	/// </summary>
	public static string Unescape(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		return input.Replace("&apos;", "'")
				   .Replace("&quot;", "\"")
				   .Replace("&gt;", ">")
				   .Replace("&lt;", "<")
				   .Replace("&amp;", "&");
	}

	/// <summary>
	/// 是否为XML安全字符
	/// </summary>
	public static bool IsXmlSafe(char c)
	{
		return c == 0x9 || c == 0xA || c == 0xD ||
			   (c >= 0x20 && c <= 0xD7FF) ||
			   (c >= 0xE000 && c <= 0xFFFD) ||
			   (c >= 0x10000 && c <= 0x10FFFF);
	}

	/// <summary>
	/// 转义无效XML字符
	/// </summary>
	public static string EscapeInvalid(string input)
	{
		if (string.IsNullOrEmpty(input))
			return input;
		var sb = new StringBuilder(input.Length);
		foreach (char c in input)
		{
			if (IsXmlSafe(c))
				sb.Append(c);
		}
		return sb.ToString();
	}

	/// <summary>
	/// 解析XML字符串为XDocument
	/// </summary>
	/// <param name="xmlStr">XML字符串</param>
	/// <returns>XDocument</returns>
	public static XDocument ParseXml(string xmlStr)
	{
		if (string.IsNullOrEmpty(xmlStr))
			return null;
		return XDocument.Parse(xmlStr);
	}

	/// <summary>
	/// XML格式字符串转换为Map
	/// </summary>
	/// <param name="xmlStr">XML字符串</param>
	/// <returns>XML数据转换后的Map</returns>
	public static Dictionary<string, object> XmlToMap(string xmlStr)
	{
		return XmlToMap(xmlStr, new Dictionary<string, object>());
	}

	/// <summary>
	/// XML格式字符串转换为Map
	/// </summary>
	/// <param name="xmlStr">XML字符串</param>
	/// <param name="result">结果Map</param>
	/// <returns>XML数据转换后的Map</returns>
	public static Dictionary<string, object> XmlToMap(string xmlStr, Dictionary<string, object> result)
	{
		if (string.IsNullOrEmpty(xmlStr))
			return result;
		var doc = XDocument.Parse(xmlStr);
		if (doc.Root == null)
			return result;
		return XmlToMap(doc.Root, result);
	}

	/// <summary>
	/// XML节点转换为Map
	/// </summary>
	/// <param name="node">XML节点</param>
	/// <returns>XML数据转换后的Map</returns>
	public static Dictionary<string, object> XmlToMap(XElement node)
	{
		return XmlToMap(node, new Dictionary<string, object>());
	}

	/// <summary>
	/// XML节点转换为Map
	/// </summary>
	/// <param name="node">XML节点</param>
	/// <param name="result">结果Map</param>
	/// <returns>XML数据转换后的Map</returns>
	public static Dictionary<string, object> XmlToMap(XElement node, Dictionary<string, object> result)
	{
		if (node == null || result == null)
		{
			result = new Dictionary<string, object>();
		}

		foreach (var childElement in node.Elements())
		{
			var key = childElement.Name.LocalName;
			object newValue;

			if (childElement.HasElements)
			{
				// 子节点继续递归遍历
				var map = XmlToMap(childElement);
				if (map.Count > 0)
				{
					newValue = map;
				}
				else
				{
					// 对于空的子节点，返回空字典而不是空字符串
					newValue = new Dictionary<string, object>();
				}
			}
			else
			{
				newValue = childElement.Value;
			}

			// 处理重复key的情况
			if (result.TryGetValue(key, out var existingValue))
			{
				if (existingValue is List<object> list)
				{
					list.Add(newValue);
				}
				else
				{
					result[key] = new List<object> { existingValue, newValue };
				}
			}
			else
			{
				result[key] = newValue;
			}
		}

		return result;
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <typeparam name="T">bean类型</typeparam>
	/// <param name="xmlStr">XML字符串</param>
	/// <returns>bean</returns>
	public static T XmlToBean<T>(string xmlStr) where T : class
	{
		return XmlToBean<T>(xmlStr, null);
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <typeparam name="T">bean类型</typeparam>
	/// <param name="xmlStr">XML字符串</param>
	/// <param name="copyOptions">Bean转换选项</param>
	/// <returns>bean</returns>
	public static T XmlToBean<T>(string xmlStr, CopyOptions copyOptions) where T : class
	{
		if (string.IsNullOrEmpty(xmlStr))
			return null;
		var doc = XDocument.Parse(xmlStr);
		if (doc.Root == null)
			return null;
		return XmlToBean(doc.Root, typeof(T), copyOptions) as T;
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <typeparam name="T">bean类型</typeparam>
	/// <param name="node">XML节点</param>
	/// <returns>bean</returns>
	public static T XmlToBean<T>(XElement node) where T : class
	{
		return XmlToBean<T>(node, null);
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <typeparam name="T">bean类型</typeparam>
	/// <param name="node">XML节点</param>
	/// <param name="copyOptions">Bean转换选项</param>
	/// <returns>bean</returns>
	public static T XmlToBean<T>(XElement node, CopyOptions copyOptions) where T : class
	{
		if (node == null)
			return null;
		return XmlToBean(node, typeof(T), copyOptions) as T;
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <param name="node">XML节点</param>
	/// <param name="bean">bean类</param>
	/// <returns>bean</returns>
	public static object XmlToBean(XElement node, Type bean)
	{
		return XmlToBean(node, bean, null);
	}

	/// <summary>
	/// XML转Java Bean
	/// </summary>
	/// <param name="node">XML节点</param>
	/// <param name="bean">bean类</param>
	/// <param name="copyOptions">Bean转换选项</param>
	/// <returns>bean</returns>
	public static object XmlToBean(XElement node, Type bean, CopyOptions copyOptions)
	{
		if (node == null || bean == null)
			return null;

		var map = XmlToMap(node);
		if (map != null && map.Count == 1)
		{
			// 只有key和bean的名称匹配时才做单一对象转换
			var firstKey = new List<string>(map.Keys)[0];
			if (bean.Name.Equals(firstKey, StringComparison.OrdinalIgnoreCase))
			{
				return BeanUtil.ToBean(map[firstKey], bean, copyOptions);
			}
		}
		return BeanUtil.ToBean(map, bean, copyOptions);
	}
}
