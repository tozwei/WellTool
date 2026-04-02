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
/// Unicode工具类<br>
/// 提供Unicode相关的工具方法
/// </summary>
public static class UnicodeUtil
{
    /// <summary>
    /// 将字符串转换为Unicode编码
    /// </summary>
    /// <param name="text">要转换的字符串</param>
    /// <returns>Unicode编码的字符串</returns>
    public static string Encode(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var builder = new System.Text.StringBuilder();
        foreach (var c in text)
        {
            if (c > 127)
            {
                builder.Append($"\\u{(int)c:X4}");
            }
            else
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }

    /// <summary>
    /// 将Unicode编码的字符串转换为普通字符串
    /// </summary>
    /// <param name="text">Unicode编码的字符串</param>
    /// <returns>普通字符串</returns>
    public static string Decode(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return System.Text.RegularExpressions.Regex.Replace(
            text,
            @"\u([0-9a-fA-F]{4})",
            match => ((char)int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString()
        );
    }

    /// <summary>
    /// 判断字符是否为中文字符
    /// </summary>
    /// <param name="c">要判断的字符</param>
    /// <returns>是否为中文字符</returns>
    public static bool IsChinese(char c)
    {
        return c >= 0x4E00 && c <= 0x9FFF;
    }

    /// <summary>
    /// 判断字符串是否包含中文字符
    /// </summary>
    /// <param name="text">要判断的字符串</param>
    /// <returns>是否包含中文字符</returns>
    public static bool ContainsChinese(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        foreach (var c in text)
        {
            if (IsChinese(c))
            {
                return true;
            }
        }

        return false;
    }
}
