using System;
using System.Windows;

namespace WellTool.Core.Swing.Clipboard
{
    /// <summary>
    /// 剪贴板内容变化监听器接口
    /// </summary>
    public interface IClipboardListener
    {
        /// <summary>
        /// 剪贴板内容变化时触发
        /// </summary>
        /// <param name="text">剪贴板文本内容</param>
        void OnTextChanged(string text);
    }

    /// <summary>
    /// 剪贴板监听器基类
    /// </summary>
    public abstract class ClipboardListener : IClipboardListener
    {
        /// <summary>
        /// 剪贴板内容变化时触发
        /// </summary>
        /// <param name="text">剪贴板文本内容</param>
        public abstract void OnTextChanged(string text);
    }
}
