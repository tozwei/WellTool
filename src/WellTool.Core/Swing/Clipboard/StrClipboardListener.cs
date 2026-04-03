using System;

namespace WellTool.Core.Swing.Clipboard
{
    /// <summary>
    /// 字符串剪贴板监听器
    /// </summary>
    public class StrClipboardListener : ClipboardListener
    {
        private readonly Action<string> _callback;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callback">文本变化回调</param>
        public StrClipboardListener(Action<string> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        /// <summary>
        /// 文本变化时触发
        /// </summary>
        public override void OnTextChanged(string text)
        {
            _callback(text);
        }
    }
}
