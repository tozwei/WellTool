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
    /// ANSI文本样式风格
    /// <p>来自Spring Boot</p>
    /// </summary>
    public class AnsiStyleCodes : IAnsiElement
    {
        /// <summary>
        /// 重置/正常
        /// </summary>
        public static readonly AnsiStyleCodes Normal = new AnsiStyleCodes(0);

        /// <summary>
        /// 粗体或增加强度
        /// </summary>
        public static readonly AnsiStyleCodes Bold = new AnsiStyleCodes(1);

        /// <summary>
        /// 弱化（降低强度）
        /// </summary>
        public static readonly AnsiStyleCodes Faint = new AnsiStyleCodes(2);

        /// <summary>
        /// 斜体
        /// </summary>
        public static readonly AnsiStyleCodes Italic = new AnsiStyleCodes(3);

        /// <summary>
        /// 下划线
        /// </summary>
        public static readonly AnsiStyleCodes Underline = new AnsiStyleCodes(4);

        private readonly int _code;

        private AnsiStyleCodes(int code)
        {
            _code = code;
        }

        /// <summary>
        /// 获取ANSI文本样式风格代码
        /// </summary>
        /// <returns>文本样式风格代码</returns>
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