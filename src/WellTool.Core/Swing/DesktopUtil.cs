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
using System.Diagnostics;

namespace WellTool.Core.Swing
{
    /// <summary>
    /// 桌面相关工具（平台相关）<br>
    /// Desktop 类允许应用程序启动已在本机桌面上注册的关联应用程序，以处理 URI 或文件。
    /// </summary>
    public static class DesktopUtil
    {
        /// <summary>
        /// 使用平台默认浏览器打开指定URL地址
        /// </summary>
        /// <param name="url">URL地址</param>
        public static void Browse(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (System.Exception e)
            {
                throw new WellToolException(e);
            }
        }

        /// <summary>
        /// 启动关联应用程序来打开文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void Open(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                throw new WellToolException(e);
            }
        }

        /// <summary>
        /// 启动关联编辑器应用程序并打开用于编辑的文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void Edit(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    Verb = "edit"
                });
            }
            catch (Exception e)
            {
                throw new WellToolException(e);
            }
        }

        /// <summary>
        /// 使用关联应用程序的打印命令, 用本机桌面打印设备来打印文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void Print(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    Verb = "print"
                });
            }
            catch (System.Exception e)
            {
                throw new WellToolException(e);
            }
        }

        /// <summary>
        /// 使用平台默认邮件客户端打开指定邮件地址
        /// </summary>
        /// <param name="mailAddress">邮件地址</param>
        public static void Mail(string mailAddress)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"mailto:{mailAddress}",
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                throw new WellToolException(e);
            }
        }
    }
}
