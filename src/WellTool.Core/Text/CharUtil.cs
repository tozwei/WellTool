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

namespace WellTool.Core.Text;

/// <summary>
/// 字符工具类
/// </summary>
public static class CharUtil
{
    /// <summary>
    /// 判断是否为字母
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为字母</returns>
    public static bool IsLetter(char c)
    {
        return char.IsLetter(c);
    }

    /// <summary>
    /// 判断是否为数字
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为数字</returns>
    public static bool IsDigit(char c)
    {
        return char.IsDigit(c);
    }

    /// <summary>
    /// 判断是否为字母或数字
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为字母或数字</returns>
    public static bool IsLetterOrDigit(char c)
    {
        return char.IsLetterOrDigit(c);
    }

    /// <summary>
    /// 判断是否为小写字母
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为小写字母</returns>
    public static bool IsLowerCase(char c)
    {
        return char.IsLower(c);
    }

    /// <summary>
    /// 判断是否为大写字母
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为大写字母</returns>
    public static bool IsUpperCase(char c)
    {
        return char.IsUpper(c);
    }

    /// <summary>
    /// 转换为小写字母
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>小写字母</returns>
    public static char ToLowerCase(char c)
    {
        return char.ToLower(c);
    }

    /// <summary>
    /// 转换为大写字母
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>大写字母</returns>
    public static char ToUpperCase(char c)
    {
        return char.ToUpper(c);
    }

    /// <summary>
    /// 判断是否为中文字符
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为中文字符</returns>
    public static bool IsChinese(char c)
    {
        return c >= 0x4e00 && c <= 0x9fa5;
    }

    /// <summary>
    /// 判断是否为空白字符
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为空白字符</returns>
    public static bool IsBlank(char c)
    {
        return char.IsWhiteSpace(c);
    }

    /// <summary>
    /// 判断是否为ASCII字符
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为ASCII字符</returns>
    public static bool IsAscii(char c)
    {
        return c <= 0x7f;
    }

    /// <summary>
    /// 判断是否为十六进制字符
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否为十六进制字符</returns>
    public static bool IsHexChar(char c)
    {
        return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
    }
}