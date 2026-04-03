using System;
using System.IO;

namespace WellTool.Core.IO.File;

/// <summary>
/// 文件追加器
/// </summary>
public class FileAppender : IDisposable
{
	private readonly StreamWriter _writer;
	private readonly bool _disposeWriter;
	private bool _disposed;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="file">文件</param>
	/// <param name="append">是否追加</param>
	public FileAppender(FileInfo file, bool append = true)
	{
		_writer = new StreamWriter(file.FullName, append);
		_disposeWriter = true;
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="writer">写入器</param>
	/// <param name="disposeWriter">是否销毁写入器</param>
	public FileAppender(StreamWriter writer, bool disposeWriter = false)
	{
		_writer = writer;
		_disposeWriter = disposeWriter;
	}

	/// <summary>
	/// 追加文本
	/// </summary>
	/// <param name="text">文本</param>
	public void Append(string text)
	{
		_writer.Write(text);
	}

	/// <summary>
	/// 追加行
	/// </summary>
	/// <param name="line">行文本</param>
	public void AppendLine(string line)
	{
		_writer.WriteLine(line);
	}

	/// <summary>
	/// 刷新缓冲区
	/// </summary>
	public void Flush()
	{
		_writer.Flush();
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			if (_disposeWriter)
			{
				_writer?.Dispose();
			}
			_disposed = true;
		}
	}
}
