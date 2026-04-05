namespace WellTool.Core.IO.File;

using WellTool.Core.IO;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 文件内容跟随器，实现类似 Linux 下 "tail -f" 命令功能
/// </summary>
public class Tailer : IDisposable
{
    private static readonly long SerialVersionUID = 1L;

    /// <summary>
    /// 默认编码
    /// </summary>
    private readonly Encoding _charset;

    /// <summary>
    /// 行处理器
    /// </summary>
    private readonly LineHandler _lineHandler;

    /// <summary>
    /// 初始读取的行数
    /// </summary>
    private readonly int _initReadLine;

    /// <summary>
    /// 定时任务检查间隔时长（毫秒）
    /// </summary>
    private readonly long _period;

    private readonly string _filePath;
    private long _lastPosition;
    private bool _stopOnDelete;
    private CancellationTokenSource? _cts;
    private Task? _task;
    private bool _disposed;

    /// <summary>
    /// 构造，默认UTF-8编码
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="lineHandler">行处理器</param>
    public Tailer(string file, LineHandler lineHandler) : this(file, lineHandler, 0)
    {
    }

    /// <summary>
    /// 构造，默认UTF-8编码
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="lineHandler">行处理器</param>
    /// <param name="initReadLine">启动时预读取的行数</param>
    public Tailer(string file, LineHandler lineHandler, int initReadLine)
    {
        _filePath = file ?? throw new ArgumentNullException(nameof(file));
        _lineHandler = lineHandler ?? throw new ArgumentNullException(nameof(lineHandler));
        _initReadLine = initReadLine;
        _period = 1000; // 默认1秒
        _charset = Encoding.UTF8;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="charset">编码</param>
    /// <param name="lineHandler">行处理器</param>
    /// <param name="initReadLine">启动时预读取的行数</param>
    /// <param name="period">检查间隔（毫秒）</param>
    public Tailer(string file, Encoding charset, LineHandler lineHandler, int initReadLine, long period)
    {
        _filePath = file ?? throw new ArgumentNullException(nameof(file));
        _charset = charset ?? Encoding.UTF8;
        _lineHandler = lineHandler ?? throw new ArgumentNullException(nameof(lineHandler));
        _initReadLine = initReadLine;
        _period = period;
    }

    /// <summary>
    /// 创建 Tailer
    /// </summary>
    /// <param name="file">文件路径</param>
    /// <param name="lineHandler">行处理器</param>
    /// <param name="initReadLine">初始读取行数</param>
    /// <returns>Tailer</returns>
    public static Tailer Create(string file, LineHandler lineHandler, int initReadLine)
    {
        return new Tailer(file, lineHandler, initReadLine);
    }

    /// <summary>
    /// 开始跟踪文件
    /// </summary>
    public void Start()
    {
        if (_task != null) return;

        _cts = new CancellationTokenSource();
        _lastPosition = GetFileLength();

        // 预读最后几行
        if (_initReadLine > 0)
        {
            ReadLastLines(_initReadLine);
        }

        _task = Task.Run(() => Run(_cts.Token));
    }

    /// <summary>
    /// 停止跟踪
    /// </summary>
    public void Stop()
    {
        _cts?.Cancel();
        _task?.Wait();
    }

    private void Run(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                long currentLength = GetFileLength();

                // 检查文件是否被删除
                if (!File.Exists(_filePath))
                {
                    if (_stopOnDelete)
                    {
                        break;
                    }
                    Thread.Sleep((int)_period);
                    continue;
                }

                if (currentLength > _lastPosition)
                {
                    ReadFrom(_lastPosition);
                    _lastPosition = currentLength;
                }
                else if (currentLength < _lastPosition)
                {
                    // 文件被截断，重新从开始读
                    _lastPosition = 0;
                    if (_initReadLine > 0)
                    {
                        ReadLastLines(_initReadLine);
                    }
                }

                Thread.Sleep((int)_period);
            }
            catch (ThreadInterruptedException)
            {
                break;
            }
            catch (Exception)
            {
                // 忽略异常继续
            }
        }
    }

    private long GetFileLength()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                return new FileInfo(_filePath).Length;
            }
        }
        catch
        {
            // 忽略
        }
        return 0;
    }

    private void ReadFrom(long position)
    {
        try
        {
            using var fs = new FileStream(_filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(position, SeekOrigin.Begin);
            using var reader = new StreamReader(fs, _charset);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                _lineHandler.Handle(line);
            }
        }
        catch (Exception)
        {
            // 忽略读取错误
        }
    }

    private void ReadLastLines(int count)
    {
        try
        {
            var lines = new ConcurrentQueue<string>();
            using var fs = new FileStream(_filePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fs, _charset);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (lines.Count >= count)
                {
                    lines.TryDequeue(out _);
                }
                lines.Enqueue(line);
            }

            foreach (var l in lines)
            {
                _lineHandler(l);
            }
        }
        catch (Exception)
        {
            // 忽略
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        Stop();
        _cts?.Dispose();
    }
}

/// <summary>
/// 控制台行处理器
/// </summary>
public class ConsoleLineHandler
{
    public static LineHandler Create()
    {
        return line => Console.WriteLine(line);
    }
}
