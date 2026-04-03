using System;
using System.Xml.Linq;

namespace WellTool.Core.Util;

/// <summary>
/// JAXB工具类（用于.NET XML处理）
/// </summary>
public static class JAXBUtil
{
	/// <summary>
	/// 将对象序列化为XML字符串
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <returns>XML字符串</returns>
	public static string ToXml<T>(T obj) where T : class
	{
		throw new NotSupportedException("Use XmlUtil instead for .NET XML serialization");
	}

	/// <summary>
	/// 将XML字符串反序列化为对象
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="xml">XML字符串</param>
	/// <returns>对象</returns>
	public static T FromXml<T>(string xml) where T : class
	{
		throw new NotSupportedException("Use XmlUtil instead for .NET XML deserialization");
	}
}
