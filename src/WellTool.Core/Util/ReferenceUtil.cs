using System;
using System.IO;

namespace WellTool.Core.Util;

/// <summary>
/// 序列化工具类
/// </summary>
public static class SerializeUtil
{
	/// <summary>
	/// 序列化对象为字节数组
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <returns>字节数组</returns>
	public static byte[] Serialize<T>(T obj) where T : class
	{
		if (obj == null)
		{
			return null;
		}

		using var ms = new MemoryStream();
		var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		formatter.Serialize(ms, obj);
		return ms.ToArray();
	}

	/// <summary>
	/// 反序列化字节数组为对象
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="data">字节数组</param>
	/// <returns>对象</returns>
	public static T Deserialize<T>(byte[] data) where T : class
	{
		if (data == null || data.Length == 0)
		{
			return default;
		}

		using var ms = new MemoryStream(data);
		var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		return formatter.Deserialize(ms) as T;
	}

	/// <summary>
	/// 序列化对象到文件
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <param name="filePath">文件路径</param>
	public static void SerializeToFile<T>(T obj, string filePath) where T : class
	{
		var data = Serialize(obj);
		if (data != null)
		{
			File.WriteAllBytes(filePath, data);
		}
	}

	/// <summary>
	/// 从文件反序列化对象
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="filePath">文件路径</param>
	/// <returns>对象</returns>
	public static T DeserializeFromFile<T>(string filePath) where T : class
	{
		if (!File.Exists(filePath))
		{
			return default;
		}

		var data = File.ReadAllBytes(filePath);
		return Deserialize<T>(data);
	}
}
