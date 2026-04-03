using System;
using System.IO;

namespace WellTool.Core.IO.File;

/// <summary>
/// 行读取监视器
/// </summary>
public class LineReadWatcher : IDisposable
{
	private FileInfo _file;
	private long _lastPosition;
	private FileSystemWatcher _watcher;
	private bool _disposed;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="file">监视的文件</param>
	/// <param name="onLineRead">行读取回调</param>
	public LineReadWatcher(FileInfo file, Action<string> onLineRead)
	{
		_file = file;
		OnLineRead = onLineRead;

		_watcher = new FileSystemWatcher(file.DirectoryName, file.Name);
		_watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
		_watcher.Changed += OnFileChanged;
		_watcher.EnableRaisingEvents = true;

		_lastPosition = file.Exists ? file.Length : 0;
	}

	/// <summary>
	/// 行读取回调
	/// </summary>
	public Action<string> OnLineRead { get; set; }

	private void OnFileChanged(object sender, FileSystemEventArgs e)
	{
		if (_file.Exists && _file.Length > _lastPosition)
		{
			try
			{
				using var reader = _file.OpenText();
				reader.BaseStream.Seek(_lastPosition, SeekOrigin.Begin);
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					OnLineRead?.Invoke(line);
				}
				_lastPosition = _file.Length;
			}
			catch
			{
			}
		}
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_watcher?.Dispose();
			_disposed = true;
		}
	}
}
