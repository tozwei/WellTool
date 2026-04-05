using System;
using System.IO;
using System.Threading;

namespace WellTool.Core.IO.File;

/// <summary>
/// 文件拷贝器
/// </summary>
public class FileCopier
{
	/// <summary>
	/// 是否覆盖目标文件
	/// </summary>
	private bool _isOverride;
	/// <summary>
	/// 是否拷贝所有属性
	/// </summary>
	private bool _isCopyAttributes;
	/// <summary>
	/// 当拷贝来源是目录时是否只拷贝目录下的内容
	/// </summary>
	private bool _isCopyContentIfDir;
	/// <summary>
	/// 当拷贝来源是目录时是否只拷贝文件而忽略子目录
	/// </summary>
	private bool _isOnlyCopyFile;
	/// <summary>
	/// 复制回调
	/// </summary>
	private Func<FileInfo, bool> _copyFilter;

	private readonly FileInfo _src;
	private FileInfo _dest;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="src">源文件</param>
	/// <param name="dest">目标文件</param>
	public FileCopier(FileInfo src, FileInfo dest)
	{
		_src = src;
		_dest = dest;
	}

	/// <summary>
	/// 是否覆盖目标文件
	/// </summary>
	public bool IsOverride
	{
		get => _isOverride;
		set => _isOverride = value;
	}

	/// <summary>
	/// 是否拷贝所有属性
	/// </summary>
	public bool IsCopyAttributes
	{
		get => _isCopyAttributes;
		set => _isCopyAttributes = value;
	}

	/// <summary>
	/// 当拷贝来源是目录时是否只拷贝目录下的内容
	/// </summary>
	public bool IsCopyContentIfDir
	{
		get => _isCopyContentIfDir;
		set => _isCopyContentIfDir = value;
	}

	/// <summary>
	/// 当拷贝来源是目录时是否只拷贝文件而忽略子目录
	/// </summary>
	public bool IsOnlyCopyFile
	{
		get => _isOnlyCopyFile;
		set => _isOnlyCopyFile = value;
	}

	/// <summary>
	/// 设置复制过滤器
	/// </summary>
	/// <param name="filter">过滤器</param>
	/// <returns>this</returns>
	public FileCopier SetCopyFilter(Func<FileInfo, bool> filter)
	{
		_copyFilter = filter;
		return this;
	}

	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <returns>拷贝后目标的文件或目录</returns>
	public FileInfo Copy()
	{
		if (_src == null)
		{
			throw new ArgumentNullException("Source file is null");
		}
		if (!_src.Exists)
		{
			throw new IOException($"File not exist: {_src}");
		}
		if (_dest == null)
		{
			throw new ArgumentNullException("Destination file is null");
		}

		if (_src.FullName == _dest.FullName)
		{
			throw new IOException($"Source and destination are the same");
		}

		if (_src.Attributes.HasFlag(FileAttributes.Directory))
		{
			if (_dest.Exists && !_dest.Attributes.HasFlag(FileAttributes.Directory))
			{
				throw new IOException("Source is a directory but destination is a file");
			}

			var target = _isCopyContentIfDir ? _dest : _dest;
			CopyDirContent(new DirectoryInfo(_src.FullName), new DirectoryInfo(target.FullName));
		}
		else
		{
			_dest = CopyFile(_src, _dest);
		}

		return _dest;
	}

	/// <summary>
	/// 拷贝目录内容
	/// </summary>
	private void CopyDirContent(DirectoryInfo src, DirectoryInfo dest)
	{
		if (!dest.Exists)
		{
			dest.Create();
		}

		foreach (var file in src.GetFiles())
		{
			if (_copyFilter != null && !_copyFilter(file))
			{
				continue;
			}
			CopyFile(file, new FileInfo(Path.Combine(dest.FullName, file.Name)));
		}

		if (!_isOnlyCopyFile)
		{
			foreach (var dir in src.GetDirectories())
			{
				var subDest = new DirectoryInfo(Path.Combine(dest.FullName, dir.Name));
				CopyDirContent(dir, subDest);
			}
		}
	}

	/// <summary>
	/// 拷贝文件
	/// </summary>
	private FileInfo CopyFile(FileInfo src, FileInfo dest)
	{
		if (_copyFilter != null && !_copyFilter(src))
		{
			return src;
		}

		if (dest.Exists)
		{
			if (dest.Attributes.HasFlag(FileAttributes.Directory))
			{
				dest = new FileInfo(Path.Combine(dest.FullName, src.Name));
			}

			if (dest.Exists && !_isOverride)
			{
				return src;
			}
		}
		else
		{
			dest.Directory?.Create();
		}

		src.CopyTo(dest.FullName, _isOverride);

		if (_isCopyAttributes)
		{
			dest.Attributes = src.Attributes;
		}

		return dest;
	}
}
