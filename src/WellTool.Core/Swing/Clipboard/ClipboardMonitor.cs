using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace WellTool.Core.Swing.Clipboard
{
    /// <summary>
    /// 剪贴板变化监听器
    /// </summary>
    public class ClipboardMonitor : IDisposable
    {
        private static ClipboardMonitor _instance;
        private static readonly object _lock = new object();
        private readonly DispatcherTimer _timer;
        private string _lastText;
        private readonly List<IClipboardListener> _listeners = new List<IClipboardListener>();
        private bool _disposed;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static ClipboardMonitor Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClipboardMonitor();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private ClipboardMonitor()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// 添加监听器
        /// </summary>
        public void AddListener(IClipboardListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        public void RemoveListener(IClipboardListener listener)
        {
            _listeners.Remove(listener);
        }

        /// <summary>
        /// 获取当前剪贴板文本
        /// </summary>
        public string GetText()
        {
            try
            {
                if (System.Windows.Clipboard.ContainsText())
                {
                    return System.Windows.Clipboard.GetText();
                }
            }
            catch
            {
                // 剪贴板可能被其他程序占用
            }
            return null;
        }

        /// <summary>
        /// 设置剪贴板文本
        /// </summary>
        public void SetText(string text)
        {
            try
            {
                System.Windows.Clipboard.SetText(text);
            }
            catch
            {
                // 剪贴板可能被其他程序占用
            }
        }

        /// <summary>
        /// 清空剪贴板
        /// </summary>
        public void Clear()
        {
            try
            {
                System.Windows.Clipboard.Clear();
            }
            catch
            {
                // 剪贴板可能被其他程序占用
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Clipboard.ContainsText())
                {
                    var text = System.Windows.Clipboard.GetText();
                    if (text != _lastText)
                    {
                        _lastText = text;
                        NotifyListeners(text);
                    }
                }
            }
            catch
            {
                // 剪贴板可能被其他程序占用
            }
        }

        private void NotifyListeners(string text)
        {
            foreach (var listener in _listeners.ToArray())
            {
                try
                {
                    listener.OnTextChanged(text);
                }
                catch
                {
                    // 忽略监听器中的异常
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();
                    _listeners.Clear();
                }
                _disposed = true;
            }
        }

        ~ClipboardMonitor()
        {
            Dispose(false);
        }
    }
}
