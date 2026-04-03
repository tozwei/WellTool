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

using System.Drawing;

namespace WellTool.Core.Swing
{
    /// <summary>
    /// 屏幕相关（当前显示设置）工具类
    /// </summary>
    public static class ScreenUtil
    {
        /// <summary>
        /// 获取屏幕宽度
        /// </summary>
        /// <returns>屏幕宽度</returns>
        public static int GetWidth()
        {
            return System.Windows.Forms.Screen.PrimaryScreen?.Bounds.Width ?? 1920;
        }

        /// <summary>
        /// 获取屏幕高度
        /// </summary>
        /// <returns>屏幕高度</returns>
        public static int GetHeight()
        {
            return System.Windows.Forms.Screen.PrimaryScreen?.Bounds.Height ?? 1080;
        }

        /// <summary>
        /// 获取屏幕的矩形
        /// </summary>
        /// <returns>屏幕的矩形</returns>
        public static Rectangle GetRectangle()
        {
            return System.Windows.Forms.Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
        }

        //-------------------------------------------------------------------------------------------- 截屏

        /// <summary>
        /// 截取全屏
        /// </summary>
        /// <returns>截屏的图片</returns>
        public static Bitmap CaptureScreen()
        {
            return RobotUtil.CaptureScreen();
        }

        /// <summary>
        /// 截取全屏到文件
        /// </summary>
        /// <param name="outFile">写出到的文件</param>
        /// <returns>写出到的文件</returns>
        public static string CaptureScreen(string outFile)
        {
            return RobotUtil.CaptureScreen(outFile);
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="screenRect">截屏的矩形区域</param>
        /// <returns>截屏的图片</returns>
        public static Bitmap CaptureScreen(Rectangle screenRect)
        {
            return RobotUtil.CaptureScreen(screenRect);
        }

        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="screenRect">截屏的矩形区域</param>
        /// <param name="outFile">写出到的文件</param>
        /// <returns>写出到的文件</returns>
        public static string CaptureScreen(Rectangle screenRect, string outFile)
        {
            return RobotUtil.CaptureScreen(screenRect, outFile);
        }
    }
}
