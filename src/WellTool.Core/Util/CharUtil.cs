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

namespace WellTool.Core.Util
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
        /// <summary>
        /// 检查字符是否为十六进制字符
        /// </summary>
        /// <param name="c">要检查的字符</param>
        /// <returns>如果是十六进制字符则返回true，否则返回false</returns>
        public static bool IsHexChar(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }

        /// <summary>
        /// 将十六进制字符转换为对应的整数
        /// </summary>
        /// <param name="c">十六进制字符</param>
        /// <returns>对应的整数</returns>
        public static int HexToInt(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            if (c >= 'a' && c <= 'f')
            {
                return c - 'a' + 10;
            }
            if (c >= 'A' && c <= 'F')
            {
                return c - 'A' + 10;
            }
            return 0;
        }

        /// <summary>
        /// 将字符转换为十六进制数字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>十六进制数字</returns>
        public static int Digit16(char c)
        {
            return HexToInt(c);
        }
    }
}