namespace WellTool.Core.util;

using System;
using System.IO;
using System.Text.Json;

/// <summary>
/// 序列化工具类
/// </summary>
public class SerializeUtil
{
	/// <summary>
	/// 序列化后拷贝流的方式克隆
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">被克隆对象</param>
	/// <returns>克隆后的对象</returns>
	public static T? Clone<T>(T obj) where T : class
	{
		if (obj == null) return null;
		return Deserialize<T>(Serialize(obj));
	}

	/// <summary>
	/// 序列化
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">要被序列化的对象</param>
	/// <returns>序列化后的字节码</returns>
	public static byte[] Serialize<T>(T obj) where T : class
	{
		if (obj == null) return Array.Empty<byte>();
		return JsonSerializer.SerializeToUtf8Bytes(obj);
	}

	/// <summary>
	/// 反序列化
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="bytes">反序列化的字节码</param>
	/// <returns>反序列化后的对象</returns>
	public static T? Deserialize<T>(byte[] bytes) where T : class
	{
		if (bytes == null || bytes.Length == 0) return null;
		try
		{
			return JsonSerializer.Deserialize<T>(bytes);
		}
		catch
		{
			return null;
		}
	}

	/// <summary>
	/// 序列化到文件
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="obj">对象</param>
	/// <param name="filePath">文件路径</param>
	public static void SerializeToFile<T>(T obj, string filePath) where T : class
	{
		if (obj == null) return;
		var bytes = Serialize(obj);
		File.WriteAllBytes(filePath, bytes);
	}

	/// <summary>
	/// 从文件反序列化
	/// </summary>
	/// <typeparam name="T">对象类型</typeparam>
	/// <param name="filePath">文件路径</param>
	/// <returns>反序列化后的对象</returns>
	public static T? DeserializeFromFile<T>(string filePath) where T : class
	{
		if (!File.Exists(filePath)) return null;
		var bytes = File.ReadAllBytes(filePath);
		return Deserialize<T>(bytes);
	}
}
