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
    /// ANSI背景颜色枚举
    /// <p>来自Spring Boot</p>
    /// </summary>
    public enum AnsiBackground : int, IAnsiElement
    {
        /// <summary>
        /// 默认背景色
        /// </summary>
        Default = 49,

        /// <summary>
        /// 黑色
        /// </summary>
        Black = 40,

        /// <summary>
        /// 红
        /// </summary>
        Red = 41,

        /// <summary>
        /// 绿
        /// </summary>
        Green = 42,

        /// <summary>
        /// 黄
        /// </summary>
        Yellow = 43,

        /// <summary>
        /// 蓝
        /// </summary>
        Blue = 44,

        /// <summary>
        /// 品红
        /// </summary>
        Magenta = 45,

        /// <summary>
        /// 青
        /// </summary>
        Cyan = 46,

        /// <summary>
        /// 白
        /// </summary>
        White = 47,

        /// <summary>
        /// 亮黑
        /// </summary>
        BrightBlack = 100,

        /// <summary>
        /// 亮红
        /// </summary>
        BrightRed = 101,

        /// <summary>
        /// 亮绿
        /// </summary>
        BrightGreen = 102,

        /// <summary>
        /// 亮黄
        /// </summary>
        BrightYellow = 103,

        /// <summary>
        /// 亮蓝
        /// </summary>
        BrightBlue = 104,

        /// <summary>
        /// 亮品红
        /// </summary>
        BrightMagenta = 105,

        /// <summary>
        /// 亮青
        /// </summary>
        BrightCyan = 106,

        /// <summary>
        /// 亮白
        /// </summary>
        BrightWhite = 107;

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