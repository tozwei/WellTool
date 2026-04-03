// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WellTool.Core.Swing
{
    /// <summary>
    /// 屏幕截图和输入模拟工具类
    /// </summary>
    public static class RobotUtil
    {
        /// <summary>
        /// 默认的延迟时间（毫秒）
        /// </summary>
        private static int _delay;

        /// <summary>
        /// 获取或设置默认的延迟时间<br>
        /// 当按键执行完后的等待时间
        /// </summary>
        public static int Delay
        {
            get => _delay;
            set => _delay = value;
        }

        /// <summary>
        /// 模拟鼠标移动
        /// </summary>
        /// <param name="x">移动到的x坐标</param>
        /// <param name="y">移动到的y坐标</param>
        public static void MouseMove(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// 模拟鼠标左键单击
        /// </summary>
        public static void Click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            DelayAction();
        }

        /// <summary>
        /// 模拟鼠标右键单击
        /// </summary>
        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            DelayAction();
        }

        /// <summary>
        /// 模拟鼠标滚轮滚动
        /// </summary>
        /// <param name="wheelAmt">滚动数，负数表示向前滚动，正数向后滚动</param>
        public static void MouseWheel(int wheelAmt)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)(wheelAmt * 120), 0);
            DelayAction();
        }

        /// <summary>
        /// 模拟键盘点击<br>
        /// 包括键盘的按下和释放
        /// </summary>
        /// <param name="keyCodes">按键码列表</param>
        public static void KeyClick(params int[] keyCodes)
        {
            foreach (var keyCode in keyCodes)
            {
                keybd_event((byte)keyCode, 0, 0, 0);
                keybd_event((byte)keyCode, 0, KEYEVENTF_KEYUP, 0);
            }
            DelayAction();
        }

        /// <summary>
        /// Shift+ 按键
        /// </summary>
        /// <param name="key">按键码</param>
        public static void KeyPressWithShift(int key)
        {
            keybd_event(VK_SHIFT, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
            DelayAction();
        }

        /// <summary>
        /// Ctrl+ 按键
        /// </summary>
        /// <param name="key">按键码</param>
        public static void KeyPressWithCtrl(int key)
        {
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
            DelayAction();
        }

        /// <summary>
        /// Alt+ 按键
        /// </summary>
        /// <param name="key">按键码</param>
        public static void KeyPressWithAlt(int key)
        {
            keybd_event(VK_ALT, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);
            DelayAction();
        }

        /// <summary>
        /// 截取全屏
        /// </summary>
        /// <returns>截屏的图片</returns>
        public static Bitmap CaptureScreen()
        {
            return CaptureScreen(ScreenUtil.GetRectangle());
        }

        /// <summary>
        /// 截取全屏到文件
        /// </summary>
        /// <param name="outFile">写出到的文件</param>
        /// <returns>写出到的文件</returns>
        public static string CaptureScreen(string outFile)
        {
            using (var bitmap = CaptureScreen())
            {
                bitmap.Save(outFile);
            }
            return outFile;
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="screenRect">截屏的矩形区域</param>
        /// <returns>截屏的图片</returns>
        public static Bitmap CaptureScreen(Rectangle screenRect)
        {
            var width = screenRect.Width;
            var height = screenRect.Height;

            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(screenRect.X, screenRect.Y, 0, 0, new Size(width, height));
            }
            return bitmap;
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="screenRect">截屏的矩形区域</param>
        /// <param name="outFile">写出到的文件</param>
        /// <returns>写出到的文件</returns>
        public static string CaptureScreen(Rectangle screenRect, string outFile)
        {
            using (var bitmap = CaptureScreen(screenRect))
            {
                bitmap.Save(outFile);
            }
            return outFile;
        }

        /// <summary>
        /// 等待指定毫秒数
        /// </summary>
        public static void DelayAction()
        {
            if (_delay > 0)
            {
                Thread.Sleep(_delay);
            }
        }

        #region Windows API

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        private const byte VK_SHIFT = 0x10;
        private const byte VK_CONTROL = 0x11;
        private const byte VK_ALT = 0x12;

        #endregion
    }
}
