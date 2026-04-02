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

namespace WellTool.Core.Lang.Ansi
{
    /// <summary>
    /// ANSI标准颜色
    /// <p>来自Spring Boot</p>
    /// </summary>
    public enum AnsiColor : int, IAnsiElement
    {
        /// <summary>
        /// 默认前景色
        /// </summary>
        Default = 39,

        /// <summary>
        /// 黑
        /// </summary>
        Black = 30,

        /// <summary>
        /// 红
        /// </summary>
        Red = 31,

        /// <summary>
        /// 绿
        /// </summary>
        Green = 32,

        /// <summary>
        /// 黄
        /// </summary>
        Yellow = 33,

        /// <summary>
        /// 蓝
        /// </summary>
        Blue = 34,

        /// <summary>
        /// 品红
        /// </summary>
        Magenta = 35,

        /// <summary>
        /// 青
        /// </summary>
        Cyan = 36,

        /// <summary>
        /// 白
        /// </summary>
        White = 37,

        /// <summary>
        /// 亮黑
        /// </summary>
        BrightBlack = 90,

        /// <summary>
        /// 亮红
        /// </summary>
        BrightRed = 91,

        /// <summary>
        /// 亮绿
        /// </summary>
        BrightGreen = 92,

        /// <summary>
        /// 亮黄
        /// </summary>
        BrightYellow = 93,

        /// <summary>
        /// 亮蓝
        /// </summary>
        BrightBlue = 94,

        /// <summary>
        /// 亮品红
        /// </summary>
        BrightMagenta = 95,

        /// <summary>
        /// 亮青
        /// </summary>
        BrightCyan = 96,

        /// <summary>
        /// 亮白
        /// </summary>
        BrightWhite = 97;

        /// <summary>
        /// 获取ANSI颜色代码
        /// </summary>
        /// <returns>颜色代码</returns>
        public int GetCode()
        {
            return (int)this;
        }

        /// <summary>
        /// 获取ANSI转义编码
        /// </summary>
        /// <returns>ANSI转义编码</returns>
        public override string ToString()
        {
            return ((int)this).ToString();
        }
    }
}