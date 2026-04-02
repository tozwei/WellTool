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
    /// ANSI文本样式风格枚举
    /// <p>来自Spring Boot</p>
    /// </summary>
    public enum AnsiStyle : int, IAnsiElement
    {
        /// <summary>
        /// 重置/正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 粗体或增加强度
        /// </summary>
        Bold = 1,

        /// <summary>
        /// 弱化（降低强度）
        /// </summary>
        Faint = 2,

        /// <summary>
        /// 斜体
        /// </summary>
        Italic = 3,

        /// <summary>
        /// 下划线
        /// </summary>
        Underline = 4;

        /// <summary>
        /// 获取ANSI文本样式风格代码
        /// </summary>
        /// <returns>文本样式风格代码</returns>
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