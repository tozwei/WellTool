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
    /// ANSI背景颜色
    /// <p>来自Spring Boot</p>
    /// </summary>
    public class AnsiBackground : IAnsiElement
    {
        /// <summary>
        /// 默认背景色
        /// </summary>
        public static readonly AnsiBackground Default = new AnsiBackground(49);

        /// <summary>
        /// 黑色
        /// </summary>
        public static readonly AnsiBackground Black = new AnsiBackground(40);

        /// <summary>
        /// 红
        /// </summary>
        public static readonly AnsiBackground Red = new AnsiBackground(41);

        /// <summary>
        /// 绿
        /// </summary>
        public static readonly AnsiBackground Green = new AnsiBackground(42);

        /// <summary>
        /// 黄
        /// </summary>
        public static readonly AnsiBackground Yellow = new AnsiBackground(43);

        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly AnsiBackground Blue = new AnsiBackground(44);

        /// <summary>
        /// 品红
        /// </summary>
        public static readonly AnsiBackground Magenta = new AnsiBackground(45);

        /// <summary>
        /// 青
        /// </summary>
        public static readonly AnsiBackground Cyan = new AnsiBackground(46);

        /// <summary>
        /// 白
        /// </summary>
        public static readonly AnsiBackground White = new AnsiBackground(47);

        /// <summary>
        /// 亮黑
        /// </summary>
        public static readonly AnsiBackground BrightBlack = new AnsiBackground(100);

        /// <summary>
        /// 亮红
        /// </summary>
        public static readonly AnsiBackground BrightRed = new AnsiBackground(101);

        /// <summary>
        /// 亮绿
        /// </summary>
        public static readonly AnsiBackground BrightGreen = new AnsiBackground(102);

        /// <summary>
        /// 亮黄
        /// </summary>
        public static readonly AnsiBackground BrightYellow = new AnsiBackground(103);

        /// <summary>
        /// 亮蓝
        /// </summary>
        public static readonly AnsiBackground BrightBlue = new AnsiBackground(104);

        /// <summary>
        /// 亮品红
        /// </summary>
        public static readonly AnsiBackground BrightMagenta = new AnsiBackground(105);

        /// <summary>
        /// 亮青
        /// </summary>
        public static readonly AnsiBackground BrightCyan = new AnsiBackground(106);

        /// <summary>
        /// 亮白
        /// </summary>
        public static readonly AnsiBackground BrightWhite = new AnsiBackground(107);

        private readonly int _code;

        private AnsiBackground(int code)
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
    }
}