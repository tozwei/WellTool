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
    public class AnsiColor : IAnsiElement
    {
        /// <summary>
        /// 默认前景色
        /// </summary>
        public static readonly AnsiColor Default = new AnsiColor(39);

        /// <summary>
        /// 黑
        /// </summary>
        public static readonly AnsiColor Black = new AnsiColor(30);

        /// <summary>
        /// 红
        /// </summary>
        public static readonly AnsiColor Red = new AnsiColor(31);

        /// <summary>
        /// 绿
        /// </summary>
        public static readonly AnsiColor Green = new AnsiColor(32);

        /// <summary>
        /// 黄
        /// </summary>
        public static readonly AnsiColor Yellow = new AnsiColor(33);

        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly AnsiColor Blue = new AnsiColor(34);

        /// <summary>
        /// 品红
        /// </summary>
        public static readonly AnsiColor Magenta = new AnsiColor(35);

        /// <summary>
        /// 青
        /// </summary>
        public static readonly AnsiColor Cyan = new AnsiColor(36);

        /// <summary>
        /// 白
        /// </summary>
        public static readonly AnsiColor White = new AnsiColor(37);

        /// <summary>
        /// 亮黑
        /// </summary>
        public static readonly AnsiColor BrightBlack = new AnsiColor(90);

        /// <summary>
        /// 亮红
        /// </summary>
        public static readonly AnsiColor BrightRed = new AnsiColor(91);

        /// <summary>
        /// 亮绿
        /// </summary>
        public static readonly AnsiColor BrightGreen = new AnsiColor(92);

        /// <summary>
        /// 亮黄
        /// </summary>
        public static readonly AnsiColor BrightYellow = new AnsiColor(93);

        /// <summary>
        /// 亮蓝
        /// </summary>
        public static readonly AnsiColor BrightBlue = new AnsiColor(94);

        /// <summary>
        /// 亮品红
        /// </summary>
        public static readonly AnsiColor BrightMagenta = new AnsiColor(95);

        /// <summary>
        /// 亮青
        /// </summary>
        public static readonly AnsiColor BrightCyan = new AnsiColor(96);

        /// <summary>
        /// 亮白
        /// </summary>
        public static readonly AnsiColor BrightWhite = new AnsiColor(97);

        private readonly int _code;

        private AnsiColor(int code)
        {
            _code = code;
        }

        /// <summary>
        /// 获取ANSI颜色代码
        /// </summary>
        /// <returns>颜色代码</returns>
        public int GetCode()
        {
            return _code;
        }

        /// <summary>
        /// 获取ANSI转义编码
        /// </summary>
        /// <returns>ANSI转义编码</returns>
        public override string ToString()
        {
            return _code.ToString();
        }

        /// <inheritdoc />
        public string ToAnsiString()
        {
            return $"\x1b[{_code}m";
        }
    }
}