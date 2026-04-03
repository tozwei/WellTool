namespace WellTool.Core.io;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// NIO风格工具类
/// </summary>
public static class NioUtil
{
	/// <summary>
	/// 从流读取所有字节
	/// </summary>
	/// <param name="stream">流</param>
	/// <returns>字节数组</returns>
	public static byte[] ReadBytes(Stream stream)
	{
		if (stream is MemoryStream ms)
			return ms.ToArray();

		using var buffer = new MemoryStream();
		stream.CopyTo(buffer);
		return buffer.ToArray();
	}

	/// <summary>
	/// 异步读取所有字节
	/// </summary>
	/// <param name="stream">流</param>
	/// <param name="cancellationToken">取消令牌</param>
	/// <returns>字节数组</returns>
	public static async Task<byte[]> ReadBytesAsync(Stream stream, CancellationToken cancellationToken = default)
	{
		if (stream is MemoryStream ms)
			return ms.ToArray();

		using var buffer = new MemoryStream();
		await stream.CopyToAsync(buffer, cancellationToken);
		return buffer.ToArray();
	}

	/// <summary>
	/// 写入数据到流
	/// </summary>
	/// <param name="stream">流</param>
	/// <param name="data">数据</param>
	public static void Write(Stream stream, byte[] data)
	{
		stream.Write(data, 0, data.Length);
	}

	/// <summary>
	/// 异步写入数据到流
	/// </summary>
	/// <param name="stream">流</param>
	/// <param name="data">数据</param>
	/// <param name="cancellationToken">取消令牌</param>
	public static async Task WriteAsync(Stream stream, byte[] data, CancellationToken cancellationToken = default)
	{
		await stream.WriteAsync(data, 0, data.Length, cancellationToken);
	}

	/// <summary>
	/// 刷新流
	/// </summary>
	/// <param name="stream">流</param>
	public static void Flush(Stream stream)
	{
		if (stream is FileStream fs)
			fs.Flush();
		else
			stream.Flush();
	}

	/// <summary>
	/// 异步刷新流
	/// </summary>
	/// <param name="stream">流</param>
	/// <param name="cancellationToken">取消令牌</param>
	public static async Task FlushAsync(Stream stream, CancellationToken cancellationToken = default)
	{
		await stream.FlushAsync(cancellationToken);
	}
}
