namespace WellTool.Core.IO.Watch;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 文件监听服务器
/// </summary>
public class WatchServer : IDisposable
{
    private readonly FileSystemWatcher _watcher;
    private readonly Watcher _watch;
    private CancellationTokenSource? _cts;
    private Task? _task;
    private bool _disposed;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="path">监听路径</param>
    /// <param name="watcher">监听器</param>
    public WatchServer(string path, Watcher watcher)
    {
        _watcher = new FileSystemWatcher(path);
        _watch = watcher ?? throw new ArgumentNullException(nameof(watcher));
    }

    /// <summary>
    /// 设置是否包含子目录
    /// </summary>
    public bool IncludeSubdirectories
    {
        get => _watcher.IncludeSubdirectories;
        set => _watcher.IncludeSubdirectories = value;
    }

    /// <summary>
    /// 设置监听过滤器
    /// </summary>
    public string Filter
    {
        get => _watcher.Filter;
        set => _watcher.Filter = value;
    }

    /// <summary>
    /// 开始监听
    /// </summary>
    public void Start()
    {
        if (_task != null) return;

        _cts = new CancellationTokenSource();

        _watcher.Created += OnCreated;
        _watcher.Changed += OnChanged;
        _watcher.Deleted += OnDeleted;
        _watcher.Renamed += OnRenamed;
        _watcher.Error += OnError;

        _watcher.EnableRaisingEvents = true;

        _task = Task.Run(() =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                Thread.Sleep(100);
            }
        });
    }

    /// <summary>
    /// 停止监听
    /// </summary>
    public void Stop()
    {
        _cts?.Cancel();
        _watcher.EnableRaisingEvents = false;
        _task?.Wait();
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        _watch.OnCreate(e.FullPath);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        _watch.OnChange(e.FullPath);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        _watch.OnDelete(e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        _watch.OnRename(e.OldFullPath, e.FullPath);
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        _watch.OnError(e.GetException());
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        Stop();
        _watcher.Dispose();
        _cts?.Dispose();
    }
}
